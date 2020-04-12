using System;
using System.Collections.Generic;

namespace Expressive.Expressions.Binary.Bitwise
{
    internal class BitwiseAndExpression : BinaryExpressionBase
    {
        #region Constructors

        public BitwiseAndExpression(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables) =>
            this.Evaluate(lhsResult, rightHandSide, variables, (l, r) => Convert.ToUInt16(l) & Convert.ToUInt16(r));

        #endregion
    }
}
