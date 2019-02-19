using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Date
{
    [TestClass]
    public class SecondsBetweenFunctionTests : FunctionBaseTests
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("SecondsBetween", this.Function.Name);
        }

        [TestMethod]
        public void TestValidValues()
        {
            Assert.AreEqual(256d,
                this.Evaluate(new DateTime(2016, 02, 27, 12, 00, 00),
                    new DateTime(2016, 02, 27, 12, 00, 00).AddSeconds(256)));
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
            this.AssertException(typeof(ParameterCountMismatchException), "SecondsBetween() takes only 2 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction Function => new SecondsBetweenFunction();

        #endregion
    }
}
