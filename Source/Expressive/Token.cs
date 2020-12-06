namespace Expressive
{
    /// <summary>
    /// Represents a chunk of expression that has been identified as something compilable.
    /// </summary>
    public sealed class Token
    {
        /// <summary>
        /// Gets the text from the expression.
        /// </summary>
        public string CurrentToken { get; }

        /// <summary>
        /// Gets the length of the text.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the index where it was discovered in the text.
        /// </summary>
        public int StartIndex { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="currentToken">The text from the expression.</param>
        /// <param name="startIndex">The index where it was discovered in the text.</param>
        public Token(string currentToken, int startIndex)
        {
            this.CurrentToken = currentToken;
            this.StartIndex = startIndex;
            this.Length = this.CurrentToken?.Length ?? 0;
        }
    }
}
