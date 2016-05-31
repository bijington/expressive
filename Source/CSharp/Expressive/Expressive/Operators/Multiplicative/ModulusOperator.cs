using Expressive.Expressions;

namespace Expressive.Operators.Multiplicative
{
    internal class ModulusOperator : OperatorBase
    {
        #region OperatorBase Members

        public override string[] Tags { get { return new[] { "%", "mod" }; } }

        public override IExpression BuildExpression(string previousToken, IExpression[] expressions)
        {
            return new BinaryExpression(BinaryExpressionType.Modulus, expressions[0], expressions[1]);
        }

        public override OperatorPrecedence GetPrecedence(string previousToken)
        {
            return OperatorPrecedence.Modulus;
        }

        #endregion
    }
}
