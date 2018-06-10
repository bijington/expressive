using System;
#if !NETSTANDARD1_4
using System.Runtime.Serialization;
using System.Security.Permissions;
#endif

namespace Expressive.Exceptions
{
    /// <summary>
    /// Represents an error that is thrown when a missing token is detected inside an <see cref="Expression"/>.
    /// </summary>
#if !NETSTANDARD1_4
    [Serializable]
#endif
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

#if !NETSTANDARD1_4
        /// <summary>
        /// Set the <see cref="SerializationInfo"/> with information about this exception.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("MissingToken", MissingToken);
        }
#endif
    }
}
