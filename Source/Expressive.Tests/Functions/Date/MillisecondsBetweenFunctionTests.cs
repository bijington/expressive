using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Date
{
    [TestFixture]
    public class MillisecondsBetweenFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("MillisecondsBetween", this.ActualFunction.Name);
        }

        [Test]
        public void TestValidValues()
        {
            Assert.AreEqual(750d,
                this.Evaluate(new DateTime(2016, 02, 27, 12, 00, 00),
                    new DateTime(2016, 02, 27, 12, 00, 00).AddMilliseconds(750)));
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
            this.AssertException(typeof(ParameterCountMismatchException), "MillisecondsBetween() takes only 2 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new MillisecondsBetweenFunction();

        #endregion
    }
}
