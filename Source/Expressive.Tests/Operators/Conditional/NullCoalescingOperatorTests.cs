﻿using System;
using Expressive.Expressions.Binary.Conditional;
using Expressive.Operators;
using Expressive.Operators.Conditional;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Conditional
{
    [TestFixture]
    public class NullCoalescingOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new NullCoalescingOperator();

        protected override Type ExpectedExpressionType => typeof(NullCoalescingExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.NullCoalescing;

        protected override string[] ExpectedTags => new[] { "??" };

        #endregion
    }
}
