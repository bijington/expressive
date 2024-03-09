using System;
using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Unary.Logical;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Expressions.Unary.Logical
{
    [TestFixture]
    public class NotExpressionTests
    {
        [Test]
        public void TestNull()
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null));

            Assert.IsNull(expression.Evaluate(null));
        }

        [Test]
        public void TestFalse()
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)false));

            Assert.IsTrue((bool)expression.Evaluate(null));
        }

        [Test]
        public void TestTrue()
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)true));

            Assert.IsFalse((bool)expression.Evaluate(null));
        }

        [Test]
        public void TestInteger()
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1));

            Assert.IsFalse((bool)expression.Evaluate(null));
        }

        [Test]
        public void TestInvalid()
        {
            var expression = new NotExpression(Mock.Of<IExpression>(e =>
                e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)DateTime.Now));

            Assert.That(() => expression.Evaluate(null), Throws.InstanceOf<InvalidCastException>());
        }
    }
}
