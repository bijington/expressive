using Expressive.Expressions.Binary.Additive;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Additive
{
    [TestFixture]
    public class SubtractExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new SubtractExpression(
                MockExpression.ThatEvaluatesTo(1),
                MockExpression.ThatEvaluatesTo(2),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(-1, expression.Evaluate(null));
        }
    }
}
