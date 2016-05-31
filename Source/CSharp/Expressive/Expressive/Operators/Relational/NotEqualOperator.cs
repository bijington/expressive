using Expressive.Expressions;

namespace Expressive.Operators.Relational
{
    internal class NotEqualOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags { get { return new[] { "!=", "<>" }; } }

        public override IExpression BuildExpression(string previousToken, IExpression[] expressions)
        {
            return new BinaryExpression(BinaryExpressionType.NotEqual, expressions[0], expressions[1]);
        }

        public override OperatorPrecedence GetPrecedence(string previousToken)
        {
            return OperatorPrecedence.NotEqual;
        }

        #endregion
    }
}
