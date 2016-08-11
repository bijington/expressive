namespace Expressive
{
    internal sealed class Token
    {
        internal string CurrentToken { get; private set; }

        internal int Length { get; private set; }

        internal int StartIndex { get; private set; }

        public Token(string currentToken, int startIndex)
        {
            this.CurrentToken = currentToken;
            this.StartIndex = startIndex;
            this.Length = this.CurrentToken?.Length ?? 0;
        }
    }
}
