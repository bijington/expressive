using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Relational
{
    internal class NotEqualExpression : BinaryExpressionBase
    {
        #region Constructors

        public NotEqualExpression(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            // Use the type of the left operand to make the comparison
            if (lhsResult is null)
            {
                return rightHandSide.Evaluate(variables) != null;
            }

            var rhsResult = rightHandSide.Evaluate(variables);

            // If we got here then the lhsResult is not null.
            if (rhsResult is null)
            {
                return true;
            }

            return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult, this.context) != 0;
        }

        #endregion
    }
}
