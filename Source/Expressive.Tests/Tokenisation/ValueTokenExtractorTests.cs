using Expressive.Tokenisation;
using NUnit.Framework;

namespace Expressive.Tests.Tokenisation
{
    public static class ValueTokenExtractorTests
    {
        [Test]
        public static void TestConstructingWithNull()
        {
            Assert.That(() => new ValueTokenExtractor(null), Throws.ArgumentNullException);
        }

        [Test]
        public static void TestWithMatchingValue()
        {
            var extractor = new ValueTokenExtractor("match");

            var token = extractor.ExtractToken("match", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("match", token.CurrentToken);
        }

        [TestCase(ExpressiveOptions.IgnoreCaseForParsing)]
#pragma warning disable 618 // As it is our own warning this is safe enough until we actually get rid
        [TestCase(ExpressiveOptions.IgnoreCase)]
#pragma warning restore 618
        public static void TestWithMatchingValueIgnoringCase(ExpressiveOptions option)
        {
            var extractor = new ValueTokenExtractor("match");

            var token = extractor.ExtractToken("MaTcH", 0, new Context(option));

            Assert.IsNotNull(token);
            Assert.AreEqual("match", token.CurrentToken);
        }

        [Test]
        public static void TestWithMatchingValueInALargerExpression()
        {
            var extractor = new ValueTokenExtractor("match");

            var token = extractor.ExtractToken("match()", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("match", token.CurrentToken);
        }

        [TestCase(ExpressiveOptions.IgnoreCaseForEquality)]
        [TestCase(ExpressiveOptions.None)]
        public static void TestWithNoMatchingValueByCase(ExpressiveOptions option)
        {
            var extractor = new ValueTokenExtractor("Match");

            var token = extractor.ExtractToken("match", 0, new Context(option));

            Assert.IsNull(token);
        }

        [Test]
        public static void TestWithNoMatchingValueByLongerToken()
        {
            var extractor = new ValueTokenExtractor("match");

            var token = extractor.ExtractToken("matcher", 0, new Context(ExpressiveOptions.None));

            Assert.IsNull(token);
        }

        [Test]
        public static void TestWithNoMatchingValueByShorterExpression()
        {
            var extractor = new ValueTokenExtractor("match");

            var token = extractor.ExtractToken("mat", 0, new Context(ExpressiveOptions.None));

            Assert.IsNull(token);
        }
    }
}
