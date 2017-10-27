using Expressive.Expressions;

namespace Expressive.Functions.String
{
    internal class ContainsFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name
        {
            get
            {
                return "Contains";
            }
        }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            string text = (string)parameters[0].Evaluate(Variables);
            string value = (string)parameters[1].Evaluate(Variables);

            if (value == null)
            {
                return false;
            }

            if (options.HasFlag(ExpressiveOptions.IgnoreCase))
            {
                text = text?.ToLower();
                value = value?.ToLower();
            }

            // Not very safe at present but let's see for now.
            return text?.Contains(value) == true;
        }

        #endregion
    }
}
