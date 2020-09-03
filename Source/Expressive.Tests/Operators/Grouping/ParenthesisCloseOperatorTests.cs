using System;
using Expressive.Expressions;
using Expressive.Operators;
using Expressive.Operators.Grouping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Operators.Grouping
{
    [TestClass]
    public class ParenthesisCloseOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new ParenthesisCloseOperator();

        protected override Type ExpectedExpressionType => typeof(IExpression); // Possibly not the greatest test but it can be any type of expression.

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.ParenthesisClose;

        protected override string[] ExpectedTags => new[] { ")" };

        #endregion
    }
}
