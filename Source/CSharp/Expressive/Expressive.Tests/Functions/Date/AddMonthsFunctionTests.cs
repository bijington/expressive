using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Date
{
    [TestClass]
    public class AddMonthsFunctionTests : FunctionBaseTests
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("AddMonths", this.Function.Name);
        }

        [TestMethod]
        public void TestLeapYear()
        {
            Assert.AreEqual(new DateTime(2016, 02, 29, 23, 00, 00),
                this.Evaluate(new DateTime(2016, 01, 29, 23, 00, 00), 1));
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
            this.AssertException(typeof(ParameterCountMismatchException), "AddMonths() takes only 2 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction Function => new AddMonthsFunction();

        #endregion
    }
}
