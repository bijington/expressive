using System;
using Expressive.Expressions.Binary.Logical;
using Expressive.Operators;
using Expressive.Operators.Logical;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Operators.Logical
{
    [TestClass]
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
