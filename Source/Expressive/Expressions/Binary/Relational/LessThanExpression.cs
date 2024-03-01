using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Relational
{
    internal class LessThanExpression : BinaryExpressionBase
    {
        #region Constructors

        public LessThanExpression(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            object rhsResult = null;

            if (Context.Options.HasFlag(ExpressiveOptions.Strict))
            {
                if (lhsResult is null)
                {
                    return false;
                }

                rhsResult = rightHandSide.Evaluate(variables);

                // If we got here then the lhsResult is not null.
                if (rhsResult is null)
                {
                    return false;
                }
            }

            return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult ?? rightHandSide.Evaluate(variables), this.Context) < 0;
        }

        #endregion
    }
}
