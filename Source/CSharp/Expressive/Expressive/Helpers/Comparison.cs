using System;
using System.Collections.Generic;

namespace Expressive.Helpers
{
    internal static class Comparison
    {
        private static readonly Type[] CommonTypes = new[] { typeof(long), typeof(int), typeof(double), typeof(bool), typeof(DateTime), typeof(string), typeof(decimal) };

        internal static int CompareUsingMostPreciseType(object a, object b, bool ignoreCase)
        {
            Type mostPreciseType = GetMostPreciseType(a.GetType(), b.GetType());

            if (mostPreciseType == typeof(string))
            {
                return string.Compare((string)Convert.ChangeType(a, mostPreciseType), (string)Convert.ChangeType(b, mostPreciseType), ignoreCase);
            }

            return Comparison.Compare(a, b, mostPreciseType, ignoreCase);
        }

        internal static Type GetMostPreciseType(Type a, Type b)
        {
            foreach (Type t in Comparison.CommonTypes)
            {
                if (a == t || b == t)
                {
                    return t;
                }
            }

            return a;
        }

        private static int Compare(object lhs, object rhs, Type mostPreciseType, bool ignoreCase)
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
                // Attempt to convert using the mostPreciseType first.
                try
                {
                    if (lhsType == mostPreciseType)
                    {
                        rhs = Convert.ChangeType(rhs, mostPreciseType);
                    }
                    else
                    {
                        lhs = Convert.ChangeType(lhs, mostPreciseType);
                    }

                    return Comparer<object>.Default.Compare(lhs, rhs);
                }
                catch (System.Exception)
                {

                }

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
                    return string.Compare((string)Convert.ChangeType(lhs, typeof(string)),
                                          (string)Convert.ChangeType(rhs, typeof(string)),
                                          ignoreCase);
                }
                catch (System.Exception)
                {

                }
            }

            return 0;
        }
    }
}
