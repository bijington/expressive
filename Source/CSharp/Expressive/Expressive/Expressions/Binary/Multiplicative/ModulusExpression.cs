using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Multiplicative
{
    internal class ModulusExpression : BinaryExpressionBase
    {
        #region Constructors

        public ModulusExpression(IExpression lhs, IExpression rhs, ExpressiveOptions options) : base(lhs, rhs, options)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            return this.Evaluate(lhsResult, rightHandSide, variables, Numbers.Modulus);
        }

        #endregion
    }
}
