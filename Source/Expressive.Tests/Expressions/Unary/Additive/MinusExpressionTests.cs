using System;
using Expressive.Expressions.Unary.Additive;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Unary.Additive
{
    [TestFixture]
    public class MinusExpressionTests
    {
        [Test]
        public void TestNull()
        {
            var expression = new MinusExpression(MockExpression.ThatEvaluatesTo(null));

            Assert.IsNull(expression.Evaluate(null));
        }

        [Test]
        public void TestInteger()
        {
            var expression = new MinusExpression(MockExpression.ThatEvaluatesTo(12));

            Assert.AreEqual(-12, expression.Evaluate(null));
        }

        [Test]
        public void TestDouble()
        {
            var expression = new MinusExpression(MockExpression.ThatEvaluatesTo(13.5d));

            Assert.AreEqual(-13.5d, expression.Evaluate(null));
        }

        [Test]
        public void TestDecimal()
        {
            var expression = new MinusExpression(MockExpression.ThatEvaluatesTo(12.4M));

            Assert.AreEqual(-12.4M, expression.Evaluate(null));
        }

        [Test]
        public void TestString()
        {
            var expression = new MinusExpression(MockExpression.ThatEvaluatesTo("12"));

            Assert.AreEqual(-12M, expression.Evaluate(null));
        }

        [Test]
        public void TestInvalid()
        {
            var expression = new MinusExpression(MockExpression.ThatEvaluatesTo(DateTime.Now));

            Assert.IsNull(expression.Evaluate(null));
        }
    }
}
