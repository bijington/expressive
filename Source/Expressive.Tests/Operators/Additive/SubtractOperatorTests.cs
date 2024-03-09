using Expressive.Expressions;
using Expressive.Expressions.Binary.Additive;
using Expressive.Expressions.Unary.Additive;
using Expressive.Operators;
using Expressive.Operators.Additive;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;

namespace Expressive.Tests.Operators.Additive
{
    [TestFixture]
    public class SubtractOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new SubtractOperator();

        protected override Type ExpectedExpressionType => typeof(SubtractExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.UnaryMinus;

        protected override string[] ExpectedTags => new[] { "-", "\u2212" };

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
            var op = new SubtractOperator();

            var expression = op.BuildExpression(
                new Token("+", 1),
                new[]
                {
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null),
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
                },
                new Context(ExpressiveOptions.None));

            Assert.That(expression, Is.InstanceOf(typeof(MinusExpression)));
        }

        [Test]
        public void TestBuildExpressionForUnaryWithRightHandExpression()
        {
            var op = new SubtractOperator();

            var expression = op.BuildExpression(
                new Token("+", 1),
                new[]
                {
                    null,
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
                },
                new Context(ExpressiveOptions.None));

            Assert.That(expression, Is.InstanceOf(typeof(MinusExpression)));
        }

        [Test]
        public void TestGetPrecedenceForUnary()
        {
            var op = new SubtractOperator();

            Assert.AreEqual(OperatorPrecedence.UnaryMinus, op.GetPrecedence(null));
        }

        [Test]
        public void TestGetPrecedenceForBinary()
        {
            var op = new SubtractOperator();

            Assert.AreEqual(OperatorPrecedence.Subtract, op.GetPrecedence(new Token("1", 1)));
        }
    }
}
