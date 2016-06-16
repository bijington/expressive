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

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 3, 3);

            object value = parameters[0].Evaluate(Variables);

            string text = null;
            if (value is string)
            {
                text = (string)value;
            }
            else
            {
                text = value.ToString();
            }

            int totalLength = (int)parameters[1].Evaluate(Variables);
            char character = (char)((string)parameters[2].Evaluate(Variables))[0];

            // Not very safe at present but let's see for now.
            return text.PadLeft(totalLength, character);
        }

        #endregion
    }
}
