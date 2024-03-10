using System;
using Expressive.Expressions.Binary.Relational;
using Expressive.Operators;
using Expressive.Operators.Relational;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Relational
{
    [TestFixture]
    public class GreaterThanOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new GreaterThanOperator();

        protected override Type ExpectedExpressionType => typeof(GreaterThanExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.GreaterThan;

        protected override string[] ExpectedTags => new[] { ">" };

        #endregion
    }
}
