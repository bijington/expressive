using System;
using Expressive.Tokenisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Tokenisation
{
    [TestClass]
    public class ValueTokenExtractorTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructingWithNull()
        {
            _ = new ValueTokenExtractor(null);
        }

        [TestMethod]
        public void TestWithMatchingValue()
        {
            var extractor = new ValueTokenExtractor("match");

            var token = extractor.ExtractToken("match", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("match", token.CurrentToken);
        }

        [TestMethod]
        public void TestWithMatchingValueIgnoringCase()
        {
            var extractor = new ValueTokenExtractor("match");

            var token = extractor.ExtractToken("MaTcH", 0, new Context(ExpressiveOptions.IgnoreCase));

            Assert.IsNotNull(token);
            Assert.AreEqual("match", token.CurrentToken);
        }

        [TestMethod]
        public void TestWithMatchingValueInALargerExpression()
        {
            var extractor = new ValueTokenExtractor("match");

            var token = extractor.ExtractToken("match()", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("match", token.CurrentToken);
        }

        [TestMethod]
        public void TestWithNoMatchingValueByCase()
        {
            var extractor = new ValueTokenExtractor("Match");

            var token = extractor.ExtractToken("match", 0, new Context(ExpressiveOptions.None));

            Assert.IsNull(token);
        }

        [TestMethod]
        public void TestWithNoMatchingValueByLongerToken()
        {
            var extractor = new ValueTokenExtractor("match");

            var token = extractor.ExtractToken("matcher", 0, new Context(ExpressiveOptions.None));

            Assert.IsNull(token);
        }

        [TestMethod]
        public void TestWithNoMatchingValueByShorterExpression()
        {
            var extractor = new ValueTokenExtractor("match");

            var token = extractor.ExtractToken("mat", 0, new Context(ExpressiveOptions.None));

            Assert.IsNull(token);
        }
    }
}
