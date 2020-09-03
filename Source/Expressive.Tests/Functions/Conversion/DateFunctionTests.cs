using System;
using System.Collections.Generic;
using Expressive.Exceptions;
using Expressive.Expressions;
using Expressive.Functions;
using Expressive.Functions.Conversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Functions.Conversion
{
    [TestClass]
    public class DateFunctionTests : FunctionBaseTests
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("Date", this.ActualFunction.Name);
        }

        [TestMethod]
        public void TestEvaluateWithDateTime()
        {
            object dateTimeNow = DateTime.Now;
            
            // If a DateTime goes in then the same should come out.
            Assert.AreEqual(dateTimeNow, this.Evaluate(dateTimeNow));
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void TestEvaluateWithDouble()
        {
            this.Evaluate(12345d);
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void TestEvaluateWithFloat()
        {
            this.Evaluate(12345f);
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void TestEvaluateWithInteger()
        {
            this.Evaluate(12345);
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void TestEvaluateWithLong()
        {
            this.Evaluate(12345L);
        }

        [TestMethod]
        public void TestEvaluateWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] {null}));
        }

        [TestMethod]
        public void TestEvaluateWithValidString()
        {
            Assert.AreEqual(new DateTime(2019, 01, 01), this.Evaluate("2019/01/01"));
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void TestEvaluateWithInvalidString()
        {
            this.Evaluate("20cb/01/01");
        }

        [TestMethod]
        public void TestEvaluateWithValidStringAndValidFormat()
        {
            const string dateString = "2019/01/01 11:00:00";
            const string format = "yyyy/MM/dd hh:mm:ss";

            Assert.AreEqual(new DateTime(2019, 01, 01, 11, 00, 00), this.Evaluate(dateString, format));
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void TestEvaluateWithValidStringAndInvalidFormat()
        {
            const string dateString = "2019/01/01 11:00:00";
            const string format = "yyyy/MM/dd"; // Not entirely convinced that this should throw an error but DateTime.ParseExact does.

            this.Evaluate(dateString, format);
        }

        [TestMethod]
        public void TestEvaluateWithValidStringAndNonStringFormat()
        {
            const string dateString = "2019/01/01 11:00:00";
            const int format = 1234;

            // Any non string format is safely ignored. TODO: Should it be?
            Assert.AreEqual(new DateTime(2019, 01, 01, 11, 00, 00), this.Evaluate(dateString, format));
        }

        [TestMethod]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "Date() expects at least 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new DateFunction();

        #endregion
    }
}
