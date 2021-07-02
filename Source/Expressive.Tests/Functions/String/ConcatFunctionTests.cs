using System;
using Expressive.Functions;
using Expressive.Functions.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.String
{
    [TestClass]
    public class ConcatFunctionTests : FunctionBaseTestBase
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("Concat", this.ActualFunction.Name);
        }

        [TestMethod]
        public void TestWith2Strings()
        {
            var left = "left";
            var right = "right";

            Assert.AreEqual(left+right, this.Evaluate(left, right));
        }

        [TestMethod]
        public void TestWith3Strings()
        {
            var left = "left";
            var middle = "middle";
            var right = "right";

            Assert.AreEqual(left + middle + right, this.Evaluate(left, middle, right));
        }

        [TestMethod]
        public void TestWithNumbers()
        {
            var left = 10;
            var middle = 14;
            var right = 12.5;
            Assert.AreEqual("101412.5", this.Evaluate(left, middle, right));
        }

        [TestMethod]
        public void TestWithMixed()
        {
            var left = "left";
            var middle = 14;
            var right = "right";
            Assert.AreEqual("left14right", this.Evaluate(left, middle, right));
        }

        [TestMethod]
        public void TestWithNestedArrays()
        {
            var left = "left";
            var middle = new object[] { 14, "+", 24.3 };
            var right = "right";
            Assert.AreEqual("left14+24.3right", this.Evaluate(left, middle, right));
        }


        [TestMethod]
        public void TestWithParametersAndNull()
        {
            
            //Assert.AreEqual(null, this.Evaluate(0, -10, 57, 45, null));
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new ConcatFunction();

        #endregion
    }
}
