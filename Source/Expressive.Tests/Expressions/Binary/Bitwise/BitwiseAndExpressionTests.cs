using Expressive.Expressions.Binary.Bitwise;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Bitwise
{
    [TestFixture]
    public class BitwiseAndExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new BitwiseAndExpression(
                MockExpression.ThatEvaluatesTo(0x1001),
                MockExpression.ThatEvaluatesTo(0x0001),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(0x0001, expression.Evaluate(null));
        }
    }
}
