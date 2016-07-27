using Expressive.Expressions;

namespace Expressive.Operators.Conditional
{
    internal class NullCoalescingOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags { get { return new[] { "??" }; } }

        public override IExpression BuildExpression(string previousToken, IExpression[] expressions)
        {
            return new BinaryExpression(BinaryExpressionType.NullCoalescing, expressions[0], expressions[1]);
        }

        public override OperatorPrecedence GetPrecedence(string previousToken)
        {
            return OperatorPrecedence.NullCoalescing;
        }

        #endregion
    }
}
