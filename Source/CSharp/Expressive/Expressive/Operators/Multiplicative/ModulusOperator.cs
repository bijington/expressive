using Expressive.Expressions;

namespace Expressive.Operators.Multiplicative
{
    internal class ModulusOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags { get { return new[] { "%", "mod" }; } }

        public override IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options)
        {
            return new BinaryExpression(BinaryExpressionType.Modulus, expressions[0], expressions[1], options);
        }

        public override OperatorPrecedence GetPrecedence(Token previousToken)
        {
            return OperatorPrecedence.Modulus;
        }

        #endregion
    }
}
