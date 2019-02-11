using System;
using Expressive.Expressions.Binary.Relational;
using Expressive.Operators;
using Expressive.Operators.Relational;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Operators.Relational
{
    [TestClass]
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
