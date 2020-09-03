using System;
using Expressive.Functions;
#if !NETSTANDARD1_4
using System.Runtime.Serialization;
using System.Security.Permissions;
#endif

namespace Expressive.Exceptions
{
    /// <summary>
    /// Represents an error that is thrown when registering an <see cref="IFunction"/> and the name is already used.
    /// </summary>
#if !NETSTANDARD1_4
    [Serializable]
#endif
    public sealed class FunctionNameAlreadyRegisteredException : Exception
    {
        /// <summary>
        /// Gets the name of the function already used.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionNameAlreadyRegisteredException"/> class with a specified unrecognised token.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        internal FunctionNameAlreadyRegisteredException(string name)
            : base($"A function has already been registered '{name}'")
        {
            this.Name = name;
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

            info.AddValue("Name", Name);
        }
#endif
    }
}
