using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Relational
{
    internal class LessThanOrEqualExpression : BinaryExpressionBase
    {
        #region Constructors

        public LessThanOrEqualExpression(IExpression lhs, IExpression rhs, ExpressiveOptions options) : base(lhs, rhs, options)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            // Use the type of the left operand to make the comparison
            if (lhsResult == null)
            {
                return null;
            }

            var rhsResult = rightHandSide.Evaluate(variables);
            if (rhsResult == null)
            {
                return null;
            }

            return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult, this.options.HasFlag(ExpressiveOptions.IgnoreCase)) <= 0;
        }

        #endregion
    }
}
