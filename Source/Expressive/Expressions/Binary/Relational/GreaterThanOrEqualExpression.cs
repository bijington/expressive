using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Relational
{
    internal class GreaterThanOrEqualExpression : BinaryExpressionBase
    {
        #region Constructors

        public GreaterThanOrEqualExpression(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            object rhsResult = rightHandSide.Evaluate(variables);

            if (Context.Options.HasFlag(ExpressiveOptions.Strict))
            {
                if (lhsResult is null && rhsResult is null)
                {
                    return true;
                }

                if (lhsResult is null || rhsResult is null)
                {
                    return false;
                }
            }

            return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult, this.Context) >= 0;
        }

        #endregion
    }
}
