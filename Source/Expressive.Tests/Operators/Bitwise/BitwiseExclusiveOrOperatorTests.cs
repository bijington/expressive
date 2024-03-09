﻿using System;
using Expressive.Expressions.Binary.Bitwise;
using Expressive.Operators;
using Expressive.Operators.Bitwise;
using NUnit.Framework;

namespace Expressive.Tests.Operators.Bitwise
{
    [TestFixture]
    public class BitwiseExclusiveOrOperatorTests : OperatorBaseTests
    {
        #region OperatorBaseTests Members

        internal override IOperator Operator => new BitwiseExclusiveOrOperator();

        protected override Type ExpectedExpressionType => typeof(BitwiseExclusiveOrExpression);

        internal override OperatorPrecedence ExpectedOperatorPrecedence => OperatorPrecedence.BitwiseXOr;

        protected override string[] ExpectedTags => new[] { "^" };

        #endregion
    }
}
