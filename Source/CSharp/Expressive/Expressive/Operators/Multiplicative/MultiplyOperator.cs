using Expressive.Expressions;

namespace Expressive.Operators.Multiplicative
{
    internal class MultiplyOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags { get { return new[] { "*", "\u00d7" }; } }

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options)
        {
            return new BinaryExpression(BinaryExpressionType.Multiply, expressions[0], expressions[1], options);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.Multiply;
        }

        #endregion
    }
}
