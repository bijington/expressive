using System;
using System.Globalization;
using System.Threading;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Conversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Conversion
{
    [TestClass]
    public class StringFunctionTests : FunctionBaseTestBase
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("String", this.ActualFunction.Name);
        }

        [TestMethod]
        public void TestEvaluateWithDateTime()
        {
            var dateTime = DateTime.Now;

            Assert.AreEqual(dateTime.ToString(CultureInfo.CurrentCulture), this.Evaluate(dateTime));
        }

        [TestMethod]
        public void TestEvaluateWithDateTimeAndValidFormat()
        {
            var dateTime = new DateTime(2019, 01, 01, 11, 00, 00);
            const string dateString = "2019/01/01 11:00:00";
            const string format = "yyyy/MM/dd hh:mm:ss";

            Assert.AreEqual(dateString, this.Evaluate(dateTime, format));
        }

        [TestMethod]
        public void TestEvaluateWithDateTimeAndNonStringFormat()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-GB");

                var dateTime = new DateTime(2019, 01, 01, 11, 00, 00);
                const string dateString = "01/01/2019 11:00:00";

                Assert.AreEqual(dateString, this.Evaluate(dateTime, 1234));
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = currentCulture;
            }
        }

        [TestMethod]
        public void TestEvaluateWithDecimal()
        {
            Assert.AreEqual("12345", this.Evaluate(12345M));
        }

        [TestMethod]
        public void TestEvaluateWithDouble()
        {
            Assert.AreEqual("12345", this.Evaluate(12345d));
        }

        [TestMethod]
        public void TestEvaluateWithFloat()
        {
            Assert.AreEqual("12345", this.Evaluate(12345f));
        }

        [TestMethod]
        public void TestEvaluateWithInteger()
        {
            Assert.AreEqual("12345", this.Evaluate(12345));
        }

        [TestMethod]
        public void TestEvaluateWithLong()
        {
            Assert.AreEqual("12345", this.Evaluate(12345L));
        }

        [TestMethod]
        public void TestEvaluateWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [TestMethod]
        public void TestEvaluateWithString()
        {
            Assert.AreEqual("12345", this.Evaluate("12345"));
        }

        [TestMethod]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "String() expects at least 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new StringFunction();

        #endregion
    }
}
