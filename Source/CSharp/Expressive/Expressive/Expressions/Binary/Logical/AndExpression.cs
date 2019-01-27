using System;
using System.Collections.Generic;

namespace Expressive.Expressions.Binary.Logical
{
    internal class AndExpression : BinaryExpressionBase
    {
        #region Constructors

        public AndExpression(IExpression lhs, IExpression rhs, ExpressiveOptions options) : base(lhs, rhs, options)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            return this.Evaluate(lhsResult, rightHandSide, variables, (l, r) => Convert.ToBoolean(l) && Convert.ToBoolean(r));
        }

        #endregion
    }
}
