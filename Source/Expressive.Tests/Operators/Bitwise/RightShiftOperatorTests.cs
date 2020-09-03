using System;
using Expressive.Expressions.Binary.Bitwise;
using Expressive.Operators;
using Expressive.Operators.Bitwise;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Expressive.Tests.Operators.Bitwise
{
    [TestClass]
    public class RightShiftOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new RightShiftOperator();

        protected override Type ExpectedExpressionType => typeof(RightShiftExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.RightShift;

        protected override string[] ExpectedTags => new[] { ">>" };

        #endregion
    }
}
