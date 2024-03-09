﻿using System.Collections.Generic;
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
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)"Non null"),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)"Never used"),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual("Non null", expression.Evaluate(null));
        }

        [Test]
        public void TestNullEvaluate()
        {
            var expression = new NullCoalescingExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)null),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)"Now used"),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual("Now used", expression.Evaluate(null));
        }
    }
}
