using Expressive.Functions;
using Expressive.Functions.Statistical;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Statistical
{
    public class MeanFunctionTests : FunctionBaseTestBase
    {
        protected override IFunction ActualFunction => new MeanFunction();

        [Test]
        public void ShouldIgnoreNullValues() => Assert.That(this.Evaluate(10, 20, null), Is.EqualTo(15));

        [Test]
        public void ShouldIgnoreNullValuesInsideEnumerables() =>
            Assert.That(this.Evaluate(new object[] { 10, 20, null }), Is.EqualTo(15));
    }
}
