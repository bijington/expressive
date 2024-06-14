using System;
using Expressive.Expressions.Binary.Multiplicative;
using Expressive.Operators;
using Expressive.Operators.Multiplicative;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Multiplicative
{
    [TestFixture]
    public class DivideOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new DivideOperator();

        protected override Type ExpectedExpressionType => typeof(DivideExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.Divide;

        protected override string[] ExpectedTags => new[] { "/", "\u00f7" };

        #endregion
    }
}
