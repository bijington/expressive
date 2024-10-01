using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Date
{
    [TestFixture]
    public class HourOfFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("HourOf", this.ActualFunction.Name);
        }

        [Test]
        public void TestWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [Test]
        public void TestWithDateTime()
        {
            Assert.AreEqual(12, this.Evaluate(new DateTime(2016, 02, 29, 12, 00, 00)));
        }

        [Test]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "HourOf() takes only 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new HourOfFunction();

        #endregion
    }
}
