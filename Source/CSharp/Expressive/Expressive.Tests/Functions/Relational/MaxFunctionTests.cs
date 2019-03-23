using System;
using System.Collections.Generic;
using Expressive.Functions;
using Expressive.Functions.Relational;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Relational
{
    [TestClass]
    public class MaxFunctionTests : FunctionBaseTests
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("Max", this.Function.Name);
        }

        [TestMethod]
        public void TestWithDates()
        {
            var min = new DateTime(2019, 06, 12);
            var middle = new DateTime(2019, 09, 12);
            var max = new DateTime(2019, 12, 12);

            this.TestValues(max, middle, max, min);
        }

        [TestMethod]
        public void TestWithDecimals()
        {
            this.TestValues(12.5M, 12.5M, 10.9M, 0.9M);
        }

        [TestMethod]
        public void TestWithDoubles()
        {
            this.TestValues(12.5, 12.5, 10.9, 0.9);
        }

        [TestMethod]
        public void TestWithIntegers()
        {
            this.TestValues(125, 125, 109, 0);
        }

        [TestMethod]
        public void TestWithLongs()
        {
            this.TestValues(long.MaxValue, long.MaxValue, 10L, long.MinValue);
        }

        [TestMethod]
        public void TestWithNull()
        {
            this.TestValues(null, long.MaxValue, 10L, long.MinValue, null);
        }

        [TestMethod]
        public void TestWithParameters()
        {
            Assert.AreEqual(57, new Expression("Max(0, -10, 57, 45)", ExpressiveOptions.IgnoreCase).Evaluate());
        }

        [TestMethod]
        public void TestWithParametersAndNestedIEnumerable()
        {
            var arguments = new Dictionary<string, object>
            {
                ["Values"] = new object[] { 57, 45, 99 }
            };

            Assert.AreEqual(99, new Expression("Max(57, -10, [Values], 45)", ExpressiveOptions.IgnoreCase).Evaluate(arguments));
        }

        [TestMethod]
        public void TestWithParametersAndNull()
        {
            Assert.AreEqual(null, new Expression("Max(0, -10, 57, 45, null)", ExpressiveOptions.IgnoreCase).Evaluate());
        }

        private void TestValues(object expectedValue, params object[] values)
        {
            var arguments = new Dictionary<string, object>
            {
                ["Values"] = values
            };

            Assert.AreEqual(expectedValue,
                new Expression("Max([Values])", ExpressiveOptions.IgnoreCase).Evaluate(arguments));
        }

        #region FunctionBaseTests Members

        protected override IFunction Function => new MaxFunction();

        #endregion
    }
}
