using System;
using System.Collections.Generic;
using Expressive.Exceptions;
using Expressive.Expressions;
using Expressive.Functions;
using Expressive.Functions.Conversion;
using NUnit.Framework;
using Moq;

namespace Expressive.Tests.Functions.Conversion
{
    [TestFixture]
    public class DecimalFunctionTests : FunctionBaseTestBase
    {
        [Test]
        public void TestName()
        {
            Assert.AreEqual("Decimal", this.ActualFunction.Name);
        }

        [Test]
        public void TestEvaluateWithDateTime()
        {
            Assert.That(() => this.Evaluate(DateTime.Now), Throws.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void TestEvaluateWithDecimal()
        {
            Assert.AreEqual(12345M, this.Evaluate(12345M));
        }

        [Test]
        public void TestEvaluateWithDouble()
        {
            Assert.AreEqual(12345M, this.Evaluate(12345d));
        }

        [Test]
        public void TestEvaluateWithFloat()
        {
            Assert.AreEqual(12345M, this.Evaluate(12345f));
        }

        [Test]
        public void TestEvaluateWithInteger()
        {
            Assert.AreEqual(12345M, this.Evaluate(12345));
        }

        [Test]
        public void TestEvaluateWithNull()
        {
            Assert.IsNull(this.Evaluate(new object[] { null }));
        }

        [Test]
        public void TestEvaluateWithString()
        {
            Assert.AreEqual(12345M, this.Evaluate("12345"));
        }

        [Test]
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

        [Test]
        public void TestExpectedParameterCount()
        {
            this.AssertException(typeof(ParameterCountMismatchException), "Decimal() takes only 1 argument(s)");
        }

        #region FunctionBaseTests Members

        protected override IFunction ActualFunction => new DecimalFunction();

        #endregion
    }
}
