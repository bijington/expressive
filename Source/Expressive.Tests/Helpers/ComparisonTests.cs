using Expressive.Helpers;
using NUnit.Framework;

namespace Expressive.Tests.Helpers
{
    public static class ComparisonTests
    {
        [Test]
        public static void CompareUsingMostPreciseTypeWithNullContextShouldThrowArgumentNullException() => 
            Assert.That(() => Comparison.CompareUsingMostPreciseType(null, null, null), Throws.ArgumentNullException);
    }
}
