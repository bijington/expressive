using System;
using System.Collections.Generic;
using System.Linq;
using Expressive.Exceptions;
using Expressive.Expressions;
using Expressive.Functions;
using Expressive.Operators;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Expressive.Tests
{
    public static class ContextTests
    {
        [Test]
        public static void TestOperatorNamesAreOrderedDescendingByLength()
        {
            var context = new Context(ExpressiveOptions.None);

            var operatorNames = context.OperatorNames.ToArray();

            for (var i = 1; i < operatorNames.Length; i++)
            {
                var previous = operatorNames[i - 1];
                var current = operatorNames[i];

                Assert.That(current.Length, Is.LessThanOrEqualTo(previous.Length));
            }
        }

        [Test]
        public static void TestRegisterFunction()
        {
            const string functionName = "SomethingNotUserBefore";
            var context = new Context(ExpressiveOptions.None);

            var function = Mock.Of<IFunction>(f => f.Name == functionName);
            context.RegisterFunction(function);

            Assert.That(context.FunctionNames, Does.Contain(functionName));
        }

        [Test]
        public static void TestRegisterFunctionInline()
        {
            const string functionName = "SomethingNotUserBefore";
            var context = new Context(ExpressiveOptions.None);

            context.RegisterFunction(functionName, (expressions, objects) => true);

            Assert.That(context.FunctionNames, Does.Contain(functionName));
        }

        [Test]
        public static void TestRegisterFunctionInlineWithDuplicate()
        {
            const string functionName = "If";
            var context = new Context(ExpressiveOptions.None);

            Assert.That(
                () => context.RegisterFunction(functionName, (expressions, objects) => true),
                Throws.InstanceOf<FunctionNameAlreadyRegisteredException>());
        }

        [Test]
        public static void TestRegisterFunctionWithDuplicate()
        {
            const string functionName = "If";
            var context = new Context(ExpressiveOptions.None);

            var function = Mock.Of<IFunction>(f => f.Name == functionName);
            Assert.That(
                () => context.RegisterFunction(function),
                Throws.InstanceOf<FunctionNameAlreadyRegisteredException>());
        }

        [Test]
        public static void TestRegisterFunctionWithForce()
        {
            const string functionName = "If";
            var context = new Context(ExpressiveOptions.None);
            var customIfCount = 0;

            var function = new Mock<IFunction>();
            function.Setup(f => f.Name).Returns(functionName);
            function.Setup(f => f.Evaluate(It.IsAny<IExpression[]>(), context))
                .Callback<IExpression[], Context>((arguments, c) =>
                {
                    customIfCount += 1;
                });

            // Register the new function.
            context.RegisterFunction(function.Object, true);

            Assert.That(context.FunctionNames, Does.Contain(functionName));
            context.TryGetFunction(functionName, out var ifFunction);
            ifFunction.Invoke(new IExpression[0], new Dictionary<string, object>());
            Assert.That(customIfCount, Is.EqualTo(1));
        }

        [Test]
        public static void TestRegisterOperator()
        {
            const string tag = "@";
            var context = new Context(ExpressiveOptions.None);

            var op = Mock.Of<IOperator>(f => f.Tags == new [] {tag});
            context.RegisterOperator(op);

            Assert.That(context.OperatorNames, Does.Contain(tag));
        }

        [Test]
        public static void TestRegisterOperatorWithDuplicate()
        {
            const string tag = "+";
            var context = new Context(ExpressiveOptions.None);

            var op = Mock.Of<IOperator>(f => f.Tags == new[] { tag });

            Assert.That(
                () => context.RegisterOperator(op),
                Throws.InstanceOf<OperatorNameAlreadyRegisteredException>());
        }

        [Test]
        public static void TestStringComparer()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.That(StringComparer.Ordinal, Is.EqualTo(context.StringComparer));
        }

        [Test]
        public static void TestStringComparerIgnoreCase()
        {
            var context = new Context(ExpressiveOptions.IgnoreCase);

            Assert.That(StringComparer.OrdinalIgnoreCase, Is.EqualTo(context.StringComparer));
        }

        [Test]
        public static void TestStringComparison()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.That(StringComparison.Ordinal, Is.EqualTo(context.StringComparison));
        }

        [Test]
        public static void TestStringComparisonIgnoreCase()
        {
            var context = new Context(ExpressiveOptions.IgnoreCase);

            Assert.That(StringComparison.OrdinalIgnoreCase, Is.EqualTo(context.StringComparison));
        }

        [Test]
        public static void TestTryGetFunctionWithMatch()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.That(context.TryGetFunction("If", out var function), Is.True);
            Assert.That(function, Is.Not.Null);
        }

        [Test]
        public static void TestTryGetFunctionWithNoMatch()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.That(context.TryGetFunction("Non existent function", out var function), Is.False);
            Assert.That(function, Is.Null);
        }

        [Test]
        public static void TestTryGetOperatorWithMatch()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.That(context.TryGetOperator("+", out var op), Is.True);
            Assert.That(op, Is.Not.Null);
        }

        [Test]
        public static void TestTryGetOperatorWithNoMatch()
        {
            var context = new Context(ExpressiveOptions.None);

            Assert.That(context.TryGetOperator("~", out var op), Is.False);
            Assert.That(op, Is.Null);
        }

        [Test]
        public static void TestUnregisterFunction()
        {
            const string functionName = "If";
            var context = new Context(ExpressiveOptions.None);

            Assert.That(context.TryGetFunction(functionName, out var function), Is.True);
            Assert.That(function, Is.Not.Null);

            context.UnregisterFunction(functionName);

            Assert.That(context.TryGetFunction(functionName, out function), Is.False);
            Assert.That(function, Is.Null);
        }

        [Test]
        public static void TestUnregisterOperator()
        {
            const string tag = "+";
            var context = new Context(ExpressiveOptions.None);

            Assert.That(context.TryGetOperator(tag, out var op), Is.True);
            Assert.That(op, Is.Not.Null);

            context.UnregisterOperator(tag);

            Assert.That(context.TryGetOperator(tag, out op), Is.False);
            Assert.That(op, Is.Null);
        }
    }
}
