using System;
using Expressive.Expressions.Binary.Logical;
using Expressive.Operators;
using Expressive.Operators.Logical;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Logical
{
    [TestFixture]
    public class AndOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new AndOperator();

        protected override Type ExpectedExpressionType => typeof(AndExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.And;

        protected override string[] ExpectedTags => new[] { "&&", "and" };

        #endregion
    }
}
