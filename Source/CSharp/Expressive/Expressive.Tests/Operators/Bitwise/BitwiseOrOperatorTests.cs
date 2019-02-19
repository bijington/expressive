using System;
using Expressive.Expressions.Binary.Bitwise;
using Expressive.Operators;
using Expressive.Operators.Bitwise;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Operators.Bitwise
{
    [TestClass]
    public class BitwiseOrOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new BitwiseOrOperator();

        protected override Type ExpectedExpressionType => typeof(BitwiseOrExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.BitwiseOr;

        protected override string[] ExpectedTags => new[] { "|" };

        #endregion
    }
}
