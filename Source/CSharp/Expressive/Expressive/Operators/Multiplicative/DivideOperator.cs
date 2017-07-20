using Expressive.Expressions;

namespace Expressive.Operators.Multiplicative
{
    internal class DivideOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags { get { return new[] { "/", "\u00f7" }; } }

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options)
        {
            return new BinaryExpression(BinaryExpressionType.Divide, expressions[0], expressions[1], options);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.Divide;
        }

        #endregion
    }
}
