using Expressive.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Functions
{
    internal class AverageFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Average"; } }

        public override object Evaluate(object[] values)
        {
            this.ValidateParameterCount(values, -1, 1);

            object result = 0;

            foreach (var value in values)
            {
                result = Numbers.Add(result, value);
            }

            return Convert.ToDouble(result) / ((double)values.Length);
        }

        #endregion
    }
}
