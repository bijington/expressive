using System;
using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Unary.Logical;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Unary.Logical
{
    [TestClass]
    public class NotExpressionTests
    {
        [TestMethod]
        public void TestNull()
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null));

            Assert.IsNull(expression.Evaluate(null));
        }

        [TestMethod]
        public void TestFalse()
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)false));

            Assert.IsTrue((bool)expression.Evaluate(null));
        }

        [TestMethod]
        public void TestTrue()
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)true));

            Assert.IsFalse((bool)expression.Evaluate(null));
        }

        [TestMethod]
        public void TestInteger()
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1));

            Assert.IsFalse((bool)expression.Evaluate(null));
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void TestInvalid()
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)DateTime.Now));

            Assert.IsFalse((bool)expression.Evaluate(null));
        }
    }
}
