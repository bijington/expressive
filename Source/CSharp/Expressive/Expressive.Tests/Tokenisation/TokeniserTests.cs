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
            var tokenExtractor = new Mock<ITokenExtractor>();
            tokenExtractor.Setup(t => t.ExtractToken(It.IsAny<string>(), 0, context)).Returns<Token>(null);

            var tokeniser = new Tokeniser(context, new [] {tokenExtractor.Object});

            var a = tokeniser.Tokenise("1~2");
        }
    }
}