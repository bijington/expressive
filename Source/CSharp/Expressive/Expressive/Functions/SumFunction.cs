using Expressive.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Functions
{
    internal class SumFunction : FunctionBase
    {
        #region IFunction Members

        public override string Name { get { return "Sum"; } }

        public override object Evaluate(object[] values)
        {
            this.ValidateParameterCount(values, -1, 1);

            object result = 0;

            foreach (var value in values)
            {
                result = Numbers.Add(result, value);
            }

            return result;
        }

        #endregion
    }
}
