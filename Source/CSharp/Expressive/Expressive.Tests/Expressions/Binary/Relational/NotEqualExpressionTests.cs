using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Relational;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Relational
{
    [TestClass]
    public class NotEqualExpressionTests
    {
        [TestMethod]
        public void TestBothNull()
        {
            var expression = new NotEqualExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)null),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)null),
                ExpressiveOptions.None);

            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestEqual()
        {
            var expression = new NotEqualExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)5),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)5),
                ExpressiveOptions.None);

            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestLeftNull()
        {
            var expression = new NotEqualExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)null),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)"abc"),
                ExpressiveOptions.None);

            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestNotEqual()
        {
            var expression = new NotEqualExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)5),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)2),
                ExpressiveOptions.None);

            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestRightNull()
        {
            var expression = new NotEqualExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)false),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)null),
                ExpressiveOptions.None);

            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestIntFloatEqual()
        {
            var expression = new NotEqualExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1.0),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1),
                ExpressiveOptions.None);

            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestIntFloatNotEqual()
        {
            var expression = new NotEqualExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1.001),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1),
                ExpressiveOptions.None);

            Assert.AreEqual(true, expression.Evaluate(null));
        }
    }
}
