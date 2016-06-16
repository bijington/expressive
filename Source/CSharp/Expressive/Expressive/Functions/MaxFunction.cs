using Expressive.Expressions;
using Expressive.Helpers;

namespace Expressive.Functions
{
    internal class MaxFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Max"; } }

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            return Numbers.Max(parameters[0].Evaluate(Variables), parameters[1].Evaluate(Variables));
        }

        #endregion
    }
}
