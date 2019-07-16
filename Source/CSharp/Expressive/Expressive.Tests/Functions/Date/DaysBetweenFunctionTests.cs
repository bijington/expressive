using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Date
{
    [TestClass]
    public class DaysBetweenFunctionTests : FunctionBaseTests
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("DaysBetween", this.Function.Name);
        }

        [TestMethod]
        public void TestValidValues()
        {
            Assert.AreEqual(2d, this.Evaluate(new DateTime(2016, 02, 27), new DateTime(2016, 02, 29)));
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
            this.AssertException(typeof(ParameterCountMismatchException), "DaysBetween() takes only 2 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction Function => new DaysBetweenFunction();

        #endregion
    }
}
