using System.Collections.Generic;

namespace Expressive.Expressions.Binary.Conditional
{
    internal class NullCoalescingExpression : BinaryExpressionBase
    {
        #region Constructors

        public NullCoalescingExpression(IExpression lhs, IExpression rhs, ExpressiveOptions options) : base(lhs, rhs, options)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            return this.Evaluate(lhsResult, rightHandSide, variables, (l, r) => l ?? r);
        }

        #endregion
    }
}
