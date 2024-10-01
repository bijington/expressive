using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Date
{
    [TestFixture]
    public class MillisecondOfFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("MillisecondOf", this.ActualFunction.Name);
        }

        [Test]
        public void TestWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [Test]
        public void TestWithDateTime()
        {
            Assert.AreEqual(500, this.Evaluate(new DateTime(2016, 02, 29).AddMilliseconds(500)));
        }

        [Test]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "MillisecondOf() takes only 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new MillisecondOfFunction();

        #endregion
    }
}
