using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Additive
{
    internal class SubtractExpression : BinaryExpressionBase
    {
        #region Constructors

        public SubtractExpression(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables) =>
            this.Evaluate(lhsResult, rightHandSide, variables, Numbers.Subtract);

        #endregion
    }
}
