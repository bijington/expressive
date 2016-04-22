using System;

namespace Expressive.Exceptions
{
    public class UnrecognisedTokenException : Exception
    {
        public string Token { get; private set; }

        public UnrecognisedTokenException(string token)
            : base("Unrecognised token '" + token + "'")
        {
            this.Token = token;
        }
    }
}
