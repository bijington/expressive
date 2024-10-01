using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Date
{
    [TestFixture]
    public class SecondsBetweenFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("SecondsBetween", this.ActualFunction.Name);
        }

        [Test]
        public void TestValidValues()
        {
            Assert.AreEqual(256d,
                this.Evaluate(new DateTime(2016, 02, 27, 12, 00, 00),
                    new DateTime(2016, 02, 27, 12, 00, 00).AddSeconds(256)));
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
            this.AssertException(typeof(ParameterCountMismatchException), "SecondsBetween() takes only 2 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new SecondsBetweenFunction();

        #endregion
    }
}
