using System;
using System.Collections.Generic;

namespace Expressive
{
    internal static class ExtensionMethods
    {
        internal static bool IsArithmeticOperator(this string source)
        {
            return string.Equals(source, "+", StringComparison.Ordinal) ||
                string.Equals(source, "-", StringComparison.Ordinal) || string.Equals(source, "\u2212", StringComparison.Ordinal) ||
                string.Equals(source, "/", StringComparison.Ordinal) || string.Equals(source, "\u00f7", StringComparison.Ordinal) ||
                string.Equals(source, "*", StringComparison.Ordinal) || string.Equals(source, "\u00d7", StringComparison.Ordinal) ||
                string.Equals(source, "+", StringComparison.Ordinal) ||
                string.Equals(source, "+", StringComparison.Ordinal) ||
                string.Equals(source, "+", StringComparison.Ordinal);
        }

        internal static bool IsNumeric(this string source)
        {
            double test;
            return double.TryParse(source, out test);
        }

        internal static T PeekOrDefault<T>(this Queue<T> queue)
        {
            return (queue.Count > 0) ? queue.Peek() : default(T);
        }

        internal static string SubstringUpTo(this string source, int startIndex, char character)
        {
            if (startIndex != 0)
            {
                string inner = source.Substring(startIndex);

                return inner.Substring(0, inner.IndexOf(character) + 1);
            }

            return source.Substring(startIndex, source.IndexOf(character) + 1);
        }
    }
}
