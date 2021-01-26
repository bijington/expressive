using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Date
{
    [TestClass]
    public class AddMinutesFunctionTests : FunctionBaseTestBase
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("AddMinutes", this.ActualFunction.Name);
        }

        [TestMethod]
        public void TestLeapYear()
        {
            Assert.AreEqual(new DateTime(2016, 02, 29, 00, 01, 00),
                this.Evaluate(new DateTime(2016, 02, 28, 23, 00, 00), 61));
        }

        [TestMethod]
        public void TestWithBothNull()
        {
            Assert.IsNull(this.Evaluate(null, null));
        }

        [TestMethod]
        public void TestWithFirstNull()
        {
            Assert.IsNull(this.Evaluate(null, 123));
        }

        [TestMethod]
        public void TestWithSecondNull()
        {
            Assert.IsNull(this.Evaluate(DateTime.Now, null));
        }

        [TestMethod]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "AddMinutes() takes only 2 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new AddMinutesFunction();

        #endregion
    }
}
