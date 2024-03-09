using Expressive.Expressions;
using Expressive.Operators;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expressive.Tests.Operators
{
    [TestFixture]
    public abstract class OperatorBaseTests
    {
        internal abstract IOperator Operator { get; }

        protected abstract Type ExpectedExpressionType { get; }

        internal abstract OperatorPrecedence ExpectedOperatorPrecedence { get; }

#pragma warning disable CA1819 // Properties should not return arrays
        protected abstract string[] ExpectedTags { get; }
#pragma warning restore CA1819 // Properties should not return arrays

        [Test]
        public void TestTags()
        {
            var operatorTags = this.Operator.Tags.ToArray();

            Assert.AreEqual(this.ExpectedTags.Length, operatorTags.Length);

            for (var i = 0; i < this.ExpectedTags.Length; i++)
            {
                Assert.AreEqual(this.ExpectedTags[i], operatorTags[i]);
            }
        }

        [Test]
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
                new Context(ExpressiveOptions.None));

            Assert.That(expression, Is.InstanceOf(this.ExpectedExpressionType));
        }

        [Test]
        public void TestCanGetCaptiveTokens()
        {
            var op = this.Operator;

            Assert.IsTrue(op.CanGetCaptiveTokens(new Token("1", 0), new Token("+", 1), new Queue<Token>()));
        }

        [Test]
        public virtual void TestGetCaptiveTokens()
        {
            var op = this.Operator;

            var token = new Token("+", 1);

            Assert.AreEqual(token, op.GetCaptiveTokens(new Token("1", 0), token, new Queue<Token>()).Single());
        }

        [Test]
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

        [Test]
        public void TestGetPrecedence()
        {
            var op = this.Operator;

            Assert.AreEqual(this.ExpectedOperatorPrecedence, op.GetPrecedence(null));
        }
    }
}
