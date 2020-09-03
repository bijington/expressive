using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Bitwise;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Bitwise
{
    [TestClass]
    public class BitwiseOrExpressionTests
    {
        [TestMethod]
        public void TestEvaluate()
        {
            var expression = new BitwiseOrExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1001),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)0001),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(1001, expression.Evaluate(null));
        }
    }
}
