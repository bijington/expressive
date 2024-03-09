using System;
using Expressive.Functions;
using Expressive.Functions.String;
using NUnit.Framework;

namespace Expressive.Tests.Functions.String
{
    [TestFixture]
    public class IndexOfFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("IndexOf", this.ActualFunction.Name);
        }

        [Test]
        public void TestWithStartString()
        {
            var left = "left";
            var right = "l";

            Assert.AreEqual(0, this.Evaluate(left, right));

            
        }

        [Test]
        public void TestWithMidString()
        {
            var left = "left";
            var right = "ft";

            Assert.AreEqual(2, this.Evaluate(left, right));
        }

        [Test]
        public void TestWithNotFound()
        {
            var left = "left";
            var right = "fte";

            Assert.AreEqual(-1, this.Evaluate(left, right));
        }

        [Test]
        public void TestWithArray()
        {
            var left =  new object[] { "left", 12, "right" };
            var right = "right";

            Assert.AreEqual(2, this.Evaluate(left, right));

            var num = 12;

            Assert.AreEqual(1, this.Evaluate(left, num));

            right = "l";

            Assert.AreEqual(-1, this.Evaluate(left, right));

            num = 14;

            Assert.AreEqual(-1, this.Evaluate(left, num));
        }


        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new IndexOfFunction();

        #endregion
    }
}
