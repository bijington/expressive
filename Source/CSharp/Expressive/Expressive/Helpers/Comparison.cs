using System;
using System.Collections;

namespace Expressive.Helpers
{
    //  Shout out to https://ncalc.codeplex.com/ for the bulk of this implementation.
    internal static class Comparison
    {
        private static Type[] CommonTypes = new[] { typeof(long), typeof(double), typeof(bool), typeof(DateTime), typeof(string), typeof(decimal) };

        internal static int CompareUsingMostPreciseType(object a, object b, bool ignoreCase)
        {
            Type mpt = GetMostPreciseType(a.GetType(), b.GetType());

            if (mpt == typeof(string))
            {
                return string.Compare((string)Convert.ChangeType(a, mpt), (string)Convert.ChangeType(b, mpt), ignoreCase);
            }

            return Comparer.Default.Compare(Convert.ChangeType(a, mpt), Convert.ChangeType(b, mpt));
        }

        internal static Type GetMostPreciseType(Type a, Type b)
        {
            foreach (Type t in CommonTypes)
            {
                if (a == t || b == t)
                {
                    return t;
                }
            }

            return a;
        }
    }
}
