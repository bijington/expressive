using System;
using System.Collections.Generic;

namespace Expressive.Expressions.Binary.Bitwise
{
    internal class RightShiftExpression : BinaryExpressionBase
    {
        #region Constructors

        public RightShiftExpression(IExpression lhs, IExpression rhs, ExpressiveOptions options) : base(lhs, rhs, options)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables) =>
            this.Evaluate(lhsResult, rightHandSide, variables, (l, r) => Convert.ToUInt16(l) >> Convert.ToUInt16(r));

        #endregion
    }
}
