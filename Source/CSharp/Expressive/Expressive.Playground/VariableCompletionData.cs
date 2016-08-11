using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Playground
{
    internal sealed class VariableCompletionData : ICompletionData
    {
        public VariableCompletionData(string variableName)
        {
            this.Text = variableName;
        }

        public System.Windows.Media.ImageSource Image
        {
            get { return null; }
        }

        public string Text { get; private set; }

        // Use this property if you want to show a fancy UIElement in the list.
        public object Content
        {
            get { return this.Text; }
        }

        public object Description
        {
            get { return $"Variable {this.Text}"; }
        }

        public double Priority
        {
            get
            {
                return 0d;
            }
        }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment.Offset - 1, completionSegment.Length + 1, $"[{this.Text}]");
        }
    }
}
