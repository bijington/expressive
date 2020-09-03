using System;
using System.Collections.Generic;
using Expressive.Helpers;

namespace Expressive.Expressions.Binary.Multiplicative
{
    internal class DivideExpression : BinaryExpressionBase
    {
        #region Constructors

        public DivideExpression(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables)
        {
            return this.Evaluate(lhsResult, rightHandSide, variables, (l, r) => 
                l is null || r is null || IsReal(l) || IsReal(r)
                    ? Numbers.Divide(l, r)
                    : Numbers.Divide(Convert.ToDouble(l), r));
        }

        #endregion

        private static bool IsReal(object value)
        {
            var typeCode = TypeHelper.GetTypeCode(value);

            return typeCode == TypeCode.Decimal || typeCode == TypeCode.Double || typeCode == TypeCode.Single;
        }
    }
}
