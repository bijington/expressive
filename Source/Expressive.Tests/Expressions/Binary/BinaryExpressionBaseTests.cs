using System;
using System.Collections.Generic;
using Expressive.Exceptions;
using Expressive.Expressions;
using Expressive.Expressions.Binary;
using Moq;
using NUnit.Framework;

namespace Expressive.Tests.Expressions.Binary
{
    public class BinaryExpressionBaseTests
    {
        [Test]
        public void TestEvaluateWithNullLeft()
        {
            var expression = new BinaryExpressionBaseImplementation(null, null, new Context(ExpressiveOptions.All));

            Assert.That(() => expression.Evaluate(null), Throws.InstanceOf<MissingParticipantException>());
        }

        [Test]
        public void TestEvaluateWithNullRight()
        {
            var expression = new BinaryExpressionBaseImplementation(new BinaryExpressionBaseImplementation(null, null, new Context(ExpressiveOptions.All)), null, new Context(ExpressiveOptions.All));

            Assert.That(() => expression.Evaluate(null), Throws.InstanceOf<MissingParticipantException>());
        }

        [Test]
        public void TestEvaluateAggregatesWithNullRight()
        {
            Assert.That(
                () => BinaryExpressionBase.EvaluateAggregates(null, null, new Dictionary<string, object>(), null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void TestEvaluateAggregatesWithNullResultSelector()
        {
            Assert.That(
                () => BinaryExpressionBase.EvaluateAggregates(null, Mock.Of<IExpression>(), new Dictionary<string, object>(), null),
                Throws.ArgumentNullException);
        }

        private class BinaryExpressionBaseImplementation : BinaryExpressionBase
        {
            public BinaryExpressionBaseImplementation(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
            {
            }

            protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
            {
                throw new NotImplementedException();
            }
        }
    }
}
