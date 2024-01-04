using System.Collections.Generic;

namespace Expressive.Operators
{
    /// <summary>
    /// Interface definition for an <see cref="IOperator"/>s metadata.
    /// </summary>
    public interface IOperatorMetadata
    {
        /// <summary>
        /// Gets the list of tags that can be used to identify this IOperator.
        /// </summary>
        IEnumerable<string> Tags { get; }
    }
}