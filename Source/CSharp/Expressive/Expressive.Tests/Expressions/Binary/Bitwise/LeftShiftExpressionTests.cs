using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Bitwise;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Bitwise
{
    [TestClass]
    public class LeftShiftExpressionTests
    {
        [TestMethod]
        public void TestEvaluate()
        {
            var expression = new LeftShiftExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)0x1001),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)0x0001),
                ExpressiveOptions.None);

            Assert.AreEqual(0x1001 << 0x0001, expression.Evaluate(null));
        }
    }
}
