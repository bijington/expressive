using Expressive.Expressions;

namespace Expressive.Functions.String
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

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 3, 3);

            string text = (string)parameters[0].Evaluate(Variables);
            int startIndex = (int)parameters[1].Evaluate(Variables);
            int length = (int)parameters[2].Evaluate(Variables);

            // Not very safe at present but let's see for now.
            return text.Substring(startIndex, length);
        }

        #endregion
    }
}
