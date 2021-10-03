using Expressive.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Expressive.Tests.Helpers
{
    public static class TypeHelperTests
    {
        [TestCaseSource(nameof(GetTypeCodeSource))]
        public static void GetTypeCodeShouldSupplyAppropriateResults(TypeHelperTestData sourceData)
        {
            if (sourceData is null)
            {
                throw new ArgumentNullException(nameof(sourceData));
            }

            Assert.That(TypeHelper.GetTypeCode(sourceData.Value), Is.EqualTo(sourceData.ExpectedTypeCode));
        }

        private static IEnumerable<TypeHelperTestData> GetTypeCodeSource
        {
            get
            {
                yield return new TypeHelperTestData(null, TypeCode.Empty);

                yield return new TypeHelperTestData((long)123, TypeCode.Int64);
                yield return new TypeHelperTestData((int)123, TypeCode.Int32);
                yield return new TypeHelperTestData((short)123, TypeCode.Int16);
                yield return new TypeHelperTestData((ulong)123, TypeCode.UInt64);
                yield return new TypeHelperTestData((uint)123, TypeCode.UInt32);
                yield return new TypeHelperTestData((ushort)123, TypeCode.UInt16);
                yield return new TypeHelperTestData((decimal)123, TypeCode.Decimal);
                yield return new TypeHelperTestData((double)123, TypeCode.Double);
                yield return new TypeHelperTestData((float)123, TypeCode.Single);

                yield return new TypeHelperTestData((byte)8, TypeCode.Byte);
                yield return new TypeHelperTestData((sbyte)8, TypeCode.SByte);

                yield return new TypeHelperTestData('a', TypeCode.Char);
                yield return new TypeHelperTestData(true, TypeCode.Boolean);
                yield return new TypeHelperTestData(DateTime.UtcNow, TypeCode.DateTime);
                yield return new TypeHelperTestData("123", TypeCode.String);
            }
        }
    }
}
