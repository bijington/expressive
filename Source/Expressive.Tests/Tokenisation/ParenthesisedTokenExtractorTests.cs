using Expressive.Exceptions;
using Expressive.Tokenisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Tokenisation
{
    [TestClass]
    public class ParenthesisedTokenExtractorTests
    {
        [TestMethod, ExpectedException(typeof(MissingTokenException))]
        public void TestWithMissingClosingCharacter()
        {
            var extractor = new ParenthesisedTokenExtractor('[', ']');

            extractor.ExtractToken("[abc", 0, new Context(ExpressiveOptions.None));
        }

        [TestMethod]
        public void TestWithEscapedCharacters()
        {
            var extractor = new ParenthesisedTokenExtractor('"');

            var token = extractor.ExtractToken(@"""a\\"" ", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual(@"""a\\""", token.CurrentToken);
        }

        [TestMethod]
        public void TestWithNoMatch()
        {
            var extractor = new ParenthesisedTokenExtractor('|');

            var token = extractor.ExtractToken("abc|", 0, new Context(ExpressiveOptions.None));

            Assert.IsNull(token);
        }

        [TestMethod]
        public void TestWithValidParenthesis()
        {
            var extractor = new ParenthesisedTokenExtractor('[', ']');

            var token = extractor.ExtractToken("[abc]", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("[abc]", token.CurrentToken);
        }

        [TestMethod]
        public void TestWithValidParenthesisSingle()
        {
            var extractor = new ParenthesisedTokenExtractor('|');

            var token = extractor.ExtractToken("|abc|", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("|abc|", token.CurrentToken);
        }
    }
}