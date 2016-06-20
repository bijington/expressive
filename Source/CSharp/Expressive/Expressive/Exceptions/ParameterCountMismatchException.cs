using System;

namespace Expressive.Exceptions
{
    [Serializable]
    public class ParameterCountMismatchException : Exception
    {
        public ParameterCountMismatchException(string message)
            : base(message)
        {

        }
    }
}
