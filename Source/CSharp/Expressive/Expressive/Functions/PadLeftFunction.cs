using Expressive.Expressions;

namespace Expressive.Functions
{
    internal class PadLeftFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name
        {
            get
            {
                return "PadLeft";
            }
        }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 3, 3);

            object value = participants[0].Evaluate(Arguments);

            string text = null;
            if (value is string)
            {
                text = (string)value;
            }
            else
            {
                text = value.ToString();
            }

            int totalLength = (int)participants[1].Evaluate(Arguments);
            char character = (char)((string)participants[2].Evaluate(Arguments))[0];

            // Not very safe at present but let's see for now.
            return text.PadLeft(totalLength, character);
        }

        #endregion
    }
}
