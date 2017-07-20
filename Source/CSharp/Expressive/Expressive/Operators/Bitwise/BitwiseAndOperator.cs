using Expressive.Expressions;

namespace Expressive.Operators.Bitwise
{
    internal class BitwiseAndOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags { get { return new[] { "&" }; } }

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options)
        {
            return new BinaryExpression(BinaryExpressionType.BitwiseAnd, expressions[0], expressions[1], options);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.BitwiseAnd;
        }

        #endregion
    }
}
