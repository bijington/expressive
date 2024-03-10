
using Expressive.Expressions;
using NUnit.Framework;
using System;

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
                Array.Empty<IExpression>());

            Assert.AreEqual(123, expression.Evaluate(null));
        }
    }
}
