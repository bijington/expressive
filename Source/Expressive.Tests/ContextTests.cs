using System;
using System.Linq;
using Expressive.Exceptions;
using Expressive.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Expressive.Tests
{
    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void TestOperatorNamesAreOrderedDescendingByLength()
        {
            var context = new Context(ExpressiveOptions.None);

            var operatorNames = context.OperatorNames.ToArray();

            for (var i = 1; i < operatorNames.Length; i++)
            {
                var previous = operatorNames[i - 1];
                var current = operatorNames[i];

                Assert.IsTrue(current.Length <= previous.Length);
            }
        }

        [TestMethod]
        public void TestRegisterFunction()
        {
            const string functionName = "SomethingNotUserBefore";
            var context = new Context(ExpressiveOptions.None);

            var function = Mock.Of<IFunction>(f => f.Name == functionName);
            context.RegisterFunction(function);

            Assert.IsTrue(context.FunctionNames.Contains(functionName));
        }

        [TestMethod]
        public void TestRegisterFunctionInline()
        {
            const string functionName = "SomethingNotUserBefore";
            var context = new Context(ExpressiveOptions.None);

            context.RegisterFunction(functionName, (expressions, objects) => true);

            Assert.IsTrue(context.FunctionNames.Contains(functionName));
        }

        [TestMethod, ExpectedException(typeof(FunctionNameAlreadyRegisteredException))]
        public void TestRegisterFunctionInlineWithDuplicate()
        {
            const string functionName = "If";
            var context = new Context(ExpressiveOptions.None);

            context.RegisterFunction(functionName, (expressions, objects) => true);
        }

        [TestMethod, ExpectedException(typeof(FunctionNameAlreadyRegisteredException))]
        public void TestRegisterFunctionWithDuplicate()
        {
            const string functionName = "If";
            var context = new Context(ExpressiveOptions.None);

            var function = Mock.Of<IFunction>(f => f.Name == functionName);
            context.RegisterFunction(function);
        }

        [TestMethod]
        public void TestStringComparer()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.AreEqual(StringComparer.Ordinal, context.StringComparer);
        }

        [TestMethod]
        public void TestStringComparerIgnoreCase()
        {
            var context = new Context(ExpressiveOptions.IgnoreCase);

            Assert.AreEqual(StringComparer.OrdinalIgnoreCase, context.StringComparer);
        }

        [TestMethod]
        public void TestStringComparison()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.AreEqual(StringComparison.Ordinal, context.StringComparison);
        }

        [TestMethod]
        public void TestStringComparisonIgnoreCase()
        {
            var context = new Context(ExpressiveOptions.IgnoreCase);

            Assert.AreEqual(StringComparison.OrdinalIgnoreCase, context.StringComparison);
        }

        [TestMethod]
        public void TestTryGetFunctionWithMatch()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.IsTrue(context.TryGetFunction("If", out var function));
            Assert.IsNotNull(function);
        }

        [TestMethod]
        public void TestTryGetFunctionWithNoMatch()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.IsFalse(context.TryGetFunction("Non existent function", out var function));
            Assert.IsNull(function);
        }

        [TestMethod]
        public void TestTryGetOperatorWithMatch()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.IsTrue(context.TryGetOperator("+", out var op));
            Assert.IsNotNull(op);
        }

        [TestMethod]
        public void TestTryGetOperatorWithNoMatch()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.IsFalse(context.TryGetOperator("~", out var op));
            Assert.IsNull(op);
        }
    }
}
