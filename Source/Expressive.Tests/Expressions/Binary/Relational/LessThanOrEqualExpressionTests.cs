using Expressive.Expressions.Binary.Relational;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Relational
{
    public static class LessThanOrEqualExpressionTests
    {
        [TestCase(null, null, true)]
        [TestCase(5, 5, true)]
        [TestCase(5, 2, false)]
        [TestCase(2, 5, true)]
        [TestCase(null, "abc", true)]
        [TestCase("abc", null, false)]
        [TestCase("abc", "abc", true)]
        [TestCase(null, false, true)]
        [TestCase(false, null, false)]
        [TestCase(true, false, false)]
        [TestCase(true, true, true)]
        [TestCase(false, false, true)]
        [TestCase(false, true, true)]
        [TestCase(1.001, 1, false)]
        [TestCase(1, 1.001, true)]
        [TestCase(1.001, 1.001, true)]
        [TestCase(1, 1.00, true)]
        [TestCase(1.00, 1, true)]
        public static void TestEvaluate(object lhs, object rhs, object expectedValue)
        {
            var expression = new LessThanOrEqualExpression(
                MockExpression.ThatEvaluatesTo(lhs),
                MockExpression.ThatEvaluatesTo(rhs),
                new Context(ExpressiveOptions.None));

            Assert.That(expression.Evaluate(null), Is.EqualTo(expectedValue));
        }
    }
}
