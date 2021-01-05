using Expressive.Tokenisation;
using NUnit.Framework;

namespace Expressive.Tests.Tokenisation
{
    public static class KeywordTokenExtractorTests
    {
        [Test]
        public static void TestConstructingWithNull()
        {
            Assert.That(() => new KeywordTokenExtractor(null), Throws.ArgumentNullException);
        }

        [Test]
        public static void TestMatch()
        {
            var extractor = new KeywordTokenExtractor(new[] { "matching" });

            var token = extractor.ExtractToken("matching()", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("matching", token.CurrentToken);
        }

        [Test]
        public static void TestMatchByCase()
        {
            var extractor = new KeywordTokenExtractor(new[] { "Matching" });

            var token = extractor.ExtractToken("matching()", 0, new Context(ExpressiveOptions.IgnoreCaseForParsing));

            Assert.IsNotNull(token);
            Assert.AreEqual("matching", token.CurrentToken);
        }

        [Test]
        public static void TestNoMatch()
        {
            var extractor = new KeywordTokenExtractor(new[] { "matching" });

            Assert.IsNull(extractor.ExtractToken("missing()", 0, new Context(ExpressiveOptions.None)));
        }

        [Test]
        public static void TestNoMatchByCase()
        {
            var extractor = new KeywordTokenExtractor(new[] { "Matching" });

            Assert.IsNull(extractor.ExtractToken("matching()", 0, new Context(ExpressiveOptions.None)));
        }
    }
}