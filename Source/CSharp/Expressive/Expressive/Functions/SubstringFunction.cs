using Expressive.Expressions;

namespace Expressive.Functions
{
    internal class SubstringFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name
        {
            get
            {
                return "Substring";
            }
        }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 3, 3);

            string text = (string)participants[0].Evaluate(Arguments);
            int startIndex = (int)participants[1].Evaluate(Arguments);
            int length = (int)participants[2].Evaluate(Arguments);

            // Not very safe at present but let's see for now.
            return text.Substring(startIndex, length);
        }

        #endregion
    }
}
