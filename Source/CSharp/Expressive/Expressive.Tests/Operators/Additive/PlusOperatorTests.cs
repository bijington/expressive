using System.Collections.Generic;
using System.Linq;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Additive;
using Expressive.Expressions.Unary.Additive;
using Expressive.Operators;
using Expressive.Operators.Additive;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Operators.Additive
{
    [TestClass]
    public class PlusOperatorTests
    {
        [TestMethod]
        public void TestTags()
        {
            var op = new PlusOperator();

            Assert.AreEqual(1, op.Tags.Length);
            Assert.AreEqual("+", op.Tags.Single());
        }

        [TestMethod]
        public void TestBuildExpressionForUnaryWithLeftHandExpression()
        {
            var op = new PlusOperator();

            var expression = op.BuildExpression(
                new Token("+", 1),
                new []
                {
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null),
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
                },
                ExpressiveOptions.None);

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
                ExpressiveOptions.None);

            Assert.IsInstanceOfType(expression, typeof(PlusExpression));
        }

        [TestMethod]
        public void TestBuildExpressionForBinary()
        {
            var op = new PlusOperator();

            var expression = op.BuildExpression(
                new Token("1", 1),
                new[]
                {
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null),
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
                },
                ExpressiveOptions.None);

            Assert.IsInstanceOfType(expression, typeof(AddExpression));
        }

        [TestMethod]
        public void TestCanGetCaptiveTokens()
        {
            var op = new PlusOperator();

            Assert.IsTrue(op.CanGetCaptiveTokens(new Token("1", 0), new Token("+", 1), new Queue<Token>()));
        }

        [TestMethod]
        public void TestGetCaptiveTokens()
        {
            var op = new PlusOperator();

            var token = new Token("+", 1);

            Assert.AreEqual(token, op.GetCaptiveTokens(new Token("1", 0), token, new Queue<Token>()).Single());
        }

        [TestMethod]
        public void TestGetInnerCaptiveTokens()
        {
            var op = new PlusOperator();

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
