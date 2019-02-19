using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Additive
{
    internal class AddExpression : BinaryExpressionBase
    {
        #region Constructors

        public AddExpression(IExpression lhs, IExpression rhs, ExpressiveOptions options) : base(lhs, rhs, options)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            if (lhsResult is string stringValue)
            {
                return stringValue + rightHandSide.Evaluate(variables);
            }

            return this.Evaluate(lhsResult, rightHandSide, variables, Numbers.Add);
        }

        #endregion
    }
}
