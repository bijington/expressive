using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Bitwise;
using Expressive.Expressions.Unary.Bitwise;

namespace Expressive.Operators.Bitwise
{
    internal class BitwiseNotOperator : OperatorBase
    {
        #region OperatorBase Members

        /// <inheritdoc />
        public override IEnumerable<string> Tags => new[] { "~" };

        /// <inheritdoc />
        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, Context context) => new BitwiseNotExpression(expressions[0] ?? expressions[1]);

        /// <inheritdoc />
        public override OperatorPrecedence GetPrecedence(Token previousToken) => OperatorPrecedence.BitwiseAnd;

        #endregion
    }
}
