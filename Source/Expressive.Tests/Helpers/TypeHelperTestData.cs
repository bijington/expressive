using System;

namespace Expressive.Tests.Helpers
{
    public class TypeHelperTestData
    {
        public TypeCode ExpectedTypeCode { get; }
        public object Value { get; }

        public TypeHelperTestData(object value, TypeCode expectedTypeCode)
        {
            this.Value = value;
            this.ExpectedTypeCode = expectedTypeCode;
        }
    }
}
