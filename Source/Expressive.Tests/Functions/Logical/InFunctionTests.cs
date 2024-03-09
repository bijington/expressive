using Expressive.Functions;
using Expressive.Functions.Logical;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Logical
{
    [TestFixture]
    public class InFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void ShouldReturnTrueIfItemIsInTheList() => Assert.IsTrue((bool)this.Evaluate(new object[] { "a", "b", "c", "a" }));

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new InFunction();

        #endregion
    }
}
