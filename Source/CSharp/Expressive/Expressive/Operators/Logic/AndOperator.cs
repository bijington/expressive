using Expressive.Expressions;

namespace Expressive.Operators.Logic
{
    internal class AndOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags { get { return new[] { "&&", "and" }; } }

        public override IExpression BuildExpression(string previousToken, IExpression[] expressions)
        {
            return new BinaryExpression(BinaryExpressionType.And, expressions[0], expressions[1]);
        }

        public override OperatorPrecedence GetPrecedence(string previousToken)
        {
            return OperatorPrecedence.And;
        }

        #endregion
    }
}
