using System;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Date
{
    [TestFixture]
    public class SecondOfFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("SecondOf", this.ActualFunction.Name);
        }

        [Test]
        public void TestWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [Test]
        public void TestWithDateTime()
        {
            Assert.AreEqual(29, this.Evaluate(new DateTime(2016, 02, 29, 15, 00, 29)));
        }

        [Test]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "SecondOf() takes only 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new SecondOfFunction();

        #endregion
    }
}
