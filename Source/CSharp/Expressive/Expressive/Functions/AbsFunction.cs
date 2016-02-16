using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Functions
{
    internal class AbsFunction : FunctionBase
    {
        #region IFunction Members

        public override string Name { get { return "Abs"; } }

        public override object Evaluate(object[] values)
        {
            this.ValidateParameterCount(values, 1, 1);

            var valueType = Type.GetTypeCode(values[0].GetType());

            switch (valueType)
            {
                case TypeCode.Decimal:
                    return Math.Abs(Convert.ToDecimal(values[0]));
                case TypeCode.Double:
                    return Math.Abs(Convert.ToDouble(values[0]));
                case TypeCode.Int16:
                    return Math.Abs(Convert.ToInt16(values[0]));
                case TypeCode.UInt16:
                    return Math.Abs(Convert.ToUInt16(values[0]));
                case TypeCode.Int32:
                    return Math.Abs(Convert.ToInt32(values[0]));
                case TypeCode.UInt32:
                    return Math.Abs(Convert.ToUInt32(values[0]));
                case TypeCode.Int64:
                    return Math.Abs(Convert.ToInt64(values[0]));
                case TypeCode.SByte:
                    return Math.Abs(Convert.ToSByte(values[0]));
                case TypeCode.Single:
                    return Math.Abs(Convert.ToSingle(values[0]));
                default:
                    break;
            }

            return null;
        }

        #endregion
    }
}
