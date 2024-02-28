using System;
using Expressive.Operators;

namespace Expressive.Exceptions
{
    /// <summary>
    /// Represents an error that is thrown when registering an <see cref="IOperator"/> and the name is already used.
    /// </summary>
    public sealed class OperatorNameAlreadyRegisteredException : Exception
    {
        /// <summary>
        /// Gets the tag of the operator already used.
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNameAlreadyRegisteredException"/> class.
        /// </summary>
        /// <param name="tag">The tag of the operator.</param>
        internal OperatorNameAlreadyRegisteredException(string tag)
            : base($"An operator has already been registered '{tag}'")
        {
            this.Tag = tag;
        }
    }
}
