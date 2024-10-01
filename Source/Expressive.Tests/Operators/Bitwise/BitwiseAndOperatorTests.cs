using System;
using Expressive.Expressions.Binary.Bitwise;
using Expressive.Operators;
using Expressive.Operators.Bitwise;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Bitwise
{
    [TestFixture]
    public class BitwiseAndOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new BitwiseAndOperator();

        protected override Type ExpectedExpressionType => typeof(BitwiseAndExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.BitwiseAnd;

        protected override string[] ExpectedTags => new[] { "&" };

        #endregion
    }
}
