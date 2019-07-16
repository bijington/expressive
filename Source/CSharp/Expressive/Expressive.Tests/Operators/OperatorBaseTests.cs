using Expressive.Expressions;
using Expressive.Operators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expressive.Tests.Operators
{
    [TestClass]
    public abstract class OperatorBaseTests
    {
        internal abstract IOperator Operator { get; }

        protected abstract Type ExpectedExpressionType { get; }

        internal abstract OperatorPrecedence ExpectedOperatorPrecedence { get; }

        protected abstract string[] ExpectedTags { get; }

        [TestMethod]
        public void TestTags()
        {
            var op = this.Operator;

            Assert.AreEqual(this.ExpectedTags.Length, op.Tags.Length);

            for (var i = 0; i < this.ExpectedTags.Length; i++)
            {
                Assert.AreEqual(this.ExpectedTags[i], op.Tags[i]);
            }
        }

        [TestMethod]
        public void TestBuildExpression()
        {
            var op = this.Operator;

            var expression = op.BuildExpression(
                new Token("1", 1),
                new[]
                {
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null),
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
                },
                ExpressiveOptions.None);

            Assert.IsInstanceOfType(expression, this.ExpectedExpressionType);
        }

        [TestMethod]
        public void TestCanGetCaptiveTokens()
        {
            var op = this.Operator;

            Assert.IsTrue(op.CanGetCaptiveTokens(new Token("1", 0), new Token("+", 1), new Queue<Token>()));
        }

        [TestMethod]
        public virtual void TestGetCaptiveTokens()
        {
            var op = this.Operator;

            var token = new Token("+", 1);

            Assert.AreEqual(token, op.GetCaptiveTokens(new Token("1", 0), token, new Queue<Token>()).Single());
        }

        [TestMethod]
        public virtual void TestGetInnerCaptiveTokens()
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

            Assert.AreEqual(0, op.GetInnerCaptiveTokens(tokens).Length);
        }

        [TestMethod]
        public void TestGetPrecedence()
        {
            var op = this.Operator;

            Assert.AreEqual(this.ExpectedOperatorPrecedence, op.GetPrecedence(null));
        }
    }
}
