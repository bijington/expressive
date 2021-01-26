using System;
using System.Linq;
using Expressive.Exceptions;
using Expressive.Expressions;
using Expressive.Functions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Expressive.Tests.Functions
{
    public static class FunctionBaseTests
    {
        [Test]
        public static void TestValidateParameterCountWithNull() =>
            Assert.That(
                () => new MockFunction().Validate(null, 0, 0),
                Throws.ArgumentNullException);

        [TestCase(0, 0, 0, false, TestName = "{m} - No parameters expected and none passed in.")]
        [TestCase(1, 0, 0, true, TestName = "{m} - No parameters expected but some passed in.")]
        [TestCase(0, 1, 0, true, TestName = "{m} - One parameter expected but none passed in.")]
        [TestCase(1, 1, 0, false, TestName = "{m} - One parameter expected and one passed in.")]
        [TestCase(2, 1, 0, true, TestName = "{m} - One parameter expected and two passed in.")]
        [TestCase(0, -1, 1, true, TestName = "{m} - One minimum expected but none passed in.")]
        [TestCase(1, -1, 1, false, TestName = "{m} - One minimum expected and one passed in.")]
        [TestCase(2, -1, 1, false, TestName = "{m} - One minimum expected and two passed in.")]
        public static void TestValidateParameterCount(int parameterCount, int expectedParameterCount, int expectedMinimumCount, bool shouldThrow)
        {
            var parameters = Enumerable.Repeat(Mock.Of<IExpression>(), parameterCount).ToArray();

            Assert.That(
                () => new MockFunction().Validate(parameters, expectedParameterCount, expectedMinimumCount),
                shouldThrow ? (Constraint)Throws.InstanceOf<ParameterCountMismatchException>() : Throws.Nothing);
        }

        private class MockFunction : FunctionBase
        {
            public override string Name => "Mock";

            public override object Evaluate(IExpression[] parameters, Context context) => throw new NotImplementedException();

            public void Validate(IExpression[] parameters, int expectedCount, int minimumCount) =>
                this.ValidateParameterCount(parameters, expectedCount, minimumCount);
        }
    }
}
