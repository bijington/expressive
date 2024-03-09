using System.Collections.Generic;
using Expressive.Exceptions;
using Expressive.Expressions;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Expressions
{
    [TestFixture]
    public class ParenthesisedExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var mockExpression = Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1);

            var expression = new ParenthesisedExpression(mockExpression);

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
