using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Multiplicative;

namespace Expressive.Operators.Multiplicative
{
    internal class ExponentOperator : OperatorBase
    {
        #region OperatorBase Members

        public override IEnumerable<string> Tags => new[] { "^", "\u2038" };

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, Context context)
        {
            return new ExponentExpression(expressions[0], expressions[1], context);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.Multiply;
        }

        #endregion
    }
}
