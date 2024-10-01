using Expressive.Expressions.Binary.Bitwise;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Bitwise
{
    [TestFixture]
    public class BitwiseExclusiveOrExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new BitwiseExclusiveOrExpression(
                MockExpression.ThatEvaluatesTo(1111),
                MockExpression.ThatEvaluatesTo(0001),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(1110, expression.Evaluate(null));
        }
    }
}
