using System;

namespace Expressive.Exceptions
{
    /// <summary>
    /// Represents an error that is thrown when a token is not recognised inside an <see cref="Expression"/>.
    /// </summary>
    public sealed class UnrecognisedTokenException : Exception
    {
        /// <summary>
        /// Gets the unrecognised token in the <see cref="Expression"/>.
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnrecognisedTokenException"/> class with a specified unrecognised token.
        /// </summary>
        /// <param name="token">The unrecognised token.</param>
        internal UnrecognisedTokenException(string token)
            : base("Unrecognised token '" + token + "'")
        {
            this.Token = token;
        }
    }
}
