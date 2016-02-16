using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Functions
{
    internal class AcosFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Acos"; } }

        public override object Evaluate(object[] values)
        {
            this.ValidateParameterCount(values, 1, 1);

            return Math.Acos(Convert.ToDouble(values[0]));
        }

        #endregion
    }
}
