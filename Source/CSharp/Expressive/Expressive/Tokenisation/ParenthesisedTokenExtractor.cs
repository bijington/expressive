using System.Collections.Generic;
using Expressive.Exceptions;

namespace Expressive.Tokenisation
{
    internal class ParenthesisedTokenExtractor : ITokenExtractor
    {
        private readonly char endingCharacter;
        private readonly char startingCharacter;

        public ParenthesisedTokenExtractor(char singleCharacter) : this(singleCharacter, singleCharacter)
        {
        }

        public ParenthesisedTokenExtractor(char startingCharacter, char endingCharacter)
        {
            this.startingCharacter = startingCharacter;
            this.endingCharacter = endingCharacter;
        }

        public Token ExtractToken(string expression, int currentIndex, Context context)
        {
            var character = expression[currentIndex];

            if (character != this.startingCharacter)
            {
                return null;
            }

            var extracted = GetString(expression, currentIndex, this.endingCharacter);

            if (string.IsNullOrWhiteSpace(extracted))
            {
                throw new MissingTokenException($"Missing closing token '{this.endingCharacter}'", this.endingCharacter);
            }

            return new Token(extracted, currentIndex);
        }

        private static string GetString(string expression, int startIndex, char quoteCharacter)
        {
            var index = startIndex;
            var foundEndingQuote = false;
            var character = expression[index];
            var isEscape = false;

            var escapableCharacters = new List<char> { '\\', '"', '\'', 't', 'n', 'r' };

            while (index < expression.Length && !foundEndingQuote)
            {
                if (index != startIndex &&
                    character == quoteCharacter &&
                    !isEscape) // Make sure the escape character wasn't previously used.
                {
                    foundEndingQuote = true;
                }

                if (isEscape && escapableCharacters.Contains(character))
                {
                    isEscape = false;
                }
                else if (character == '\\' && !isEscape)
                {
                    isEscape = true;
                }

                index++;

                if (index == expression.Length)
                {
                    break;
                }

                character = expression[index];
            }

            if (foundEndingQuote)
            {
                return expression.Substring(startIndex, index - startIndex);
            }

            return null;
        }
    }
}