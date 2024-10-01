using Expressive.Expressions.Binary.Bitwise;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Bitwise
{
    [TestFixture]
    public class BitwiseOrExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new BitwiseOrExpression(
                MockExpression.ThatEvaluatesTo(1001),
                MockExpression.ThatEvaluatesTo(0001),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(1001, expression.Evaluate(null));
        }
    }
}
