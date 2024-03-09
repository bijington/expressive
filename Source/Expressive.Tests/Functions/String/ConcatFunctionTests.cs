using System;
using Expressive.Functions;
using Expressive.Functions.String;
using NUnit.Framework;

namespace Expressive.Tests.Functions.String
{
    [TestFixture]
    public class ConcatFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("Concat", this.ActualFunction.Name);
        }

        [Test]
        public void TestWith2Strings()
        {
            var left = "left";
            var right = "right";

            Assert.AreEqual(left+right, this.Evaluate(left, right));
        }

        [Test]
        public void TestWith3Strings()
        {
            var left = "left";
            var middle = "middle";
            var right = "right";

            Assert.AreEqual(left + middle + right, this.Evaluate(left, middle, right));
        }

        [Test]
        public void TestWithNumbers()
        {
            var left = 10;
            var middle = 14;
            var right = 12.5;
            Assert.AreEqual("101412.5", this.Evaluate(left, middle, right));
        }

        [Test]
        public void TestWithMixed()
        {
            var left = "left";
            var middle = 14;
            var right = "right";
            Assert.AreEqual("left14right", this.Evaluate(left, middle, right));
        }

        [Test]
        public void TestWithNestedArrays()
        {
            var left = "left";
            var middle = new object[] { 14, "+", 24.3 };
            var right = "right";
            Assert.AreEqual("left14+24.3right", this.Evaluate(left, middle, right));
        }


        [Test]
        public void TestWithParametersAndNull()
        {
            
            //Assert.AreEqual(null, this.Evaluate(0, -10, 57, 45, null));
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new ConcatFunction();

        #endregion
    }
}
