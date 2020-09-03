using System;
using Expressive.Expressions.Binary.Bitwise;
using Expressive.Operators;
using Expressive.Operators.Bitwise;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Operators.Bitwise
{
    [TestClass]
    public class LeftShiftOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new LeftShiftOperator();

        protected override Type ExpectedExpressionType => typeof(LeftShiftExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.LeftShift;

        protected override string[] ExpectedTags => new[] { "<<" };

        #endregion
    }
}
