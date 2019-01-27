using Expressive.Expressions;
using Expressive.Expressions.Binary.Logical;

namespace Expressive.Operators.Logical
{
    internal class OrOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags => new[] { "||", "or" };

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options)
        {
            return new OrExpression(expressions[0], expressions[1], options);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.Or;
        }

        #endregion
    }
}
