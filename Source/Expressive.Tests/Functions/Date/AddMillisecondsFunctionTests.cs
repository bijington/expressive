using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Date
{
    [TestFixture]
    public class AddMillisecondsFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("AddMilliseconds", this.ActualFunction.Name);
        }

        [Test]
        public void TestLeapYear()
        {
            Assert.AreEqual(new DateTime(2016, 02, 29, 00, 00, 01),
                this.Evaluate(new DateTime(2016, 02, 28, 23, 59, 59), 2000));
        }

        [Test]
        public void TestWithBothNull()
        {
            Assert.IsNull(this.Evaluate(null, null));
        }

        [Test]
        public void TestWithFirstNull()
        {
            Assert.IsNull(this.Evaluate(null, 123));
        }

        [Test]
        public void TestWithSecondNull()
        {
            Assert.IsNull(this.Evaluate(DateTime.Now, null));
        }

        [Test]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "AddMilliseconds() takes only 2 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new AddMillisecondsFunction();

        #endregion
    }
}
