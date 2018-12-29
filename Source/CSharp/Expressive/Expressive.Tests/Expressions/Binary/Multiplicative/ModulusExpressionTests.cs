using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Multiplicative;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Multiplicative
{
    [TestClass]
    public class ModulusExpressionTests
    {
        [TestMethod]
        public void TestEvaluate()
        {
            var expression = new ModulusExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)5),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)2),
                ExpressiveOptions.None);

            Assert.AreEqual(1, expression.Evaluate(null));
        }
    }
}
