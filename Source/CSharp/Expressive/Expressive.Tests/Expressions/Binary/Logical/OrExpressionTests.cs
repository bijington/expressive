using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Logical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Logical
{
    [TestClass]
    public class OrExpressionTests
    {
        [TestMethod]
        public void TestBothTrueEvaluate()
        {
            var expression = new OrExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)true),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)true),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestLeftTrueEvaluate()
        {
            var expression = new OrExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)true),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)false),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestNeitherTrueEvaluate()
        {
            var expression = new OrExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)false),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)false),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestRightTrueEvaluate()
        {
            var expression = new OrExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)false),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)true),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(true, expression.Evaluate(null));
        }
    }
}
