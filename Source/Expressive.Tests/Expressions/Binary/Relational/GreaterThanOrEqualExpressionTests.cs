﻿using Expressive.Expressions.Binary.Relational;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Relational
{
    public static class GreaterThanOrEqualExpressionTests
    {
        [TestCase(null, null, true)]
        [TestCase(5, 5, true)]
        [TestCase(5, 2, true)]
        [TestCase(2, 5, false)]
        [TestCase(null, "abc", false)]
        [TestCase("abc", null, true)]
        [TestCase("abc", "abc", true)]
        [TestCase(null, false, false)]
        [TestCase(false, null, true)]
        [TestCase(true, false, true)]
        [TestCase(true, true, true)]
        [TestCase(false, false, true)]
        [TestCase(false, true, false)]
        [TestCase(1.001, 1, true)]
        [TestCase(1, 1.001, false)]
        [TestCase(1.001, 1.001, true)]
        [TestCase(1, 1.00, true)]
        [TestCase(1.00, 1, true)]
        public static void TestEvaluate(object lhs, object rhs, object expectedValue)
        {
            var expression = new GreaterThanOrEqualExpression(
                MockExpression.ThatEvaluatesTo(lhs),
                MockExpression.ThatEvaluatesTo(rhs),
                new Context(ExpressiveOptions.None));

            Assert.That(expression.Evaluate(null), Is.EqualTo(expectedValue));
        }
    }
}
