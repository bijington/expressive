namespace Expressive
{
    internal sealed class Token
    {
        internal string CurrentToken { get; }

        internal int Length { get; }

        internal int StartIndex { get; }

        public Token(string currentToken, int startIndex)
        {
            this.CurrentToken = currentToken;
            this.StartIndex = startIndex;
            this.Length = this.CurrentToken?.Length ?? 0;
        }
    }
}
