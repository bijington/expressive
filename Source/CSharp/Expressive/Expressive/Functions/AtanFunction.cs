using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Functions
{
    internal class AtanFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Atan"; } }

        public override object Evaluate(object[] values)
        {
            this.ValidateParameterCount(values, 1, 1);

            return Math.Atan(Convert.ToDouble(values[0]));
        }

        #endregion
    }
}
