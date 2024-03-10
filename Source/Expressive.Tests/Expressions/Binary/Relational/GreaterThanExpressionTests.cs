using Expressive.Expressions.Binary.Relational;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Relational
{
    public static class GreaterThanExpressionTests
    {
        [TestCase(null, null, false)]
        [TestCase(5, 5, false)]
        [TestCase(5, 2, true)]
        [TestCase(2, 5, false)]
        [TestCase(null, "abc", false)]
        [TestCase("abc", null, true)]
        [TestCase("abc", "abc", false)]
        [TestCase(null, false, false)]
        [TestCase(false, null, true)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        [TestCase(false, false, false)]
        [TestCase(false, true, false)]
        [TestCase(1.001, 1, true)]
        [TestCase(1, 1.001, false)]
        [TestCase(1.001, 1.001, false)]
        [TestCase(1, 1.00, false)]
        [TestCase(1.00, 1, false)]
        public static void TestEvaluate(object lhs, object rhs, object expectedValue)
        {
            var expression = new GreaterThanExpression(
                MockExpression.ThatEvaluatesTo(lhs),
                MockExpression.ThatEvaluatesTo(rhs),
                new Context(ExpressiveOptions.None));

            Assert.That(expression.Evaluate(null), Is.EqualTo(expectedValue));
        }
    }
}
