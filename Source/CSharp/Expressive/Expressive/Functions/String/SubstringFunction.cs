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

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 3, 3);

            object value = parameters[0].Evaluate(Variables);

            if (value == null)
            {
                return null;
            }

            // Safely handle the text input, if not text then call ToString.
            string text = null;
            if (value is string)
            {
                text = (string)value;
            }
            else
            {
                text = value.ToString();
            }

            int startIndex = (int)parameters[1].Evaluate(Variables);
            int length = (int)parameters[2].Evaluate(Variables);
            
            return text?.Substring(startIndex, length);
        }

        #endregion
    }
}
