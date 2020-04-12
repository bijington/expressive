using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Additive;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Additive
{
    [TestClass]
    public class SubtractExpressionTests
    {
        [TestMethod]
        public void TestEvaluate()
        {
            var expression = new SubtractExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)2),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(-1, expression.Evaluate(null));
        }
    }
}
