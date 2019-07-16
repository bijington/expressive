using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Conditional;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Conditional
{
    [TestClass]
    public class NullCoalescingExpressionTests
    {
        [TestMethod]
        public void TestNotNullEvaluate()
        {
            var expression = new NullCoalescingExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)"Non null"),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)"Never used"),
                ExpressiveOptions.None);

            Assert.AreEqual("Non null", expression.Evaluate(null));
        }

        [TestMethod]
        public void TestNullEvaluate()
        {
            var expression = new NullCoalescingExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)null),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)"Now used"),
                ExpressiveOptions.None);

            Assert.AreEqual("Now used", expression.Evaluate(null));
        }
    }
}
