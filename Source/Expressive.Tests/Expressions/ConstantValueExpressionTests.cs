using System;
using Expressive.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Expressions
{
    [TestClass]
    public class ConstantValueExpressionTests
    {
        [TestMethod]
        public void TestBooleanValue()
        {
            var constantValueExpression = new ConstantValueExpression(true);

            Assert.AreEqual(true, constantValueExpression.Evaluate(null));
        }

        [TestMethod]
        public void TestDateTimeValue()
        {
            var now = DateTime.Now;

            var constantValueExpression = new ConstantValueExpression(now);

            Assert.AreEqual(now, constantValueExpression.Evaluate(null));
        }

        [TestMethod]
        public void TestDecimalValue()
        {
            var constantValueExpression = new ConstantValueExpression(123M);

            Assert.AreEqual(123M, constantValueExpression.Evaluate(null));
        }

        [TestMethod]
        public void TestDoubleValue()
        {
            var constantValueExpression = new ConstantValueExpression(123d);

            Assert.AreEqual(123d, constantValueExpression.Evaluate(null));
        }

        [TestMethod]
        public void TestIntegerValue()
        {
            var constantValueExpression = new ConstantValueExpression(123);

            Assert.AreEqual(123, constantValueExpression.Evaluate(null));
        }

        [TestMethod]
        public void TestLongValue()
        {
            var constantValueExpression = new ConstantValueExpression(123L);

            Assert.AreEqual(123L, constantValueExpression.Evaluate(null));
        }

        [TestMethod]
        public void TestStringValue()
        {
            var constantValueExpression = new ConstantValueExpression("123");

            Assert.AreEqual("123", constantValueExpression.Evaluate(null));
        }

        [TestMethod]
        public void TestUnsupportedValue()
        {
            var unsupportedValue = new {Name = "Shaun", Age = 99};
            var constantValueExpression = new ConstantValueExpression(unsupportedValue);

            Assert.AreEqual(unsupportedValue, constantValueExpression.Evaluate(null));
        }
    }
}
