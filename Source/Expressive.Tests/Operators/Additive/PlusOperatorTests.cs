using Expressive.Expressions.Binary.Additive;
using Expressive.Expressions.Unary.Additive;
using Expressive.Operators;
using Expressive.Operators.Additive;
using NUnit.Framework;
using System;

namespace Expressive.Tests.Operators.Additive
{
    [TestFixture]
    public class PlusOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new PlusOperator();

        protected override Type ExpectedExpressionType => typeof(AddExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.UnaryPlus;

        protected override string[] ExpectedTags => new[] { "+" };

        [Test]
        public override void TestGetInnerCaptiveTokens()
        {
            var op = this.Operator;

            var tokens = new[]
            {
                new Token("+", 1),
                new Token("(", 2),
                new Token("1", 3),
                new Token("-", 4),
                new Token("4", 5),
                new Token(")", 6),
            };

            Assert.AreEqual(tokens.Length - 1, op.GetInnerCaptiveTokens(tokens).Length);
        }

        #endregion

        [Test]
        public void TestBuildExpressionForUnaryWithLeftHandExpression()
        {
            var op = new PlusOperator();

            var expression = op.BuildExpression(
                new Token("+", 1),
                new[]
                {
                    MockExpression.ThatEvaluatesTo(null),
                    MockExpression.ThatEvaluatesTo(null)
                },
                new Context(ExpressiveOptions.None));

            Assert.That(expression, Is.InstanceOf(typeof(PlusExpression)));
        }

        [Test]
        public void TestBuildExpressionForUnaryWithRightHandExpression()
        {
            var op = new PlusOperator();

            var expression = op.BuildExpression(
                new Token("+", 1),
                new[]
                {
                    null,
                    MockExpression.ThatEvaluatesTo(null)
                },
                new Context(ExpressiveOptions.None));

            Assert.That(expression, Is.InstanceOf(typeof(PlusExpression)));
        }

        [Test]
        public void TestGetPrecedenceForUnary()
        {
            var op = new PlusOperator();

            Assert.AreEqual(OperatorPrecedence.UnaryPlus, op.GetPrecedence(null));
        }

        [Test]
        public void TestGetPrecedenceForBinary()
        {
            var op = new PlusOperator();

            Assert.AreEqual(OperatorPrecedence.Add, op.GetPrecedence(new Token("1", 1)));
        }
    }
}
