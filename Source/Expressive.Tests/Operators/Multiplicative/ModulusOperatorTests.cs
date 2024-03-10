using System;
using Expressive.Expressions.Binary.Multiplicative;
using Expressive.Operators;
using Expressive.Operators.Multiplicative;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Multiplicative
{
    [TestFixture]
    public class ModulusOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new ModulusOperator();

        protected override Type ExpectedExpressionType => typeof(ModulusExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.Modulus;

        protected override string[] ExpectedTags => new[] { "%", "mod" };

        #endregion
    }
}
