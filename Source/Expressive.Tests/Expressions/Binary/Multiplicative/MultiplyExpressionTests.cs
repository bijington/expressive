using Expressive.Expressions.Binary.Multiplicative;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Multiplicative
{
    [TestFixture]
    public class MultiplyExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new MultiplyExpression(
                MockExpression.ThatEvaluatesTo(5),
                MockExpression.ThatEvaluatesTo(2),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(10, expression.Evaluate(null));
        }
    }
}
