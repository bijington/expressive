using System.Collections.Generic;
using Expressive.Exceptions;
using Expressive.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions
{
    [TestClass]
    public class ParenthesisedExpressionTests
    {
        [TestMethod]
        public void TestEvaluate()
        {
            var mockExpression = Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1);

            var expression = new ParenthesisedExpression(mockExpression);

            Assert.AreEqual(1, expression.Evaluate(null));
        }

        [TestMethod, ExpectedException(typeof(MissingParticipantException), "Missing contents inside ().")]
        public void TestNullEvaluate()
        {
            var expression = new ParenthesisedExpression(null);

            Assert.AreEqual(1, expression.Evaluate(null));
        }
    }
}
