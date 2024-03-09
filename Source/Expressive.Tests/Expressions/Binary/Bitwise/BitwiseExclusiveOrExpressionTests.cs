using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Bitwise;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Bitwise
{
    [TestFixture]
    public class BitwiseExclusiveOrExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new BitwiseExclusiveOrExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1111),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)0001),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(1110, expression.Evaluate(null));
        }
    }
}
