using System;
using System.Collections;

namespace Expressive.Helpers
{
    //  Shout out to https://ncalc.codeplex.com/ for the bulk of this implementation.
    internal static class Comparison
    {
        private static Type[] CommonTypes = new[] { typeof(Int64), typeof(Double), typeof(Boolean), typeof(DateTime), typeof(String), typeof(Decimal) };

        internal static int CompareUsingMostPreciseType(object a, object b)
        {
            Type mpt = GetMostPreciseType(a.GetType(), b.GetType());
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
