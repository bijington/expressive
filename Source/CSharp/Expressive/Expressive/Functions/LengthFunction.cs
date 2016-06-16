using Expressive.Expressions;

namespace Expressive.Functions
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

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 1, 1);

            string text = (string)parameters[0].Evaluate(Variables);

            // Not very safe at present but let's see for now.
            return text.Length;
        }

        #endregion
    }
}
