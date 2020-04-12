using System;
using System.Collections.Generic;
using Expressive.Exceptions;
using Expressive.Expressions;
using Expressive.Functions;
using Expressive.Functions.Conversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests.Functions.Conversion
{
    [TestClass]
    public class DecimalFunctionTests : FunctionBaseTests
    {
        [TestMethod]
        public void TestName()
        {
            Assert.AreEqual("Decimal", this.Function.Name);
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void TestEvaluateWithDateTime()
        {
            this.Evaluate(DateTime.Now);
        }

        [TestMethod]
        public void TestEvaluateWithDecimal()
        {
            Assert.AreEqual(12345M, this.Evaluate(12345M));
        }

        [TestMethod]
        public void TestEvaluateWithDouble()
        {
            Assert.AreEqual(12345M, this.Evaluate(12345d));
        }

        [TestMethod]
        public void TestEvaluateWithFloat()
        {
            Assert.AreEqual(12345M, this.Evaluate(12345f));
        }

        [TestMethod]
        public void TestEvaluateWithInteger()
        {
            Assert.AreEqual(12345M, this.Evaluate(12345));
        }

        [TestMethod]
        public void TestEvaluateWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [TestMethod]
        public void TestEvaluateWithString()
        {
            Assert.AreEqual(12345M, this.Evaluate("12345"));
        }

        [TestMethod]
        public void TestEvaluateWithLong()
        {
            var function = new DecimalFunction();

            const long longValue = 12345L;

            var result = function.Evaluate(new[]
                {
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object)longValue)
                },
                new Context(ExpressiveOptions.None));

            Assert.AreEqual(12345M, result);
        }

        [TestMethod]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "Decimal() takes only 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction Function => new DecimalFunction();

        #endregion
    }
}
