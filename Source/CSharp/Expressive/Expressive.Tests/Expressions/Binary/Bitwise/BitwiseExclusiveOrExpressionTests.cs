using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Bitwise;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Bitwise
{
    [TestClass]
    public class BitwiseExclusiveOrExpressionTests
    {
        [TestMethod]
        public void TestEvaluate()
        {
            var expression = new BitwiseExclusiveOrExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1111),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)0001),
                ExpressiveOptions.None);

            Assert.AreEqual(1110, expression.Evaluate(null));
        }
    }
}
