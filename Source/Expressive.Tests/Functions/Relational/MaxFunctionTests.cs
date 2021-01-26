using System;
using Expressive.Functions;
using Expressive.Functions.Relational;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Relational
{
    [TestClass]
    public class MaxFunctionTests : FunctionBaseTestBase
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("Max", this.ActualFunction.Name);
        }

        [TestMethod]
        public void TestWithDates()
        {
            var min = new DateTime(2019, 06, 12);
            var middle = new DateTime(2019, 09, 12);
            var max = new DateTime(2019, 12, 12);

            Assert.AreEqual(max, this.Evaluate(middle, max, min));
        }

        [TestMethod]
        public void TestWithDecimals()
        {
            Assert.AreEqual(12.5M, this.Evaluate(12.5M, 10.9M, 0.9M));
        }

        [TestMethod]
        public void TestWithDoubles()
        {
            Assert.AreEqual(12.5, this.Evaluate(12.5, 10.9, 0.9));
        }

        [TestMethod]
        public void TestWithIntegers()
        {
            Assert.AreEqual(125, this.Evaluate(125, 109, 0));
        }

        [TestMethod]
        public void TestWithLongs()
        {
            Assert.AreEqual(long.MaxValue, this.Evaluate(long.MaxValue, 10L, long.MinValue));
        }

        [TestMethod]
        public void TestWithNull()
        {
            Assert.AreEqual(null, this.Evaluate(long.MaxValue, 10L, long.MinValue, null));
        }

        [TestMethod]
        public void TestWithParameters()
        {
            Assert.AreEqual(57, this.Evaluate(0, -10, 57, 45));
        }

        [TestMethod]
        public void TestWithParametersAndNestedIEnumerable()
        {
            Assert.AreEqual(99, this.Evaluate(57, -10, new object[] { 57, 45, 99 }, 45));
        }

        [TestMethod]
        public void TestWithParametersAndNull()
        {
            Assert.AreEqual(null, this.Evaluate(0, -10, 57, 45, null));
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new MaxFunction();

        #endregion
    }
}
