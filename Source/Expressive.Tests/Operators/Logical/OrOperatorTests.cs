using System;
using Expressive.Expressions.Binary.Logical;
using Expressive.Operators;
using Expressive.Operators.Logical;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Operators.Logical
{
    [TestClass]
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
