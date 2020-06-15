using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Relational
{
    internal class LessThanOrEqualExpression : BinaryExpressionBase
    {
        #region Constructors

        public LessThanOrEqualExpression(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            // Use the type of the left operand to make the comparison
            if (lhsResult is null)
            {
                return null;
            }

            var rhsResult = rightHandSide.Evaluate(variables);
            if (rhsResult is null)
            {
                return null;
            }

            return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult, this.context) <= 0;
        }

        #endregion
    }
}
