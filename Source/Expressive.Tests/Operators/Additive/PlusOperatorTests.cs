using Expressive.Expressions;
using Expressive.Expressions.Binary.Additive;
using Expressive.Expressions.Unary.Additive;
using Expressive.Operators;
using Expressive.Operators.Additive;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Expressive.Tests.Operators.Additive
{
    [TestClass]
    public class PlusOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new PlusOperator();

        protected override Type ExpectedExpressionType => typeof(AddExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.UnaryPlus;

        protected override string[] ExpectedTags => new[] { "+" };

        [TestMethod]
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

        [TestMethod]
        public void TestBuildExpressionForUnaryWithLeftHandExpression()
        {
            var op = new PlusOperator();

            var expression = op.BuildExpression(
                new Token("+", 1),
                new[]
                {
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null),
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
                },
                new Context(ExpressiveOptions.None));

            Assert.IsInstanceOfType(expression, typeof(PlusExpression));
        }

        [TestMethod]
        public void TestBuildExpressionForUnaryWithRightHandExpression()
        {
            var op = new PlusOperator();

            var expression = op.BuildExpression(
                new Token("+", 1),
                new[]
                {
                    null,
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
                },
                new Context(ExpressiveOptions.None));

            Assert.IsInstanceOfType(expression, typeof(PlusExpression));
        }

        [TestMethod]
        public void TestGetPrecedenceForUnary()
        {
            var op = new PlusOperator();

            Assert.AreEqual(OperatorPrecedence.UnaryPlus, op.GetPrecedence(null));
        }

        [TestMethod]
        public void TestGetPrecedenceForBinary()
        {
            var op = new PlusOperator();

            Assert.AreEqual(OperatorPrecedence.Add, op.GetPrecedence(new Token("1", 1)));
        }
    }
}
