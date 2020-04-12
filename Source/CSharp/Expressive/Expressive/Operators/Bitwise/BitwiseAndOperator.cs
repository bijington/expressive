using Expressive.Expressions;
using Expressive.Expressions.Binary.Bitwise;

namespace Expressive.Operators.Bitwise
{
    internal class BitwiseAndOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags => new[] { "&" };

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, Context context)
        {
            return new BitwiseAndExpression(expressions[0], expressions[1], context);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.BitwiseAnd;
        }

        #endregion
    }
}
