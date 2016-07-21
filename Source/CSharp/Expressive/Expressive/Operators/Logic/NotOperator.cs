using Expressive.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Expressive.Operators.Logic
{
    internal class NotOperator : OperatorBase
    {
        #region IOperator Members

        public override string[] Tags { get { return new[] { "!", "not" }; } }

        public override IExpression BuildExpression(string previousToken, IExpression[] expressions)
        {
            return new UnaryExpression(UnaryExpressionType.Not, expressions[0] ?? expressions[1]);
        }

        public override OperatorPrecedence GetPrecedence(string previousToken)
        {
            return OperatorPrecedence.Not;
        }

        #endregion
    }
}
