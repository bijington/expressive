using Expressive.Expressions.Binary.Bitwise;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Bitwise
{
    [TestFixture]
    public class LeftShiftExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new LeftShiftExpression(
                MockExpression.ThatEvaluatesTo(0x1001),
                MockExpression.ThatEvaluatesTo(0x0001),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(0x1001 << 0x0001, expression.Evaluate(null));
        }
    }
}
