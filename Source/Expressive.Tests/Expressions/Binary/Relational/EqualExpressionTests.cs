﻿using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Relational;
using Moq;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Relational
{
    public static class EqualExpressionTests
    {
        [TestCase(null, null, true)]
        [TestCase(5, 5, true)]
        [TestCase(5, 2, false)]
        [TestCase(2, 5, false)]
        [TestCase(null, "abc", false)]
        [TestCase("abc", null, false)]
        [TestCase("abc", "abc", true)]
        [TestCase(null, false, false)]
        [TestCase(false, null, false)]
        [TestCase(true, false, false)]
        [TestCase(true, true, true)]
        [TestCase(false, false, true)]
        [TestCase(false, true, false)]
        [TestCase(1.001, 1, false)]
        [TestCase(1, 1.001, false)]
        [TestCase(1.001, 1.001, true)]
        [TestCase(1, 1.00, true)]
        [TestCase(1.00, 1, true)]
        public static void TestEvaluate(object lhs, object rhs, object expectedValue)
        {
            var expression = new EqualExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == lhs),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == rhs),
                new Context(ExpressiveOptions.None));

            Assert.That(expression.Evaluate(null), Is.EqualTo(expectedValue));
        }
    }
}
