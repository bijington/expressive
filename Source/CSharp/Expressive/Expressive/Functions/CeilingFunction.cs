using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Functions
{
    internal class CeilingFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Ceiling"; } }

        public override object Evaluate(object[] values)
        {
            this.ValidateParameterCount(values, 1, 1);

            if (values[0] is double)
            {
                return Math.Ceiling((double)values[0]);
            }
            else if (values[0] is decimal)
            {
                return Math.Ceiling((decimal)values[0]);
            }
            return Math.Ceiling(Convert.ToDouble(values[0]));
        }

        #endregion
    }
}
