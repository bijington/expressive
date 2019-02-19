using System;
using Expressive.Expressions.Binary.Relational;
using Expressive.Operators;
using Expressive.Operators.Relational;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Operators.Relational
{
    [TestClass]
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
