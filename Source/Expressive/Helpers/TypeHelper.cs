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
            return Type.GetTypeCode(value?.GetType());
        }
    }
}
