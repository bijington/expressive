using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Bitwise;
using Moq;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary.Bitwise
{
    public static class BitwiseAndExpressionTests
    {
        [TestCase(null, null, null)]

        [TestCase(0x1001, 0x0001, 0x0001)]
        [TestCase(byte.MaxValue, null, null)]
        [TestCase(byte.MaxValue, byte.MaxValue, byte.MaxValue)]
        [TestCase(byte.MaxValue, byte.MaxValue - 1, byte.MaxValue - 1)]
        [TestCase(byte.MaxValue, int.MaxValue, byte.MaxValue)]
        [TestCase(byte.MaxValue, long.MaxValue, byte.MaxValue)]
        [TestCase(byte.MaxValue, short.MaxValue, byte.MaxValue)]
        [TestCase(byte.MaxValue, uint.MaxValue, byte.MaxValue)]
        [TestCase(byte.MaxValue, ulong.MaxValue, byte.MaxValue)]
        [TestCase(byte.MaxValue, ushort.MaxValue, byte.MaxValue)]

        [TestCase(int.MaxValue, null, null)]
        [TestCase(int.MaxValue, int.MaxValue, int.MaxValue)]
        [TestCase(int.MaxValue, int.MaxValue - 1, int.MaxValue - 1)]
        [TestCase(int.MaxValue, byte.MaxValue, byte.MaxValue)]
        [TestCase(int.MaxValue, long.MaxValue, int.MaxValue)]
        [TestCase(int.MaxValue, short.MaxValue, short.MaxValue)]
        [TestCase(int.MaxValue, uint.MaxValue, int.MaxValue)]
        [TestCase(int.MaxValue, ushort.MaxValue, ushort.MaxValue)]

        [TestCase(long.MaxValue, null, null)]
        [TestCase(long.MaxValue, long.MaxValue, long.MaxValue)]
        [TestCase(long.MaxValue, long.MaxValue - 1, long.MaxValue - 1)]
        [TestCase(long.MaxValue, byte.MaxValue, byte.MaxValue)]
        [TestCase(long.MaxValue, int.MaxValue, int.MaxValue)]
        [TestCase(long.MaxValue, short.MaxValue, short.MaxValue)]
        [TestCase(long.MaxValue, uint.MaxValue, uint.MaxValue)]
        [TestCase(long.MaxValue, ushort.MaxValue, ushort.MaxValue)]

        [TestCase(short.MaxValue, null, null)]
        [TestCase(short.MaxValue, short.MaxValue, short.MaxValue)]
        [TestCase(short.MaxValue, short.MaxValue - 1, short.MaxValue - 1)]
        [TestCase(short.MaxValue, byte.MaxValue, byte.MaxValue)]
        [TestCase(short.MaxValue, int.MaxValue, short.MaxValue)]
        [TestCase(short.MaxValue, long.MaxValue, short.MaxValue)]
        [TestCase(short.MaxValue, uint.MaxValue, short.MaxValue)]
        [TestCase(short.MaxValue, ushort.MaxValue, short.MaxValue)]

        [TestCase(uint.MaxValue, null, null)]
        [TestCase(uint.MaxValue, uint.MaxValue, uint.MaxValue)]
        [TestCase(uint.MaxValue, uint.MaxValue - 1, uint.MaxValue - 1)]
        [TestCase(uint.MaxValue, byte.MaxValue, byte.MaxValue)]
        [TestCase(uint.MaxValue, int.MaxValue, int.MaxValue)]
        [TestCase(uint.MaxValue, long.MaxValue, uint.MaxValue)]
        [TestCase(uint.MaxValue, short.MaxValue, short.MaxValue)]
        [TestCase(uint.MaxValue, ushort.MaxValue, ushort.MaxValue)]

        [TestCase(ulong.MaxValue, null, null)]
        [TestCase(ulong.MaxValue, ulong.MaxValue, ulong.MaxValue)]
        [TestCase(ulong.MaxValue, ulong.MaxValue - 1, ulong.MaxValue - 1)]
        [TestCase(ulong.MaxValue, byte.MaxValue, byte.MaxValue)]
        [TestCase(ulong.MaxValue, uint.MaxValue, uint.MaxValue)]
        [TestCase(ulong.MaxValue, ushort.MaxValue, ushort.MaxValue)]

        [TestCase(ushort.MaxValue, null, null)]
        [TestCase(ushort.MaxValue, ushort.MaxValue, ushort.MaxValue)]
        [TestCase(ushort.MaxValue, ushort.MaxValue - 1, ushort.MaxValue - 1)]
        [TestCase(ushort.MaxValue, byte.MaxValue, byte.MaxValue)]
        [TestCase(ushort.MaxValue, int.MaxValue, ushort.MaxValue)]
        [TestCase(ushort.MaxValue, short.MaxValue, short.MaxValue)]
        [TestCase(ushort.MaxValue, uint.MaxValue, ushort.MaxValue)]
        [TestCase(ushort.MaxValue, ulong.MaxValue, ushort.MaxValue)]
        public static void TestEvaluate(object lhs, object rhs, object expectedValue)
        {
            var expression = new BitwiseAndExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == lhs),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == rhs),
                new Context(ExpressiveOptions.None));

            Assert.That(expression.Evaluate(null), Is.EqualTo(expectedValue));
        }
    }
}
