using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Functions
{
    internal abstract class FunctionBase : IFunction
    {
        #region IFunction Members

        public abstract string Name { get; }

        public abstract object Evaluate(object[] values);

        #endregion

        /// <summary>
        /// Validates whether the expected number of Parameters are present.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="expectedCount">The expected number of Parameters, use -1 for an unknown number.</param>
        /// <returns>True if the correct number are present, false otherwise.</returns>
        protected bool ValidateParameterCount(object[] values, int expectedCount, int minimumCount)
        {
            if (expectedCount != -1 && (values == null || !values.Any() || values.Length != expectedCount))
            {
                throw new ArgumentException(this.Name + "() takes only " + expectedCount + " argument(s)");
            }

            if (minimumCount > 0 && (values == null || !values.Any() || values.Length < minimumCount))
            {
                throw new ArgumentException(this.Name + "() expects at least " + minimumCount + " argument(s)");
            }

            return true;
        }
    }
}
