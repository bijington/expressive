using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Date
{
    [TestFixture]
    public class DaysBetweenFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("DaysBetween", this.ActualFunction.Name);
        }

        [Test]
        public void TestValidValues()
        {
            Assert.AreEqual(2d, this.Evaluate(new DateTime(2016, 02, 27), new DateTime(2016, 02, 29)));
        }

        [Test]
        public void TestWithBothNull()
        {
            Assert.IsNull(this.Evaluate(null, null));
        }

        [Test]
        public void TestWithFirstNull()
        {
            Assert.IsNull(this.Evaluate(null, 123));
        }

        [Test]
        public void TestWithSecondNull()
        {
            Assert.IsNull(this.Evaluate(DateTime.Now, null));
        }

        [Test]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "DaysBetween() takes only 2 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new DaysBetweenFunction();

        #endregion
    }
}
