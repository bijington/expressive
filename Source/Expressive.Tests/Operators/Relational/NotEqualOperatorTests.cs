using System;
using Expressive.Expressions.Binary.Relational;
using Expressive.Operators;
using Expressive.Operators.Relational;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Relational
{
    [TestFixture]
    public class NotEqualOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new NotEqualOperator();

        protected override Type ExpectedExpressionType => typeof(NotEqualExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.NotEqual;

        protected override string[] ExpectedTags => new[] { "!=", "<>" };

        #endregion
    }
}
