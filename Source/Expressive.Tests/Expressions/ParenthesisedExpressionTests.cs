using Expressive.Exceptions;
using Expressive.Expressions;
using NUnit.Framework;

namespace Expressive.Tests.Expressions
{
    [TestFixture]
    public class ParenthesisedExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new ParenthesisedExpression(MockExpression.ThatEvaluatesTo(1));

            Assert.AreEqual(1, expression.Evaluate(null));
        }

        [Test]
        public void TestNullEvaluate()
        {
            var expression = new ParenthesisedExpression(null);

            Assert.That(() => expression.Evaluate(null), Throws.InstanceOf<MissingParticipantException>());
        }
    }
}
