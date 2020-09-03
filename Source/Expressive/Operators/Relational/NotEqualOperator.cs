using Expressive.Expressions;
using Expressive.Expressions.Binary.Relational;

namespace Expressive.Operators.Relational
{
    internal class NotEqualOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags => new[] { "!=", "<>" };

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, Context context)
        {
            return new NotEqualExpression(expressions[0], expressions[1], context);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.NotEqual;
        }

        #endregion
    }
}
