﻿using System;
using Expressive.Expressions.Binary.Multiplicative;
using Expressive.Operators;
using Expressive.Operators.Multiplicative;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Multiplicative
{
    [TestFixture]
    public class MultiplyOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new MultiplyOperator();

        protected override Type ExpectedExpressionType => typeof(MultiplyExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.Multiply;

        protected override string[] ExpectedTags => new[] { "*", "\u00d7" };

        #endregion
    }
}
