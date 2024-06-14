using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Date
{
    [TestFixture]
    public class AddYearsFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("AddYears", this.ActualFunction.Name);
        }

        [Test]
        public void TestLeapYear()
        {
            Assert.AreEqual(new DateTime(2017, 02, 28, 23, 00, 00),
                this.Evaluate(new DateTime(2016, 02, 29, 23, 00, 00), 1));
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
            this.AssertException(typeof(ParameterCountMismatchException), "AddYears() takes only 2 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new AddYearsFunction();

        #endregion
    }
}
