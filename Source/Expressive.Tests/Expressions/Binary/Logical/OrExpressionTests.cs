using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Logical;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Logical
{
    [TestFixture]
    public class OrExpressionTests
    {
        [Test]
        public void TestBothTrueEvaluate()
        {
            var expression = new OrExpression(
                MockExpression.ThatEvaluatesTo(true),
                MockExpression.ThatEvaluatesTo(true),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [Test]
        public void TestLeftTrueEvaluate()
        {
            var expression = new OrExpression(
                MockExpression.ThatEvaluatesTo(true),
                MockExpression.ThatEvaluatesTo(false),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [Test]
        public void TestNeitherTrueEvaluate()
        {
            var expression = new OrExpression(
                MockExpression.ThatEvaluatesTo(false),
                MockExpression.ThatEvaluatesTo(false),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(false, expression.Evaluate(null));
        }

        [Test]
        public void TestRightTrueEvaluate()
        {
            var expression = new OrExpression(
                MockExpression.ThatEvaluatesTo(false),
                MockExpression.ThatEvaluatesTo(true),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(true, expression.Evaluate(null));
        }

        [Test]
        public void TestShortCircuit()
        {
            var rightHandMock = new Mock<IExpression>();

            var expression = new OrExpression(
                MockExpression.ThatEvaluatesTo(true),
                rightHandMock.Object,
                new Context(ExpressiveOptions.None));

            expression.Evaluate(null);

            rightHandMock.Verify(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()), Times.Never);
        }
    }
}
