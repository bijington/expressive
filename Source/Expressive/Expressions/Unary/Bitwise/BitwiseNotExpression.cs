using System;
using System.Collections.Generic;

namespace Expressive.Expressions.Unary.Bitwise
{
    internal class BitwiseNotExpression : UnaryExpressionBase
    {
        #region Constructors

        public BitwiseNotExpression(IExpression expression) : base(expression)
        {
        }

        #endregion

        #region UnaryExpressionBase Members

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var value = this.expression.Evaluate(variables);

            switch (value)
            {
                case null:
                    return null;

                case byte byteValue:
                    return unchecked((byte)~byteValue);

                case int intValue:
                    return ~intValue;

                case long longValue:
                    return ~longValue;

                case short shortValue:
                    return ~shortValue;

                case uint uintValue:
                    return ~uintValue;

                case ulong ulongValue:
                    return ~ulongValue;

                case ushort ushortValue:
                    return ~ushortValue;

                default:
                    return ~Convert.ToInt64(value);
            }
        }

        #endregion
    }
}
