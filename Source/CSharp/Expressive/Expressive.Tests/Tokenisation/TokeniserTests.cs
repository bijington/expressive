using System;
using System.Linq;
using Expressive.Tokenisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Tokenisation
{
    [TestClass]
    public class TokeniserTests
    {
        [TestMethod]
        public void TestTokeniseFirstMatchWins()
        {
            var context = new Context(ExpressiveOptions.None);

            var tokeniser = new Tokeniser(context, new[]
            {
                CreateTokenExtractor("hello", 0, context),
                CreateTokenExtractor("world", 6, context),
                CreateTokenExtractor("hello world", 0, context)
            });

            var tokens = tokeniser.Tokenise("hello world");

            Assert.IsTrue(tokens.Count == 2);
            Assert.AreEqual("hello", tokens.First().CurrentToken);
            Assert.AreEqual("world", tokens.Last().CurrentToken);

            tokeniser = new Tokeniser(context, new[]
            {
                CreateTokenExtractor("hello world", 0, context),
                CreateTokenExtractor("hello", 0, context),
                CreateTokenExtractor("world", 6, context)
            });

            tokens = tokeniser.Tokenise("hello world");

            Assert.IsTrue(tokens.Count == 1);
            Assert.AreEqual("hello world", tokens.First().CurrentToken);
        }

        [TestMethod]
        public void TestTokeniseWithNull()
        {
            var tokeniser = new Tokeniser(new Context(ExpressiveOptions.None), Enumerable.Empty<ITokenExtractor>());

            Assert.IsNull(tokeniser.Tokenise(null));
            Assert.IsNull(tokeniser.Tokenise(string.Empty));
        }

        [TestMethod]
        public void TestTokeniseWithSimple()
        {
            var context = new Context(ExpressiveOptions.None);

            var tokeniser = new Tokeniser(context, new []
            {
                CreateTokenExtractor("hello", 0, context),
                CreateTokenExtractor("world", 6, context)
            });

            var tokens = tokeniser.Tokenise("hello world");

            Assert.IsTrue(tokens.Count == 2);
            Assert.AreEqual("hello", tokens.First().CurrentToken);
            Assert.AreEqual("world", tokens.Last().CurrentToken);
        }

        [TestMethod]
        public void TestTokeniseWithUnrecognised()
        {
            var context = new Context(ExpressiveOptions.None);

            var tokeniser = new Tokeniser(context, new[]
            {
                CreateTokenExtractor("hel", 0, context),
                CreateTokenExtractor("rld", 6, context)
            });

            var tokens = tokeniser.Tokenise("hello world");

            Assert.IsTrue(tokens.Count == 4);
            Assert.AreEqual("hel", tokens[0].CurrentToken);
            Assert.AreEqual("lo", tokens[1].CurrentToken);
            Assert.AreEqual("wo", tokens[2].CurrentToken);
            Assert.AreEqual("rld", tokens[3].CurrentToken);
        }

        private static ITokenExtractor CreateTokenExtractor(string matchingToken, int expectedIndex, Context context)
        {
            var tokenExtractor = new Mock<ITokenExtractor>();

            tokenExtractor.Setup(t => t.ExtractToken(It.IsAny<string>(), It.IsAny<int>(), context))
                .Returns<string, int, Context>((expression, index, localContext) =>
                {
                    var subExpression = expression.Substring(index, expression.Length - index);

                    return subExpression.StartsWith(matchingToken, StringComparison.Ordinal) ? new Token(matchingToken, index) : null;
                });

            return tokenExtractor.Object;
        }
    }
}