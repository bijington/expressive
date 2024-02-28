using System;
using Expressive.Functions;

namespace Expressive.Exceptions
{
    /// <summary>
    /// Represents an error that is thrown when registering an <see cref="IFunction"/> and the name is already used.
    /// </summary>
    public sealed class FunctionNameAlreadyRegisteredException : Exception
    {
        /// <summary>
        /// Gets the name of the function already used.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionNameAlreadyRegisteredException"/> class.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        internal FunctionNameAlreadyRegisteredException(string name)
            : base($"A function has already been registered '{name}'")
        {
            this.Name = name;
        }
    }
}
