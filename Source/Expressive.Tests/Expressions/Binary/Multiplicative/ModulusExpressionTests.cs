using Expressive.Expressions.Binary.Multiplicative;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Multiplicative
{
    [TestFixture]
    public class ModulusExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new ModulusExpression(
                MockExpression.ThatEvaluatesTo(5),
                MockExpression.ThatEvaluatesTo(2),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(1, expression.Evaluate(null));
        }
    }
}
