using Expressive.Expressions;
using System.Collections.Generic;

namespace Expressive.Functions
{
    /// <summary>
    /// Interface definition for a Function that can be evaluated.
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// Gets or sets the Variables and their values to be used in evaluating an <see cref="Expression"/>.
        /// </summary>
        IDictionary<string, object> Variables { get; set; }

        /// <summary>
        /// Gets the name of the Function.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Forces the Function to evaluate itself using the supplied parameters.
        /// </summary>
        /// <param name="parameters">The list of parameters inside the Function.</param>
        /// <param name="options">The evaluation options to be used.</param>
        /// <returns>The result of the Function.</returns>
        object Evaluate(IExpression[] parameters, ExpressiveOptions options);
    }
}
