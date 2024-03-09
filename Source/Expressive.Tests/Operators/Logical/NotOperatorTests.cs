using System;
using Expressive.Expressions.Unary.Logical;
using Expressive.Operators;
using Expressive.Operators.Logical;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Logical
{
    [TestFixture]
    public class NotOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new NotOperator();

        protected override Type ExpectedExpressionType => typeof(NotExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.Not;

        protected override string[] ExpectedTags => new[] { "!", "not" };

        #endregion
    }
}
