﻿using Expressive.Expressions.Binary.Relational;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Relational
{
    public static class LessThanExpressionTests
    {
        [TestCase(null, null, false)]
        [TestCase(5, 5, false)]
        [TestCase(5, 2, false)]
        [TestCase(2, 5, true)]
        [TestCase(null, "abc", true)]
        [TestCase("abc", null, false)]
        [TestCase("abc", "abc", false)]
        [TestCase(null, false, true)]
        [TestCase(false, null, false)]
        [TestCase(true, false, false)]
        [TestCase(true, true, false)]
        [TestCase(false, false, false)]
        [TestCase(false, true, true)]
        [TestCase(1.001, 1, false)]
        [TestCase(1, 1.001, true)]
        [TestCase(1.001, 1.001, false)]
        [TestCase(1, 1.00, false)]
        [TestCase(1.00, 1, false)]
        public static void TestEvaluate(object lhs, object rhs, object expectedValue)
        {
            var expression = new LessThanExpression(
                MockExpression.ThatEvaluatesTo(lhs),
                MockExpression.ThatEvaluatesTo(rhs),
                new Context(ExpressiveOptions.None));

            Assert.That(expression.Evaluate(null), Is.EqualTo(expectedValue));
        }
    }
}
