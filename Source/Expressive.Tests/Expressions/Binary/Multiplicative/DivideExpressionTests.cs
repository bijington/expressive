using Expressive.Expressions.Binary.Multiplicative;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Multiplicative
{
    [TestFixture]
    public class DivideExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new DivideExpression(
                MockExpression.ThatEvaluatesTo(1),
                MockExpression.ThatEvaluatesTo(1),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(1d, expression.Evaluate(null));
        }
    }
}
