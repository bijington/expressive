using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Bitwise;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Bitwise
{
    [TestFixture]
    public class RightShiftExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new RightShiftExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)0x1001),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)0x0001),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(0x1001 >> 0x0001, expression.Evaluate(null));
        }
    }
}
