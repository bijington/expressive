using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Conversion;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Conversion
{
    [TestFixture]
    public class IntegerFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("Integer", this.ActualFunction.Name);
        }

        [Test]
        public void TestEvaluateWithDateTime()
        {
            Assert.That(() => this.Evaluate(DateTime.Now), Throws.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void TestEvaluateWithDecimal()
        {
            Assert.AreEqual(12345, this.Evaluate(12345M));
        }

        [Test]
        public void TestEvaluateWithDouble()
        {
            Assert.AreEqual(12345, this.Evaluate(12345d));
        }

        [Test]
        public void TestEvaluateWithFloat()
        {
            Assert.AreEqual(12345, this.Evaluate(12345f));
        }

        [Test]
        public void TestEvaluateWithInteger()
        {
            Assert.AreEqual(12345, this.Evaluate(12345));
        }

        [Test]
        public void TestEvaluateWithLong()
        {
            Assert.AreEqual(12345, this.Evaluate(12345L));
        }

        [Test]
        public void TestEvaluateWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [Test]
        public void TestEvaluateWithString()
        {
            Assert.AreEqual(12345, this.Evaluate("12345"));
        }

        [Test]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "Integer() takes only 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new IntegerFunction();

        #endregion
    }
}
