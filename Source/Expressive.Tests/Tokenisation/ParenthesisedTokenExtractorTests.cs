using Expressive.Exceptions;
using Expressive.Tokenisation;
using NUnit.Framework;

namespace Expressive.Tests.Tokenisation
{
    public class ParenthesisedTokenExtractorTests
    {
        [Test]
        public void TestWithMissingClosingCharacter()
        {
            var extractor = new ParenthesisedTokenExtractor('[', ']');

            Assert.That(() => extractor.ExtractToken("[abc", 0, new Context(ExpressiveOptions.None)), Throws.InstanceOf<MissingTokenException>());
        }

        [Test]
        public void TestWithNoMatch()
        {
            var extractor = new ParenthesisedTokenExtractor('|');

            var token = extractor.ExtractToken("abc|", 0, new Context(ExpressiveOptions.None));

            Assert.IsNull(token);
        }

        [Test]
        public void TestWithValidParenthesis()
        {
            var extractor = new ParenthesisedTokenExtractor('[', ']');

            var token = extractor.ExtractToken("[abc]", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("[abc]", token.CurrentToken);
        }

        [TestCase('"', @"""a\\"" ", @"""a\\""")]
        [TestCase('|', "|abc|", "|abc|")]
        [TestCase('\'', @"'hh\:mm'", @"'hh\:mm'")]
        [TestCase('\'', @"'(^\+?[0-9]{​10,15}​)$'", @"'(^\+?[0-9]{​10,15}​)$'")]
        public static void TestValidScenarios(char character, string input, string output)
        {
            var extractor = new ParenthesisedTokenExtractor(character);

            var token = extractor.ExtractToken(input, 0, new Context(ExpressiveOptions.None));

            Assert.That(token, Is.Not.Null);
            Assert.That(token.CurrentToken, Is.EqualTo(output));
        }
    }
}