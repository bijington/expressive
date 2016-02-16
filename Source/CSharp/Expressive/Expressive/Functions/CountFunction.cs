using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Functions
{
    internal class CountFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Count"; } }

        public override object Evaluate(object[] values)
        {
            this.ValidateParameterCount(values, -1, 1);

            return values != null ? values.Length : 0;
        }

        #endregion
    }
}
