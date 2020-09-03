using Expressive.Expressions;
using Expressive.Expressions.Binary.Conditional;

namespace Expressive.Operators.Conditional
{
    internal class NullCoalescingOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags => new[] { "??" };

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, Context context)
        {
            return new NullCoalescingExpression(expressions[0], expressions[1], context);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.NullCoalescing;
        }

        #endregion
    }
}
