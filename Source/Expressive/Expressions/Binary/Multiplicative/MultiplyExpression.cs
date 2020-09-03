using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Multiplicative
{
    internal class MultiplyExpression : BinaryExpressionBase
    {
        #region Constructors

        public MultiplyExpression(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            return this.Evaluate(lhsResult, rightHandSide, variables, Numbers.Multiply);
        }

        #endregion
    }
}
