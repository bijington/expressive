using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Date
{
    [TestClass]
    public class HourOfFunctionTests : FunctionBaseTests
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("HourOf", this.Function.Name);
        }

        [TestMethod]
        public void TestWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [TestMethod]
        public void TestWithDateTime()
        {
            Assert.AreEqual(12, this.Evaluate(new DateTime(2016, 02, 29, 12, 00, 00)));
        }

        [TestMethod]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "HourOf() takes only 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction Function => new HourOfFunction();

        #endregion
    }
}
