using Expressive.Exceptions;
using Expressive.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Expressive.Functions
{
    internal abstract class FunctionBase : IFunction
    {
        #region IFunction Members

        public IDictionary<string, object> Variables { get; set; }

        public abstract string Name { get; }

        public abstract object Evaluate(IExpression[] parameters, ExpressiveOptions options);

        #endregion

        /// <summary>
        /// Validates whether the expected number of parameters are present.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="expectedCount">The expected number of parameters, use -1 for an unknown number.</param>
        /// <param name="minimumCount">The minimum number of parameters.</param>
        /// <returns>True if the correct number are present, false otherwise.</returns>
        protected bool ValidateParameterCount(IExpression[] parameters, int expectedCount, int minimumCount)
        {
            if (expectedCount != -1 && (parameters == null || !parameters.Any() || parameters.Length != expectedCount))
            {
                throw new ParameterCountMismatchException(this.Name + "() takes only " + expectedCount + " argument(s)");
            }

            if (minimumCount > 0 && (parameters == null || !parameters.Any() || parameters.Length < minimumCount))
            {
                throw new ParameterCountMismatchException(this.Name + "() expects at least " + minimumCount + " argument(s)");
            }

            return true;
        }
    }
}
