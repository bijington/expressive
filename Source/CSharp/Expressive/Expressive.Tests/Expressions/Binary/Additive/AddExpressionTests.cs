using System.Collections.Generic;
using Expressive.Expressions;
using Expressive.Expressions.Binary.Additive;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Expressions.Binary.Additive
{
    [TestClass]
    public class AddExpressionTests
    {
        [TestMethod]
        public void TestEvaluate()
        {
            var expression = new AddExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)1),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)2),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(3, expression.Evaluate(null));
        }

        [TestMethod]
        public void TestEvaluateWithDifferentSizedArrays()
        {
            var expression = new AddExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)new[] { 1, 2, 3 }),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)new[] { 1 }),
                new Context(ExpressiveOptions.None));

            Assert.IsNull(expression.Evaluate(null));
        }

        [TestMethod]
        public void TestEvaluateWithEmptyLeftArray()
        {
            var expression = new AddExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)new int[0]),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)new[] { 1, 2, 3 }),
                new Context(ExpressiveOptions.None));

            var result = (object[])expression.Evaluate(null);
            Assert.IsTrue(result.Length == 3);

            // TODO: Is this result correct? Currently null + 1 is null so technically yes but should it be the same for an empty array or default to the data types default e.g. default(int)
            Assert.AreEqual(result[0], null);
            Assert.AreEqual(result[1], null);
            Assert.AreEqual(result[2], null);
        }

        [TestMethod]
        public void TestEvaluateWithOneSidedArray()
        {
            var expression = new AddExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)new[] { 1, 2, 3 }),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)2),
                new Context(ExpressiveOptions.None));

            var result = (object[])expression.Evaluate(null);
            Assert.IsTrue(result.Length == 3);
            Assert.AreEqual(result[0], 3);
            Assert.AreEqual(result[1], 4);
            Assert.AreEqual(result[2], 5);
        }

        [TestMethod]
        public void TestEvaluateWithSameSizedArrays()
        {
            var expression = new AddExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)new[] { 1, 2, 3 }),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)new[] { 1, 2, 3 }),
                new Context(ExpressiveOptions.None));

            var result = (object[])expression.Evaluate(null);
            Assert.IsTrue(result.Length == 3);
            Assert.AreEqual(result[0], 2);
            Assert.AreEqual(result[1], 4);
            Assert.AreEqual(result[2], 6);
        }

        [TestMethod]
        public void TestStringAddition()
        {
            var expression = new AddExpression(
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)"1"),
                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)2),
                new Context(ExpressiveOptions.None));

            Assert.AreEqual("12", expression.Evaluate(null));
        }
    }
}
