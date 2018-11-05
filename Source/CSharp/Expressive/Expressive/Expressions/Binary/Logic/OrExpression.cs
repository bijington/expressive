using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expressive.Expressions.Binary.Logic
{
    internal class OrExpression : BinaryExpressionBase
    {
        #region Constructors

        public OrExpression(IExpression lhs, IExpression rhs, ExpressiveOptions options) : base(lhs, rhs, options)
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
