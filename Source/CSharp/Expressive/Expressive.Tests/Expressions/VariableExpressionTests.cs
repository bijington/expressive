using System;
using System.Collections.Generic;
using Expressive.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions
{
    [TestClass]
    public class VariableExpressionTests
    {
        [TestMethod]
        public void TestWithExpressionVariableSupplied()
        {
            var expression = new VariableExpression("pi");

            var variable = Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)Math.PI);

            Assert.AreEqual(Math.PI, expression.Evaluate(new Dictionary<string, object>
            {
                ["pi"] = variable
            }));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException), "The variable 'pie' has not been supplied.")]
        public void TestWithoutVariableSupplied()
        {
            var expression = new VariableExpression("pie");

            Assert.AreEqual(Math.PI, expression.Evaluate(new Dictionary<string, object>
            {
                ["pi"] = Math.PI
            }));
        }

        [TestMethod]
        public void TestWithVariableSupplied()
        {
            var expression = new VariableExpression("pi");

            Assert.AreEqual(Math.PI, expression.Evaluate(new Dictionary<string, object>
            {
                ["pi"] = Math.PI
            }));
        }
    }
}
