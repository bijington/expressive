using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests
{
    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void TestStringComparwe()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.AreEqual(StringComparer.Ordinal, context.StringComparer);
        }

        [TestMethod]
        public void TestStringComparerIgnoreCase()
        {
            var context = new Context(ExpressiveOptions.IgnoreCase);

            Assert.AreEqual(StringComparer.OrdinalIgnoreCase, context.StringComparer);
        }

        [TestMethod]
        public void TestStringComparison()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.AreEqual(StringComparison.Ordinal, context.StringComparison);
        }

        [TestMethod]
        public void TestStringComparisonIgnoreCase()
        {
            var context = new Context(ExpressiveOptions.IgnoreCase);

            Assert.AreEqual(StringComparison.OrdinalIgnoreCase, context.StringComparison);
        }
    }
}
