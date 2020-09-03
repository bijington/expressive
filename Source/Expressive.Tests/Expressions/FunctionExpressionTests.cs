using System.Collections.Generic;
using Expressive.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions
{
    [TestClass]
    public class FunctionExpressionTests
    {
        [TestMethod]
        public void TestEvaluate()
        {
            var expression = new FunctionExpression(
                "testFunc",
                (p,a) => 123,
                new []{ Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1) });

            Assert.AreEqual(123, expression.Evaluate(null));
        }
    }
}
