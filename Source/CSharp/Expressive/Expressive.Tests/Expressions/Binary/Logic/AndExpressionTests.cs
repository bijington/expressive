using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Logic
{
    [TestClass]
    public class AndExpressionTests
    {
        [TestMethod]
        public void TestBothTrueEvaluate()
        {
            var expression = new AndExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) true),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) true),
                ExpressiveOptions.None);

            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestLeftTrueEvaluate()
        {
            var expression = new AndExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) true),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) false),
                ExpressiveOptions.None);

            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestNeitherTrueEvaluate()
        {
            var expression = new AndExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) false),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) false),
                ExpressiveOptions.None);

            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestRightTrueEvaluate()
        {
            var expression = new AndExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) false),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) true),
                ExpressiveOptions.None);

            Assert.AreEqual(false, expression.Evaluate(null));
        }
    }
}