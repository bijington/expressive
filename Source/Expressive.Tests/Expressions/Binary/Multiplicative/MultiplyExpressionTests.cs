﻿using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Multiplicative;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Multiplicative
{
    [TestFixture]
    public class MultiplyExpressionTests
    {
        [Test]
        public void TestEvaluate()
        {
            var expression = new MultiplyExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)5),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)2),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(10, expression.Evaluate(null));
        }
    }
}
