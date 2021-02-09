using System;
using System.Collections.Generic;

namespace Expressive.Expressions.Binary.Bitwise
{
    internal class BitwiseAndExpression : BinaryExpressionBase
    {
        #region Constructors

        public BitwiseAndExpression(IExpression lhs, IExpression rhs, Context context) : base(lhs, rhs, context)
        {
        }

        #endregion

        #region BinaryExpressionBase Members

        /// <inheritdoc />
        protected override object EvaluateImpl(object lhsResult, IExpression rightHandSide, IDictionary<string, object> variables) =>
            EvaluateAggregates(lhsResult, rightHandSide, variables, (l, r) => Evaluate(l, r));

        #endregion

        private static object Evaluate(object lhs, object rhs)
        {
            switch (lhs)
            {
                case null:
                    return null;

                case byte leftByte:
                    return EvaluateByte(leftByte, rhs);

                case int leftInt:
                    return EvaluateInt(leftInt, rhs);

                case long longLeft:
                    return longLeft & (long)rhs;

                case short shortLeft:
                    return ~shortLeft;

                case uint leftUint:
                    return ~leftUint;

                case ulong ulongLeft:
                    return ~ulongLeft;

                case ushort ushortLeft:
                    return ~ushortLeft;

                default:
                    return ~Convert.ToInt64(lhs);
            }
        }

        private static object EvaluateByte(byte leftByte, object rhs)
        {
            switch (rhs)
            {
                case null:
                    return null;

                case byte byteRight:
                    return leftByte & byteRight;

                case int intRight:
                    return leftByte & intRight;

                case long longRight:
                    return leftByte & longRight;

                case short shortRight:
                    return leftByte & shortRight;

                case uint uintRight:
                    return leftByte & uintRight;

                case ulong ulongRight:
                    return leftByte & ulongRight;

                case ushort ushortRight:
                    return leftByte & ushortRight;

                default:
                    return leftByte & Convert.ToByte(rhs);
            }
        }

        private static object EvaluateInt(int leftInt, object rhs)
        {
            switch (rhs)
            {
                case null:
                    return null;

                case byte byteRight:
                    return leftInt & byteRight;

                case int intRight:
                    return leftInt & intRight;

                case long longRight:
                    return leftInt & longRight;

                case short shortRight:
                    return leftInt & shortRight;

                case uint uintRight:
                    return leftInt & uintRight;

                case ulong ulongRight:
                    return leftInt & ulongRight;

                case ushort ushortRight:
                    return leftInt & ushortRight;

                default:
                    return leftInt & Convert.ToByte(rhs);
            }
        }
    }
}
