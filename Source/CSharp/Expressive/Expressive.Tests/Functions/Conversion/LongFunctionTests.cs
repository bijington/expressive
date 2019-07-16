using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expressive.Exceptions;
using Expressive.Functions;
using Expressive.Functions.Conversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Conversion
{
    [TestClass]
    public class LongFunctionTests : FunctionBaseTests
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("Long", this.Function.Name);
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void TestEvaluateWithDateTime()
        {
            this.Evaluate(DateTime.Now);
        }

        [TestMethod]
        public void TestEvaluateWithDecimal()
        {
            Assert.AreEqual(12345L, this.Evaluate(12345M));
        }

        [TestMethod]
        public void TestEvaluateWithDouble()
        {
            Assert.AreEqual(12345L, this.Evaluate(12345d));
        }

        [TestMethod]
        public void TestEvaluateWithFloat()
        {
            Assert.AreEqual(12345L, this.Evaluate(12345f));
        }

        [TestMethod]
        public void TestEvaluateWithInteger()
        {
            Assert.AreEqual(12345L, this.Evaluate(12345));
        }

        [TestMethod]
        public void TestEvaluateWithLong()
        {
            Assert.AreEqual(12345L, this.Evaluate(12345L));
        }

        [TestMethod]
        public void TestEvaluateWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [TestMethod]
        public void TestEvaluateWithString()
        {
            Assert.AreEqual(12345L, this.Evaluate("12345"));
        }

        [TestMethod]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "Long() takes only 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction Function => new LongFunction();

        #endregion
    }
}
