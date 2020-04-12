using Expressive.Expressions;

namespace Expressive.Operators.Grouping
{
    internal class ParenthesisCloseOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags => new[] { ")" };

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, Context context)
        {
            return expressions[0] ?? expressions[1];
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.ParenthesisClose;
        }

        #endregion
    }
}
