using System;
using Expressive.Expressions.Binary.Relational;
using Expressive.Operators;
using Expressive.Operators.Relational;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Relational
{
    [TestFixture]
    public class LessThanOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new LessThanOperator();

        protected override Type ExpectedExpressionType => typeof(LessThanExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.LessThan;

        protected override string[] ExpectedTags => new[] { "<" };

        #endregion
    }
}
