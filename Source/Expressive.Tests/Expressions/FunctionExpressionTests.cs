using System.Collections.Generic;
using Expressive.Expressions;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Expressions
{
    [TestFixture]
    public class FunctionExpressionTests
    {
        [Test]
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
