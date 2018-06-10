using System;
using System.Collections.Generic;

namespace Expressive.Helpers
{
    //  Shout out to https://ncalc.codeplex.com/ for the bulk of this implementation.
    internal static class Comparison
    {
        private static readonly Type[] CommonTypes = new[] { typeof(long), typeof(double), typeof(bool), typeof(DateTime), typeof(string), typeof(decimal) };

        internal static int CompareUsingMostPreciseType(object a, object b, bool ignoreCase)
        {
            Type mpt = GetMostPreciseType(a.GetType(), b.GetType());

            if (mpt == typeof(string))
            {
                return string.Compare((string)Convert.ChangeType(a, mpt), (string)Convert.ChangeType(b, mpt), ignoreCase);
            }

            return Comparison.Compare(a, b);
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

        private static int Compare(object lhs, object rhs)
        {
            // If at least one is null then the check is simple.
            if (lhs == null && rhs == null)
            {
                return 0;
            }
            else if (lhs == null)
            {
                return -1;
            }
            else if (rhs == null)
            {
                return 1;
            }

            var lhsType = lhs.GetType();
            var rhsType = rhs.GetType();

            if (lhsType == rhsType)
            {
                return Comparer<object>.Default.Compare(lhs, rhs);
            }
            else
            {
                // Attempt to convert the RHS to match the LHS.
                try
                {
                    return Comparer<object>.Default.Compare(lhs, Convert.ChangeType(rhs, lhsType));
                }
                catch (System.Exception)
                {

                }

                // Attempt to convert the LHS to match the RHS.
                try
                {
                    return Comparer<object>.Default.Compare(lhs, Convert.ChangeType(rhs, lhsType));
                }
                catch (System.Exception)
                {

                }

                // Failing that resort to a string.
                try
                {
                    return Comparer<string>.Default.Compare((string)Convert.ChangeType(lhs, typeof(string)),
                                                            (string)Convert.ChangeType(rhs, typeof(string)));
                }
                catch (System.Exception)
                {

                }
            }

            return 0;
        }
    }
}
