using System;
using Expressive.Expressions.Unary.Bitwise;
using Expressive.Operators;
using Expressive.Operators.Bitwise;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Operators.Bitwise
{
    [TestClass]
    public class BitwiseNotOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new BitwiseNotOperator();

        protected override Type ExpectedExpressionType => typeof(BitwiseNotExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.BitwiseAnd;

        protected override string[] ExpectedTags => new[] { "~" };

        #endregion
    }
}
