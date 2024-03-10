using System;
using Expressive.Expressions.Binary.Logical;
using Expressive.Operators;
using Expressive.Operators.Logical;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Logical
{
    [TestFixture]
    public class OrOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new OrOperator();

        protected override Type ExpectedExpressionType => typeof(OrExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.Or;

        protected override string[] ExpectedTags => new[] { "||", "or" };

        #endregion
    }
}
