using Expressive.Exceptions;
using Expressive.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Expressive.Functions
{
    internal abstract class FunctionBase : IFunction
    {
        #region IFunction Members

        public IDictionary<string, object> Arguments { get; set; }

        public abstract string Name { get; }

        public abstract object Evaluate(IExpression[] participants);

        #endregion

        /// <summary>
        /// Validates whether the expected number of Parameters are present.
        /// </summary>
        /// <param name="participants"></param>
        /// <param name="expectedCount">The expected number of Parameters, use -1 for an unknown number.</param>
        /// <param name="minimumCount">The minimum number of Parameters.</param>
        /// <returns>True if the correct number are present, false otherwise.</returns>
        protected bool ValidateParameterCount(IExpression[] participants, int expectedCount, int minimumCount)
        {
            if (expectedCount != -1 && (participants == null || !participants.Any() || participants.Length != expectedCount))
            {
                throw new ParameterCountMismatchException(this.Name + "() takes only " + expectedCount + " argument(s)");
            }

            if (minimumCount > 0 && (participants == null || !participants.Any() || participants.Length < minimumCount))
            {
                throw new ParameterCountMismatchException(this.Name + "() expects at least " + minimumCount + " argument(s)");
            }

            return true;
        }
    }
}
