using Expressive.Expressions;

namespace Expressive.Operators.Logic
{
    internal class AndOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags { get { return new[] { "&&", "and" }; } }

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options)
        {
            return new BinaryExpression(BinaryExpressionType.And, expressions[0], expressions[1], options);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.And;
        }

        #endregion
    }
}
