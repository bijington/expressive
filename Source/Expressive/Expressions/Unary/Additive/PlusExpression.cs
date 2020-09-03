using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Unary.Additive
{
    internal class PlusExpression : UnaryExpressionBase
    {
        #region Constructors

        public PlusExpression(IExpression expression) : base(expression)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        public override object Evaluate(IDictionary<string, object> variables) =>
            Numbers.Add(0, this.expression.Evaluate(variables));

        #endregion
    }
}
