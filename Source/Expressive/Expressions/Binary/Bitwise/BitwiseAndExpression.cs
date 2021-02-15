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

                case long leftLong:
                    return EvaluateLong(leftLong, rhs);

                case short leftShort:
                    return EvaluateShort(leftShort, rhs);

                case uint leftUint:
                    return EvaluateUint(leftUint, rhs);

                case ulong leftUlong:
                    return EvaluateUlong(leftUlong, rhs);

                case ushort leftUshort:
                    return EvaluateUshort(leftUshort, rhs);

                default:
                    return Convert.ToInt64(lhs) & Convert.ToInt64(rhs);
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

                case ushort ushortRight:
                    return leftInt & ushortRight;

                default:
                    return leftInt & Convert.ToInt32(rhs);
            }
        }

        private static object EvaluateLong(long leftLong, object rhs)
        {
            switch (rhs)
            {
                case null:
                    return null;

                case byte byteRight:
                    return leftLong & byteRight;

                case int intRight:
                    return leftLong & intRight;

                case long longRight:
                    return leftLong & longRight;

                case short shortRight:
                    return leftLong & shortRight;

                case uint uintRight:
                    return leftLong & uintRight;

                case ushort ushortRight:
                    return leftLong & ushortRight;

                default:
                    return leftLong & Convert.ToInt64(rhs);
            }
        }

        private static object EvaluateShort(short leftShort, object rhs)
        {
            switch (rhs)
            {
                case null:
                    return null;

                case byte byteRight:
                    return leftShort & byteRight;

                case int intRight:
                    return leftShort & intRight;

                case long longRight:
                    return leftShort & longRight;

                case short shortRight:
                    return leftShort & shortRight;

                case uint uintRight:
                    return leftShort & uintRight;

                case ushort ushortRight:
                    return leftShort & ushortRight;

                default:
                    return leftShort & Convert.ToInt16(rhs);
            }
        }

        private static object EvaluateUint(uint leftUint, object rhs)
        {
            switch (rhs)
            {
                case null:
                    return null;

                case byte byteRight:
                    return leftUint & byteRight;

                case int intRight:
                    return leftUint & intRight;

                case long longRight:
                    return leftUint & longRight;

                case short shortRight:
                    return leftUint & shortRight;

                case uint uintRight:
                    return leftUint & uintRight;

                case ushort ushortRight:
                    return leftUint & ushortRight;

                default:
                    return leftUint & Convert.ToUInt32(rhs);
            }
        }

        private static object EvaluateUlong(ulong leftUlong, object rhs)
        {
            switch (rhs)
            {
                case null:
                    return null;

                case byte byteRight:
                    return leftUlong & byteRight;

                case uint uintRight:
                    return leftUlong & uintRight;

                case ulong ulongRight:
                    return leftUlong & ulongRight;

                case ushort ushortRight:
                    return leftUlong & ushortRight;

                default:
                    return leftUlong & Convert.ToUInt64(rhs);
            }
        }

        private static object EvaluateUshort(ushort leftUshort, object rhs)
        {
            switch (rhs)
            {
                case null:
                    return null;

                case byte byteRight:
                    return leftUshort & byteRight;

                case int intRight:
                    return leftUshort & intRight;

                case long longRight:
                    return leftUshort & longRight;

                case short shortRight:
                    return leftUshort & shortRight;

                case uint uintRight:
                    return leftUshort & uintRight;

                case ulong ulongRight:
                    return leftUshort & ulongRight;

                case ushort ushortRight:
                    return leftUshort & ushortRight;

                default:
                    return leftUshort & Convert.ToUInt16(rhs);
            }
        }
    }
}
