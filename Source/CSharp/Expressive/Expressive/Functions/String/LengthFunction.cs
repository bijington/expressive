using Expressive.Expressions;

namespace Expressive.Functions.String
{
    internal class LengthFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name
        {
            get
            {
                return "Length";
            }
        }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 1, 1);

            object value = parameters[0].Evaluate(Variables);

            if (value == null) return null;

            string text = value as string;
            if (text != null)
            {
                return text.Length;
            }
            else
            {
                return value.ToString().Length;
            }
        }

        #endregion
    }
}
