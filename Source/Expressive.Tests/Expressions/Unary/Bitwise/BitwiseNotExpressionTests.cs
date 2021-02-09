using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Unary.Logical;
using Moq;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Unary.Bitwise
{
    public static class BitwiseNotExpressionTests
    {
        [TestCase(null, null)]
        [TestCase(0b11110000, 0b00001111)]
        [TestCase(1, 1)]
        public static void TestEvaluate(object input, object expectedResult)
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == input));

            Assert.That(expression.Evaluate(null), Is.EqualTo(expectedResult));
        }
    }
}
