using System;

namespace Expressive.Exceptions
{
    public class MissingTokenException : Exception
    {
        public char MissingToken { get; private set; }

        public MissingTokenException(string message, char missingToken)
            : base(message)
        {
            this.MissingToken = missingToken;
        }
    }
}
