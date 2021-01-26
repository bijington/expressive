using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Date;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Date
{
    [TestClass]
    public class YearOfFunctionTests : FunctionBaseTestBase
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("YearOf", this.ActualFunction.Name);
        }

        [TestMethod]
        public void TestWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [TestMethod]
        public void TestWithDateTime()
        {
            Assert.AreEqual(2016, this.Evaluate(new DateTime(2016, 02, 29)));
        }

        [TestMethod]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "YearOf() takes only 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new YearOfFunction();

        #endregion
    }
}
