using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Conditional;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Conditional
{
    [TestFixture]
    public class NullCoalescingExpressionTests
    {
        [Test]
        public void TestNotNullEvaluate()
        {
            var expression = new NullCoalescingExpression(
                MockExpression.ThatEvaluatesTo("Non null"),
                MockExpression.ThatEvaluatesTo("Never used"),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual("Non null", expression.Evaluate(null));
        }

        [Test]
        public void TestNullEvaluate()
        {
            var expression = new NullCoalescingExpression(
                MockExpression.ThatEvaluatesTo(null),
                MockExpression.ThatEvaluatesTo("Now used"),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual("Now used", expression.Evaluate(null));
        }
    }
}
