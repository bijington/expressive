using System;
using System.Globalization;
using System.Threading;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Conversion;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Conversion
{
    [TestFixture]
    public class StringFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("String", this.ActualFunction.Name);
        }

        [Test]
        public void TestEvaluateWithDateTime()
        {
            var dateTime = DateTime.Now;

            Assert.AreEqual(dateTime.ToString(CultureInfo.CurrentCulture), this.Evaluate(dateTime));
        }

        [Test]
        public void TestEvaluateWithDateTimeAndValidFormat()
        {
            var dateTime = new DateTime(2019, 01, 01, 11, 00, 00);
            const string dateString = "2019/01/01 11:00:00";
            const string format = "yyyy/MM/dd hh:mm:ss";

            Assert.AreEqual(dateString, this.Evaluate(dateTime, format));
        }

        [Test]
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

        [Test]
        public void TestEvaluateWithDecimal()
        {
            Assert.AreEqual("12345", this.Evaluate(12345M));
        }

        [Test]
        public void TestEvaluateWithDouble()
        {
            Assert.AreEqual("12345", this.Evaluate(12345d));
        }

        [Test]
        public void TestEvaluateWithFloat()
        {
            Assert.AreEqual("12345", this.Evaluate(12345f));
        }

        [Test]
        public void TestEvaluateWithInteger()
        {
            Assert.AreEqual("12345", this.Evaluate(12345));
        }

        [Test]
        public void TestEvaluateWithLong()
        {
            Assert.AreEqual("12345", this.Evaluate(12345L));
        }

        [Test]
        public void TestEvaluateWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [Test]
        public void TestEvaluateWithString()
        {
            Assert.AreEqual("12345", this.Evaluate("12345"));
        }

        [Test]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "String() expects at least 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new StringFunction();

        #endregion
    }
}
