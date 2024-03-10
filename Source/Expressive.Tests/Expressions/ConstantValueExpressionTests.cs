using System;
using Expressive.Expressions;
using NUnit.Framework;

namespace Expressive.Tests.Expressions
{
    [TestFixture]
    public class ConstantValueExpressionTests
    {
        [Test]
        public void TestBooleanValue()
        {
            var constantValueExpression = new ConstantValueExpression(true);

            Assert.AreEqual(true, constantValueExpression.Evaluate(null));
        }

        [Test]
        public void TestDateTimeValue()
        {
            var now = DateTime.Now;

            var constantValueExpression = new ConstantValueExpression(now);

            Assert.AreEqual(now, constantValueExpression.Evaluate(null));
        }

        [Test]
        public void TestDecimalValue()
        {
            var constantValueExpression = new ConstantValueExpression(123M);

            Assert.AreEqual(123M, constantValueExpression.Evaluate(null));
        }

        [Test]
        public void TestDoubleValue()
        {
            var constantValueExpression = new ConstantValueExpression(123d);

            Assert.AreEqual(123d, constantValueExpression.Evaluate(null));
        }

        [Test]
        public void TestIntegerValue()
        {
            var constantValueExpression = new ConstantValueExpression(123);

            Assert.AreEqual(123, constantValueExpression.Evaluate(null));
        }

        [Test]
        public void TestLongValue()
        {
            var constantValueExpression = new ConstantValueExpression(123L);

            Assert.AreEqual(123L, constantValueExpression.Evaluate(null));
        }

        [Test]
        public void TestStringValue()
        {
            var constantValueExpression = new ConstantValueExpression("123");

            Assert.AreEqual("123", constantValueExpression.Evaluate(null));
        }

        [Test]
        public void TestUnsupportedValue()
        {
            var unsupportedValue = new {Name = "Shaun", Age = 99};
            var constantValueExpression = new ConstantValueExpression(unsupportedValue);

            Assert.AreEqual(unsupportedValue, constantValueExpression.Evaluate(null));
        }
    }
}
