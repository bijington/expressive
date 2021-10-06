using Expressive.Expressions;

namespace Expressive.Functions.Statistical
{
    internal class AverageFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "Average";

        public override object Evaluate(IExpression[] parameters, Context context)
        {
            this.ValidateParameterCount(parameters, -1, 1);

            return MeanFunction.Evaluate(parameters, Variables);
        }

        #endregion
    }
}
