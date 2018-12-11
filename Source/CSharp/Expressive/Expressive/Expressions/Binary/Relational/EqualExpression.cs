using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Relational
{
    internal class EqualExpression : BinaryExpressionBase
    {
        #region Constructors

        public EqualExpression(IExpression lhs, IExpression rhs, ExpressiveOptions options) : base(lhs, rhs, options)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            // Use the type of the left operand to make the comparison
            if (lhsResult == null)
            {
                return rightHandSide.Evaluate(variables) == null;
            }

            var rhsResult = rightHandSide.Evaluate(variables);

            // If we got here then the lhsResult is not null.
            if (rhsResult == null)
            {
                return false;
            }

            return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult, this.options.HasFlag(ExpressiveOptions.IgnoreCase)) == 0;
        }

        #endregion
    }
}
