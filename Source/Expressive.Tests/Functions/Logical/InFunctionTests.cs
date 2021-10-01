using Expressive.Functions;
using Expressive.Functions.Logical;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Functions.Logical
{
    [TestClass]
    public class InFunctionTests : FunctionBaseTestBase
    {
        [TestMethod]
        public void ShouldReturnTrueIfItemIsInTheList() => Assert.IsTrue((bool)this.Evaluate(new object[] { "a", "b", "c", "a" }));

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new InFunction();

        #endregion
    }
}
