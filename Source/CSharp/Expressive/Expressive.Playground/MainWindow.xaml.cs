using Expressive.Exceptions;
using Expressive.Playground.TextMarking;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Snippets;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Expressive.Playground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private CompletionWindow _completionWindow;
        private DispatcherTimer _evaluationTimer;
        private IList<FunctionCompletionData> _functions;
        private ITextMarkerService _textMarkerService;

        #endregion

        #region Commands

        public ICommand EvaluateCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        #endregion

        #region Properties

        private ExpressiveOptions Options
        {
            get
            {
                var options = ExpressiveOptions.None;

                if (this.IgnoreCase)
                {
                    options |= ExpressiveOptions.IgnoreCase;
                }
                if (this.NoCache)
                {
                    options |= ExpressiveOptions.NoCache;
                }
                if (this.RoundAwayFromZero)
                {
                    options |= ExpressiveOptions.RoundAwayFromZero;
                }

                return options;
            }
        }

        #region Options

        public bool IgnoreCase
        {
            get { return (bool)GetValue(IgnoreCaseProperty); }
            set { SetValue(IgnoreCaseProperty, value); }
        }

        public static readonly DependencyProperty IgnoreCaseProperty =
            DependencyProperty.Register("IgnoreCase", typeof(bool), typeof(MainWindow), new PropertyMetadata(false, MainWindow.IgnoreCasePropertyChanged));

        private static void IgnoreCasePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var typedSender = (MainWindow)sender;

            typedSender.TriggerEvaluation();
        }

        public bool NoCache
        {
            get { return (bool)GetValue(NoCacheProperty); }
            set { SetValue(NoCacheProperty, value); }
        }

        public static readonly DependencyProperty NoCacheProperty =
            DependencyProperty.Register("NoCache", typeof(bool), typeof(MainWindow), new PropertyMetadata(false, MainWindow.NoCachePropertyChanged));

        private static void NoCachePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var typedSender = (MainWindow)sender;

            typedSender.TriggerEvaluation();
        }

        public bool RoundAwayFromZero
        {
            get { return (bool)GetValue(RoundAwayFromZeroProperty); }
            set { SetValue(RoundAwayFromZeroProperty, value); }
        }

        public static readonly DependencyProperty RoundAwayFromZeroProperty =
            DependencyProperty.Register("RoundAwayFromZero", typeof(bool), typeof(MainWindow), new PropertyMetadata(false, MainWindow.RoundAwayFromZeroPropertyChanged));

        private static void RoundAwayFromZeroPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var typedSender = (MainWindow)sender;

            typedSender.TriggerEvaluation();
        }

        #endregion

        public object Result
        {
            get { return (object)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register("Result", typeof(object), typeof(MainWindow), new PropertyMetadata(null));
        
        public ObservableCollection<VariableDescriptor> Variables
        {
            get { return (ObservableCollection<VariableDescriptor>)GetValue(VariablesProperty); }
            set { SetValue(VariablesProperty, value); }
        }

        public static readonly DependencyProperty VariablesProperty =
            DependencyProperty.Register("Variables", typeof(ObservableCollection<VariableDescriptor>), typeof(MainWindow), new PropertyMetadata(null));

        public string VersionText
        {
            get { return (string)GetValue(VersionTextProperty); }
            set { SetValue(VersionTextProperty, value); }
        }

        public static readonly DependencyProperty VersionTextProperty =
            DependencyProperty.Register("VersionText", typeof(string), typeof(MainWindow), new PropertyMetadata(null));

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.Variables = new ObservableCollection<VariableDescriptor>();
            this.Variables.CollectionChanged += Variables_CollectionChanged;

            textEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            textEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;
            textEditor.TextArea.TextInput += TextArea_TextInput;
            textEditor.TextChanged += TextEditor_TextChanged;

            this.InitialiseCommands();
            this.InitialiseFunctions();
        }

        #endregion

        #region Event Handlers

        private void TextArea_TextInput(object sender, TextCompositionEventArgs e)
        {
            this.UpdateTimer();
        }

        private void Variables_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var oldItem in e.OldItems.OfType<VariableDescriptor>())
                {
                    oldItem.PropertyChanged -= Variable_PropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems.OfType<VariableDescriptor>())
                {
                    newItem.PropertyChanged += Variable_PropertyChanged;
                }
            }

            this.UpdateTimer();
        }

        private void Variable_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.UpdateTimer();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    this.VersionText = "v" + ((AssemblyFileVersionAttribute)attributes[0]).Version;
                }

                Assembly resourceAssembly = Assembly.GetExecutingAssembly();
                string[] manifests = resourceAssembly.GetManifestResourceNames();

                using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Expressive.Playground.ExpressiveSyntax.xshd"))
                {
                    using (XmlTextReader reader = new XmlTextReader(s))
                    {
                        textEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);

                        // Dynamic syntax highlighting for your own purpose
                        var rules = textEditor.SyntaxHighlighting.MainRuleSet.Rules;
                        
                        var highlightingRule = new HighlightingRule();
                        highlightingRule.Color = new HighlightingColor { Foreground = new CustomBrush((Color)new ColorConverter().ConvertFromInvariantString("#569cd6")) };

                        // TODO this needs to be populated from the Expression class or some other exposed class
                        var wordList = _functions.Select(f => f.Text).ToArray();    
                        //String[] wordList = new[]
                        //{
                        //    // Date
                        //    "Days",
                        //    // Logical
                        //    "If", "In",
                        //    // Mathematical
                        //    "Abs", "Acos", "Asin", "Atan", "Ceiling", "Cos", "Count", "Exp", "Floor", "IEEERemainder", "Log10", "Log", "Max", "Min", "Pow", "Sign", "Sin", "Sqrt", "Sum", "Tan", "Truncate",
                        //    // Statistical
                        //    "Average", "Mean", "Median", "Mode",
                        //    // String
                        //    "Length", "PadLeft", "PadRight", "Regex", "Substring",
                        //};
                        String regex = String.Format(@"\b({0})\w*\b", String.Join("|", wordList));
                        highlightingRule.Regex = new Regex(regex, RegexOptions.IgnoreCase);

                        rules.Add(highlightingRule);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            this.InitializeTextMarkerService();
            this.textEditor.Focus();
        }

        void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            var matchingFunctions = _functions.Where(f => f.Text.StartsWith(e.Text, StringComparison.OrdinalIgnoreCase));

            if (matchingFunctions.Any() &&
                _completionWindow == null&&
                this.textEditor.Text[Math.Max(this.textEditor.CaretOffset - 2, 0)] != '[' &&
                this.textEditor.Text[Math.Max(this.textEditor.CaretOffset - 2, 0)] != '\'' &&
                this.textEditor.Text[Math.Max(this.textEditor.CaretOffset - 2, 0)] != '#') // Prevent showing the function intellisense if we entering a variable.
            {
                // Open code completion after the user has pressed dot:
                _completionWindow = new CompletionWindow(textEditor.TextArea);
                IList<ICompletionData> data = _completionWindow.CompletionList.CompletionData;

                foreach (var item in matchingFunctions)
                {
                    data.Add(item);
                }

                _completionWindow.Show();
                _completionWindow.Closed += delegate
                {
                    _completionWindow = null;
                };
            }
            else if (e.Text == "[" && this.Variables.Any())
            {
                _completionWindow = new CompletionWindow(textEditor.TextArea);
                IList<ICompletionData> data = _completionWindow.CompletionList.CompletionData;
                foreach (var variable in this.Variables)
                {
                    data.Add(new VariableCompletionData(variable.Name));
                }

                _completionWindow.Show();
                _completionWindow.Closed += delegate
                {
                    _completionWindow = null;
                };
            }

            this.UpdateTimer();
        }

        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && _completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    _completionWindow.CompletionList.RequestInsertion(e);
                    //_variableBuilder = null;
                }
            }
            // Do not set e.Handled=true.
            // We still want to insert the character that was typed.
        }

        private void TextEditor_TextChanged(object sender, EventArgs e)
        {
            this.UpdateTimer();
        }

        private void EvaluationTimer_Tick(object sender, EventArgs e)
        {
            _evaluationTimer.Stop();

            this.TriggerEvaluation();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var t = this.textEditor.SelectedText;

            var result = this.EvaluateExpression(this.textEditor.SelectedText);
            
            MessageBox.Show($"Result: {result}");
        }

        private void textEditor_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (_completionWindow != null)
                {
                    var item = _completionWindow.CompletionList.SelectedItem;

                    if (item != null)
                    {
                        _completionWindow.CompletionList.RequestInsertion(e);
                        e.Handled = true;
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        bool IsSelected(ITextMarker marker)
        {
            int selectionEndOffset = textEditor.SelectionStart + textEditor.SelectionLength;
            if (marker.StartOffset >= textEditor.SelectionStart && marker.StartOffset <= selectionEndOffset)
                return true;
            if (marker.EndOffset >= textEditor.SelectionStart && marker.EndOffset <= selectionEndOffset)
                return true;
            return false;
        }

        private object EvaluateExpression(string expressionText)
        {
            object result = null;

            _textMarkerService.RemoveAll(IsSelected);

            if (!string.IsNullOrWhiteSpace(expressionText))
            {
                Expression expression = new Expression(expressionText, this.Options);
                try
                {
                    result = expression.Evaluate(this.Variables.Where(v => !string.IsNullOrWhiteSpace(v.Name)).ToDictionary(v => v.Name, v => v.TypedValue));
                }
                catch (ExpressiveException ee)
                {
                    //if (ee.InnerException is MissingTokenException)
                    //{
                    //    var mte = (MissingTokenException)ee.InnerException;
                    //    ITextMarker marker = textMarkerService.Create(mte.StartOffset, mte.Length);
                    //    marker.MarkerTypes = TextMarkerTypes.SquigglyUnderline;
                    //    marker.MarkerColor = Colors.Red;
                    //}
                    result = ee.Message;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                
            }

            return result;
        }

        private void InitialiseCommands()
        {
            this.EvaluateCommand = new RelayCommand((o) => this.EvaluateCommandHandler());
            this.OpenCommand = new RelayCommand((o) => this.OpenCommandHandler());
            this.SaveCommand = new RelayCommand((o) => this.SaveCommandHandler());
        }

        private void InitialiseFunctions()
        {
            _functions = new List<FunctionCompletionData>();
            // TODO actually populate from the Expressive library
            // Date
            _functions.Add(new FunctionCompletionData("AddDays", "Returns the specified date with the specified number of days added to it.", VariableType.Date, VariableType.Number));
            _functions.Add(new FunctionCompletionData("DayOf", "Return the day component of the specified date.", VariableType.Date));
            _functions.Add(new FunctionCompletionData("DaysBetween", "Returns number of days between the specified two dates.", VariableType.Date, VariableType.Date));
            // Logical
            _functions.Add(new FunctionCompletionData("If", "Allows you to make logical comparisons between a value and what you expect and return a value on whether the expected value matches the actual.", VariableType.Boolean, VariableType.String, VariableType.String));
            _functions.Add(new FunctionCompletionData("In", "Returns true if the first specified value exists within the following specified value(s).", VariableType.Number, VariableType.Number));
            // Mathematical
            _functions.Add(new FunctionCompletionData("Abs", "Returns the absolute value of the specified number.", VariableType.Number));
            _functions.Add(new FunctionCompletionData("Acos", "Returns the angle whose cosine is the specified number.", VariableType.Number));
            _functions.Add(new FunctionCompletionData("Asin", "Returns the angle whose sine is the specified number.", VariableType.Number));
            _functions.Add(new FunctionCompletionData("Atan", "Returns the angle whose tangent is the specified number.", VariableType.Number));
            _functions.Add(new FunctionCompletionData("Round", "Returns the first specified number rounded to the specified number of decimal places.", VariableType.Number, VariableType.Number));
            _functions.Add(new FunctionCompletionData("Sqrt", "Returns the square root of a specified number.", VariableType.Number));
            // Statistical
            _functions.Add(new FunctionCompletionData("Average", "Returns the average number from the specified numbers.", VariableType.Number));
            _functions.Add(new FunctionCompletionData("Mean", "Returns the average number from the specified numbers.", VariableType.Number));
            _functions.Add(new FunctionCompletionData("Median", "Returns the median number from the specified numbers.", VariableType.Number));
            _functions.Add(new FunctionCompletionData("Mode", "Returns the mode number from the specified numbers.", VariableType.Number));
            // String
            _functions.Add(new FunctionCompletionData("Length", "Gets the length of the string", VariableType.String));
            _functions.Add(new FunctionCompletionData("PadLeft", "Returns a new string that right-aligns the characters in the specified string by padding them on the left with a specified Unicode character, for a specified total length.", VariableType.String, VariableType.Number, VariableType.String));
            _functions.Add(new FunctionCompletionData("PadRight", "Returns a new string that left-aligns the characters in the specified string by padding them on the right with a specified Unicode character, for a specified total length.", VariableType.String, VariableType.Number, VariableType.String));
            //_functions.Add(new FunctionCompletionData("Regex", "Gets the length of the string", VariableType.String));
            _functions.Add(new FunctionCompletionData("Substring", "Retrieves a substring from the specified string. The substring starts at a specified character position and has a specified length.", VariableType.String, VariableType.Number, VariableType.Number));

        }

        void InitializeTextMarkerService()
        {
            var textMarkerService = new TextMarkerService(textEditor.Document);
            textEditor.TextArea.TextView.BackgroundRenderers.Add(textMarkerService);
            textEditor.TextArea.TextView.LineTransformers.Add(textMarkerService);
            IServiceContainer services = (IServiceContainer)textEditor.Document.ServiceProvider.GetService(typeof(IServiceContainer));
            if (services != null)
                services.AddService(typeof(ITextMarkerService), textMarkerService);
            this._textMarkerService = textMarkerService;
        }

        private void TriggerEvaluation()
        {
            foreach (var line in this.textEditor.Document.Lines)
            {

                var t = line.ToString();
            }


            //foreach (var expressionText in this.textEditor.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string expressionText = this.textEditor.Text;
                
                Expression expression = new Expression(expressionText, this.Options);

                // Add any variables into the data grid if they are not already in there.
                try
                {
                    foreach (var variable in expression.ReferencedVariables)
                    {
                        if (!this.Variables.Any(v => string.Equals(v.Name, variable, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            this.Variables.Add(new VariableDescriptor { Name = variable, Value = "" });
                        }
                    }
                }
                catch (Exception)
                {

                }

                object result = this.EvaluateExpression(expressionText);

                this.Result = result;
            }
        }

        private void UpdateTimer()
        {
            if (_evaluationTimer == null)
            {
                _evaluationTimer = new DispatcherTimer();
                _evaluationTimer.Tick += EvaluationTimer_Tick;
                _evaluationTimer.Interval = TimeSpan.FromMilliseconds(700);
            }

            if (_evaluationTimer.IsEnabled)
            {
                _evaluationTimer.Stop();
            }

            _evaluationTimer.Start();
        }

        #endregion

        #region Command Handlers

        private void EvaluateCommandHandler()
        {
            var result = this.EvaluateExpression(this.textEditor.SelectedText);

            MessageBox.Show($"Expression: \r\n{this.textEditor.SelectedText} \r\n\r\nEvaluates to:\r\n{result}");
        }

        private void OpenCommandHandler()
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "Expressive Playground (.expl)|*.expl",
                Multiselect = false,
            };

            if (openDialog.ShowDialog() == true)
            {
                var xmlConfig = XElement.Load(openDialog.FileName);

                this.textEditor.Text = xmlConfig.Element("Expression")?.Value;
                var variables = xmlConfig.Element("Variables").Elements().Select(e => VariableDescriptor.FromXml(e));

                foreach (var v in variables)
                {
                    this.Variables.Add(v);
                }
            }
        }

        private void SaveCommandHandler()
        {
            var saveDialog = new SaveFileDialog
            {
                AddExtension = true,
                //DefaultExt = "*.expressive",
                Filter = "Expressive Playground (.expl)|*.expl",
                OverwritePrompt = true,
            };

            if (saveDialog.ShowDialog() == true)
            {
                XElement xmlConfig = new XElement("Playground",
                                                  new XElement("Expression", this.textEditor.Text),
                                                  new XElement("Variables", this.Variables.Select(v => v.ToXml())));

                xmlConfig.Save(saveDialog.FileName);
            }
        }

        #endregion
    }
}
