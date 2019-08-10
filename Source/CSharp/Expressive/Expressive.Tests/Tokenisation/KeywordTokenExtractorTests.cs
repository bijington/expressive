using System;
using Expressive.Tokenisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Tokenisation
{
    [TestClass]
    public class KeywordTokenExtractorTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestConstructingWithNull()
        {
            _ = new KeywordTokenExtractor(null);
        }

        [TestMethod]
        public void TestMatch()
        {
            var extractor = new KeywordTokenExtractor(new[] { "matching" });

            var token = extractor.ExtractToken("matching()", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("matching", token.CurrentToken);
        }

        [TestMethod]
        public void TestMatchByCase()
        {
            var extractor = new KeywordTokenExtractor(new[] { "Matching" });

            var token = extractor.ExtractToken("matching()", 0, new Context(ExpressiveOptions.IgnoreCase));

            Assert.IsNotNull(token);
            Assert.AreEqual("matching", token.CurrentToken);
        }

        [TestMethod]
        public void TestNoMatch()
        {
            var extractor = new KeywordTokenExtractor(new[] { "matching" });

            Assert.IsNull(extractor.ExtractToken("missing()", 0, new Context(ExpressiveOptions.None)));
        }

        [TestMethod]
        public void TestNoMatchByCase()
        {
            var extractor = new KeywordTokenExtractor(new[] { "Matching" });

            Assert.IsNull(extractor.ExtractToken("matching()", 0, new Context(ExpressiveOptions.None)));
        }
    }
}