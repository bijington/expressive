using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Unary.Bitwise;
using Moq;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Unary.Bitwise
{
    public static class BitwiseNotExpressionTests
    {
        [TestCase(null, null)]

        [TestCase((byte)0xEE, (byte)0x11)]
        [TestCase(byte.MaxValue, byte.MinValue)]
        [TestCase((byte)(byte.MaxValue - 1), byte.MinValue + 1)]

        [TestCase(int.MaxValue, int.MinValue)]
        [TestCase(int.MaxValue - 1, int.MinValue + 1)]
        [TestCase(1234, -1235)]

        [TestCase(long.MaxValue, long.MinValue)]
        [TestCase(long.MaxValue - 1, long.MinValue + 1)]

        [TestCase(short.MaxValue, short.MinValue)]
        [TestCase(short.MaxValue - 1, short.MinValue + 1)]

        [TestCase(uint.MaxValue, uint.MinValue)]
        [TestCase(uint.MaxValue - 1, uint.MinValue + 1)]

        [TestCase(ulong.MaxValue, ulong.MinValue)]
        [TestCase(ulong.MaxValue - 1, ulong.MinValue + 1)]

        [TestCase(ushort.MaxValue, ushort.MinValue)]
        [TestCase((ushort)(ushort.MaxValue - 1), ushort.MinValue + 1)]
        public static void TestEvaluate(object input, object expectedResult)
        {
            var expression = new BitwiseNotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == input));

            Assert.That(expression.Evaluate(null), Is.EqualTo(expectedResult));
        }
    }
}
