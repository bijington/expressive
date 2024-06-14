using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Conversion;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Conversion
{
    [TestFixture]
    public class DateFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("Date", this.ActualFunction.Name);
        }

        [Test]
        public void TestEvaluateWithDateTime()
        {
            object dateTimeNow = DateTime.Now;
            
            // If a DateTime goes in then the same should come out.
            Assert.AreEqual(dateTimeNow, this.Evaluate(dateTimeNow));
        }

        [Test]
        public void TestEvaluateWithDouble()
        {
            Assert.That(() => this.Evaluate(12345d), Throws.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void TestEvaluateWithFloat()
        {
            Assert.That(() => this.Evaluate(12345f), Throws.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void TestEvaluateWithInteger()
        {
            Assert.That(() => this.Evaluate(12345), Throws.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void TestEvaluateWithLong()
        {
            Assert.That(() => this.Evaluate(12345L), Throws.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void TestEvaluateWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] {null}));
        }

        [Test]
        public void TestEvaluateWithValidString()
        {
            Assert.AreEqual(new DateTime(2019, 01, 01), this.Evaluate("2019/01/01"));
        }

        [Test]
        public void TestEvaluateWithInvalidString()
        {
            Assert.That(() => this.Evaluate("20cb/01/01"), Throws.InstanceOf<FormatException>());
        }

        [Test]
        public void TestEvaluateWithValidStringAndValidFormat()
        {
            const string dateString = "2019/01/01 11:00:00";
            const string format = "yyyy/MM/dd hh:mm:ss";

            Assert.AreEqual(new DateTime(2019, 01, 01, 11, 00, 00), this.Evaluate(dateString, format));
        }

        [Test]
        public void TestEvaluateWithValidStringAndInvalidFormat()
        {
            const string dateString = "2019/01/01 11:00:00";
            const string format = "yyyy/MM/dd"; // Not entirely convinced that this should throw an error but DateTime.ParseExact does.

            Assert.That(() => this.Evaluate(dateString, format), Throws.InstanceOf<FormatException>());
        }

        [Test]
        public void TestEvaluateWithValidStringAndNonStringFormat()
        {
            const string dateString = "2019/01/01 11:00:00";
            const int format = 1234;

            // Any non string format is safely ignored. TODO: Should it be?
            Assert.AreEqual(new DateTime(2019, 01, 01, 11, 00, 00), this.Evaluate(dateString, format));
        }

        [Test]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "Date() expects at least 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new DateFunction();

        #endregion
    }
}
