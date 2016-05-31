using System;

namespace Expressive.Exceptions
{
    public class ParameterCountMismatchException : Exception
    {
        public ParameterCountMismatchException(string message)
            : base(message)
        {

        }
    }
}
