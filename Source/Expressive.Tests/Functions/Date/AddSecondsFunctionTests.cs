using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Date
{
    [TestClass]
    public class AddSecondsFunctionTests : FunctionBaseTestBase
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("AddSeconds", this.ActualFunction.Name);
        }

        [TestMethod]
        public void TestLeapYear()
        {
            Assert.AreEqual(new DateTime(2016, 02, 29, 00, 00, 05),
                this.Evaluate(new DateTime(2016, 02, 28, 23, 59, 40), 25));
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
            this.AssertException(typeof(ParameterCountMismatchException), "AddSeconds() takes only 2 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new AddSecondsFunction();

        #endregion
    }
}
