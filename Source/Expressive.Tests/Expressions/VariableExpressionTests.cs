using System;
using System.Collections.Generic;
using Expressive.Expressions;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Expressive.Tests.Expressions
{
    public static class VariableExpressionTests
    {
        [Test]
        public static void TestWithExpressionVariableSupplied()
        {
            var expression = new VariableExpression("pi");

            var variable = Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)Math.PI);

            Assert.That(
                expression.Evaluate(new Dictionary<string, object> { ["pi"] = variable }),
                Is.EqualTo(Math.PI));
        }

        [Test]
        public static void TestWithNullVariableSupplied()
        {
            var expression = new VariableExpression("pie");

            Assert.That(
                () => expression.Evaluate(null),
                Throws.ArgumentException);
        }

        [Test]
        public static void TestWithoutVariableSupplied()
        {
            var expression = new VariableExpression("pie");

            Assert.That(
                () => expression.Evaluate(new Dictionary<string, object>()),
                Throws.ArgumentException);
        }

        [Test]
        public static void TestWithVariableSupplied()
        {
            var expression = new VariableExpression("pi");

            Assert.That(
                expression.Evaluate(new Dictionary<string, object> { ["pi"] = Math.PI }),
                Is.EqualTo(Math.PI));
        }
    }
}
