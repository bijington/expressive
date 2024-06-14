using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expressive.Expressions;
using Expressive.Operators;
using Expressive.Operators.Grouping;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Grouping
{
    [TestFixture]
    public class ParenthesisOpenOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new ParenthesisOpenOperator();

        protected override Type ExpectedExpressionType => typeof(IExpression); // Possibly not the greatest test but it can be any type of expression.

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.ParenthesisOpen;

        protected override string[] ExpectedTags => new[] { "(" };

        [Test]
        public override void TestGetCaptiveTokens()
        {
            var op = this.Operator;

            var token = new Token("(", 1);
            var tokenArray = new[]
            {
                new Token("1", 2),
                new Token("*", 3),
                new Token("2", 4),
                new Token(")", 5),
                new Token("+", 6),
                new Token("(", 7),
                new Token("1", 8),
                new Token("*", 9),
                new Token("3", 10),
                new Token(")", 11)
            };

            var remainingTokens = new Queue<Token>(tokenArray);

            var captive = op.GetCaptiveTokens(new Token("1", 0), token, remainingTokens);

            // We only expect the first set of parenthesis to be captured (e.g. '(1*2)').
            Assert.AreEqual(5, captive.Length);

            for (var i = 0; i < captive.Length; i++)
            {
                var expected = i == 0 ? token : tokenArray[i - 1];
                var actual = captive[i];

                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        public override void TestGetInnerCaptiveTokens()
        {
            var op = this.Operator;

            var tokens = new[]
            {
                new Token("(", 1),
                new Token("(", 2),
                new Token("1", 3),
                new Token("-", 4),
                new Token("4", 5),
                new Token(")", 6),
                new Token(")", 7)
            };

            // We only care about what is inside the parentheses.
            Assert.AreEqual(tokens.Length - 2, op.GetInnerCaptiveTokens(tokens).Length);
        }

        #endregion

        [Test]
        public void TestComplexGetCaptiveTokens()
        {
            var op = this.Operator;

            var token = new Token("(", 1);
            var tokenArray = new[]
            {
                new Token("(", 2),
                new Token("1", 3),
                new Token("*", 4),
                new Token("2", 5),
                new Token(")", 6),
                new Token("+", 7),
                new Token("1.5", 8),
                new Token(")", 9), // Expect up to here.
                new Token("/", 10),
                new Token("(", 11),
                new Token("25", 12),
                new Token("*", 13),
                new Token("3", 14),
                new Token(")", 15),
            };

            var remainingTokens = new Queue<Token>(tokenArray);

            var captive = op.GetCaptiveTokens(new Token("1", 0), token, remainingTokens);

            // We only expect the following: '((1*2)+1.5)'.
            Assert.AreEqual(9, captive.Length);

            for (var i = 0; i < captive.Length; i++)
            {
                var expected = i == 0 ? token : tokenArray[i - 1];
                var actual = captive[i];

                Assert.AreEqual(expected, actual);
            }
        }
    }
}
