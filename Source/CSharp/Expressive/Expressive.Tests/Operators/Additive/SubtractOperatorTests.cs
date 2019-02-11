using Expressive.Expressions;
using Expressive.Expressions.Binary.Additive;
using Expressive.Expressions.Unary.Additive;
using Expressive.Operators;
using Expressive.Operators.Additive;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Expressive.Tests.Operators.Additive
{
    //[TestClass]
    //public class SubtractOperatorTests
    //{
    //    [TestMethod]
    //    public void TestTags()
    //    {
    //        var op = new SubtractOperator();

    //        Assert.AreEqual(2, op.Tags.Length);
    //        Assert.AreEqual("-", op.Tags.First());
    //        Assert.AreEqual("\u2212", op.Tags.Last());
    //    }

    //    [TestMethod]
    //    public void TestBuildExpressionForUnaryWithLeftHandExpression()
    //    {
    //        var op = new SubtractOperator();

    //        var expression = op.BuildExpression(
    //            new Token("-", 1),
    //            new[]
    //            {
    //                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null),
    //                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
    //            },
    //            ExpressiveOptions.None);

    //        Assert.IsInstanceOfType(expression, typeof(MinusExpression));
    //    }

    //    [TestMethod]
    //    public void TestBuildExpressionForUnaryWithRightHandExpression()
    //    {
    //        var op = new SubtractOperator();

    //        var expression = op.BuildExpression(
    //            new Token("-", 1),
    //            new[]
    //            {
    //                null,
    //                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
    //            },
    //            ExpressiveOptions.None);

    //        Assert.IsInstanceOfType(expression, typeof(MinusExpression));
    //    }

    //    [TestMethod]
    //    public void TestBuildExpressionForBinary()
    //    {
    //        var op = new SubtractOperator();

    //        var expression = op.BuildExpression(
    //            new Token("1", 1),
    //            new[]
    //            {
    //                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null),
    //                Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
    //            },
    //            ExpressiveOptions.None);

    //        Assert.IsInstanceOfType(expression, typeof(SubtractExpression));
    //    }

    //    [TestMethod]
    //    public void TestCanGetCaptiveTokens()
    //    {
    //        var op = new SubtractOperator();

    //        Assert.IsTrue(op.CanGetCaptiveTokens(new Token("1", 0), new Token("-", 1), new Queue<Token>()));
    //    }

    //    [TestMethod]
    //    public void TestGetCaptiveTokens()
    //    {
    //        var op = new SubtractOperator();

    //        var token = new Token("-", 1);

    //        Assert.AreEqual(token, op.GetCaptiveTokens(new Token("1", 0), token, new Queue<Token>()).Single());
    //    }

    //    [TestMethod]
    //    public void TestGetInnerCaptiveTokens()
    //    {
    //        var op = new SubtractOperator();

    //        var tokens = new[]
    //        {
    //            new Token("+", 1),
    //            new Token("(", 2),
    //            new Token("1", 3),
    //            new Token("-", 4),
    //            new Token("4", 5),
    //            new Token(")", 6),
    //        };

    //        Assert.AreEqual(tokens.Length - 1, op.GetInnerCaptiveTokens(tokens).Length);
    //    }

    //    [TestMethod]
    //    public void TestGetPrecedenceForUnary()
    //    {
    //        var op = new SubtractOperator();

    //        Assert.AreEqual(OperatorPrecedence.UnaryMinus, op.GetPrecedence(null));
    //    }

    //    [TestMethod]
    //    public void TestGetPrecedenceForBinary()
    //    {
    //        var op = new SubtractOperator();

    //        Assert.AreEqual(OperatorPrecedence.Subtract, op.GetPrecedence(new Token("1", 1)));
    //    }
    //}
    [TestClass]
    public class SubtractOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new SubtractOperator();

        protected override Type ExpectedExpressionType => typeof(SubtractExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.UnaryMinus;

        protected override string[] ExpectedTags => new[] { "-", "\u2212" };

        [TestMethod]
        public override void TestGetInnerCaptiveTokens()
        {
            var op = this.Operator;

            var tokens = new[]
            {
                new Token("+", 1),
                new Token("(", 2),
                new Token("1", 3),
                new Token("-", 4),
                new Token("4", 5),
                new Token(")", 6),
            };

            Assert.AreEqual(tokens.Length - 1, op.GetInnerCaptiveTokens(tokens).Length);
        }

        #endregion

        [TestMethod]
        public void TestBuildExpressionForUnaryWithLeftHandExpression()
        {
            var op = new SubtractOperator();

            var expression = op.BuildExpression(
                new Token("+", 1),
                new[]
                {
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null),
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
                },
                ExpressiveOptions.None);

            Assert.IsInstanceOfType(expression, typeof(MinusExpression));
        }

        [TestMethod]
        public void TestBuildExpressionForUnaryWithRightHandExpression()
        {
            var op = new SubtractOperator();

            var expression = op.BuildExpression(
                new Token("+", 1),
                new[]
                {
                    null,
                    Mock.Of<IExpression>(e => e.Evaluate(It.IsAny<IDictionary<string, object>>()) == (object) null)
                },
                ExpressiveOptions.None);

            Assert.IsInstanceOfType(expression, typeof(MinusExpression));
        }

        [TestMethod]
        public void TestGetPrecedenceForUnary()
        {
            var op = new SubtractOperator();

            Assert.AreEqual(OperatorPrecedence.UnaryMinus, op.GetPrecedence(null));
        }

        [TestMethod]
        public void TestGetPrecedenceForBinary()
        {
            var op = new SubtractOperator();

            Assert.AreEqual(OperatorPrecedence.Subtract, op.GetPrecedence(new Token("1", 1)));
        }
    }
}
