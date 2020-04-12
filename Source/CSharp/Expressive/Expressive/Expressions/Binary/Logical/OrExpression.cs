using System;
using System.Collections.Generic;

namespace Expressive.Expressions.Binary.Logical
{
    internal class OrExpression : BinaryExpressionBase
    {
        #region Constructors

        public OrExpression(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            return this.Evaluate(lhsResult, rightHandSide, variables, (l, r) => Convert.ToBoolean(l) || Convert.ToBoolean(r));
        }

        #endregion
    }
}
