using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Snippets;
using System;

namespace Expressive.Playground
{
    internal sealed class FunctionCompletionData : ICompletionData
    {
        private VariableType[] _parameterTypes;

        public FunctionCompletionData(string functionName, string description, params VariableType[] parameterTypes)
        {
            this.Text = functionName;
            this.Description = description;
            _parameterTypes = parameterTypes;
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

        public object Description { get; private set; }

        public double Priority
        {
            get
            {
                return 0d;
            }
        }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            // Clear down the first lot.
            textArea.Document.Replace(completionSegment.Offset - 1, completionSegment.Length + 1, "");

            var loopCounter = new SnippetReplaceableTextElement();

            Snippet snippet = new Snippet
            {
                Elements = {
                    new SnippetTextElement { Text = $"{this.Text}(" },
                }
            };

            if (_parameterTypes != null)
            {
                bool addComma = false;

                foreach (var type in _parameterTypes)
                {
                    if (addComma)
                    {
                        snippet.Elements.Add(new SnippetTextElement { Text = ", " });
                    }

                    string prefix = "";
                    string text = "a";
                    string suffix = "";

                    switch (type)
                    {
                        case VariableType.Number:
                            text = "0";
                            break;
                        case VariableType.String:
                            prefix = suffix = "'";
                            break;
                        case VariableType.Date:
                            prefix = suffix = "#";
                            text = $"{DateTime.Now}";
                            break;
                        case VariableType.Boolean:
                            text = "true";
                            break;
                        case VariableType.Null:
                            text = "null";
                            break;
                    }

                    if (!string.IsNullOrWhiteSpace(prefix))
                    {
                        snippet.Elements.Add(new SnippetTextElement { Text = prefix });
                    }

                    snippet.Elements.Add(new SnippetReplaceableTextElement { Text = text });

                    if (!string.IsNullOrWhiteSpace(suffix))
                    {
                        snippet.Elements.Add(new SnippetTextElement { Text = suffix });
                    }

                    addComma = true;
                }
            }

            snippet.Elements.Add(new SnippetTextElement { Text = ")" });

            snippet.Insert(textArea);
        }
    }
}
