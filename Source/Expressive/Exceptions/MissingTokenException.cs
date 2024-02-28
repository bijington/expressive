using System;

namespace Expressive.Exceptions
{
    /// <summary>
    /// Represents an error that is thrown when a missing token is detected inside an <see cref="Expression"/>.
    /// </summary>
    public sealed class MissingTokenException : Exception
    {
        /// <summary>
        /// Gets the token that is missing from the <see cref="Expression"/>.
        /// </summary>
        public char MissingToken { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingTokenException"/> class with a specified error message and missing token.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="missingToken">The token that is missing.</param>
        internal MissingTokenException(string message, char missingToken)
            : base(message)
        {
            this.MissingToken = missingToken;
        }
    }
}
