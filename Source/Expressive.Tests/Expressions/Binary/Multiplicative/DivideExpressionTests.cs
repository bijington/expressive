using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Multiplicative;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Multiplicative
{
    [TestFixture]
    public class DivideExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new DivideExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(1d, expression.Evaluate(null));
        }
    }
}
