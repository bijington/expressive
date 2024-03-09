using System;
using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Unary.Additive;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Expressions.Unary.Additive
{
    [TestFixture]
    public class PlusExpressionTests
    {
        [Test]
        public void TestNull()
        {
            var expression = new PlusExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null));

            Assert.IsNull(expression.Evaluate(null));
        }

        [Test]
        public void TestInteger()
        {
            var expression = new PlusExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) 12));

            Assert.AreEqual(12, expression.Evaluate(null));
        }

        [Test]
        public void TestDouble()
        {
            var expression = new PlusExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) 13.5d));

            Assert.AreEqual(13.5d, expression.Evaluate(null));
        }

        [Test]
        public void TestDecimal()
        {
            var expression = new PlusExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) 12.4M));

            Assert.AreEqual(12.4M, expression.Evaluate(null));
        }

        [Test]
        public void TestString()
        {
            var expression = new PlusExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) "12"));

            Assert.AreEqual(12M, expression.Evaluate(null));
        }

        [Test]
        public void TestInvalid()
        {
            var expression = new PlusExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) DateTime.Now));

            Assert.IsNull(expression.Evaluate(null));
        }
    }
}