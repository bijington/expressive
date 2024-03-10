using System;
using Expressive.Expressions.Binary.Relational;
using Expressive.Operators;
using Expressive.Operators.Relational;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Relational
{
    [TestFixture]
    public class LessThanOrEqualOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new LessThanOrEqualOperator();

        protected override Type ExpectedExpressionType => typeof(LessThanOrEqualExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.LessThanOrEqual;

        protected override string[] ExpectedTags => new[] { "<=" };

        #endregion
    }
}
