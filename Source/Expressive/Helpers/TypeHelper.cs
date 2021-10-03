using System;

namespace Expressive.Helpers
{
    /// <summary>
    /// Helper class to determine <see cref="Type"/> information.
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Gets the underlying type code of the specified <paramref name="value"/>s <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The value to find the underlying <see cref="TypeCode"/> for.</param>
        /// <returns>The code of the underlying type, or <see cref="TypeCode.Empty"/> if type is null.</returns>
        public static TypeCode GetTypeCode(object value)
        {
            var typeCode = TypeCode.Object;

#if NETSTANDARD1_4
            // TODO: Explore converting all numbers to decimal and simplifying all of this.

            switch (value)
            {
                case bool _:
                    typeCode = TypeCode.Boolean;
                    break;
                case byte _:
                    typeCode = TypeCode.Byte;
                    break;
                case char _:
                    typeCode = TypeCode.Char;
                    break;
                case DateTime _:
                    typeCode = TypeCode.DateTime;
                    break;
                case decimal _:
                    typeCode = TypeCode.Decimal;
                    break;
                case double _:
                    typeCode = TypeCode.Double;
                    break;
                case long _:
                    typeCode = TypeCode.Int64;
                    break;
                case int _:
                    typeCode = TypeCode.Int32;
                    break;
                case short _:
                    typeCode = TypeCode.Int16;
                    break;
                case sbyte _:
                    typeCode = TypeCode.SByte;
                    break;
                case float _:
                    typeCode = TypeCode.Single;
                    break;
                case string _:
                    typeCode = TypeCode.String;
                    break;
                case ushort _:
                    typeCode = TypeCode.UInt16;
                    break;
                case uint _:
                    typeCode = TypeCode.UInt32;
                    break;
                case ulong _:
                    typeCode = TypeCode.UInt64;
                    break;
                case null:
                    typeCode = TypeCode.Empty;
                    break;
            }

#else
            typeCode = Type.GetTypeCode(value?.GetType());
#endif

            return typeCode;
        }
    }
}
