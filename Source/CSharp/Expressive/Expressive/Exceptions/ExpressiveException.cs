using System;

namespace Expressive.Exceptions
{
    /// <summary>
    /// The main exposed <see cref="Exception"/> for users of an Expression. Check the InnerException for more information.
    /// </summary>
    [Serializable]
    public sealed class ExpressiveException : Exception
    {
        internal ExpressiveException(Exception innerException) : base(innerException.Message, innerException)
        {

        }
    }
}
