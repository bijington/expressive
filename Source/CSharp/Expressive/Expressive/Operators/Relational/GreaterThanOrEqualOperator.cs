using Expressive.Expressions;

namespace Expressive.Operators.Relational
{
    internal class GreaterThanOrEqualOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags { get { return new[] { ">=" }; } }

        public override IExpression BuildExpression(string previousToken, IExpression[] expressions)
        {
            return new BinaryExpression(BinaryExpressionType.GreaterThanOrEqual, expressions[0], expressions[1]);
        }

        public override OperatorPrecedence GetPrecedence(string previousToken)
        {
            return OperatorPrecedence.GreaterThanOrEqual;
        }

        #endregion
    }
}
