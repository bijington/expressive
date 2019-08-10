using Expressive.Tokenisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Tokenisation
{
    [TestClass]
    public class NumericTokenExtractorTests
    {
        [TestMethod]
        public void TestWithDecimalPlace()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("123.45", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("123.45", token.CurrentToken);
        }

        [TestMethod]
        public void TestWithInteger()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("123", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("123", token.CurrentToken);
        }

        [TestMethod]
        public void TestWithInvalidNumber()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("abc", 0, new Context(ExpressiveOptions.None));

            Assert.IsNull(token);
        }

        [TestMethod]
        public void TestWithMultipleDecimalPlaces()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("123.45.678", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("123.45", token.CurrentToken);
        }
    }
}