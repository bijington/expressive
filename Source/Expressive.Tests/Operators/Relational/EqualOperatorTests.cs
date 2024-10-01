using System;
using Expressive.Expressions.Binary.Relational;
using Expressive.Operators;
using Expressive.Operators.Relational;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Relational
{
    [TestFixture]
    public class EqualOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new EqualOperator();

        protected override Type ExpectedExpressionType => typeof(EqualExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.Equal;

        protected override string[] ExpectedTags => new[] { "=", "==" };

        #endregion
    }
}
