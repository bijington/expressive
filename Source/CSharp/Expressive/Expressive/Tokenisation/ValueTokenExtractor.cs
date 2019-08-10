using System;
using System.Linq;

namespace Expressive.Tokenisation
{
    internal class ValueTokenExtractor : ITokenExtractor
    {
        private readonly string value;

        public ValueTokenExtractor(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.value = value;
        }

        public Token ExtractToken(string expression, int currentIndex, Context context)
        {
            var character = expression[currentIndex];
            var expressionLength = expression.Length;

            var initialToken = this.value.First();

            if (string.Equals(character.ToString(), initialToken.ToString(), context.StringComparison) &&
                CanExtractValue(expression, expressionLength, currentIndex, this.value, context))
            {
                return new Token(this.value, currentIndex);
            }

            return null;
        }

        private static bool CanExtractValue(string expression, int expressionLength, int index, string expectedValue, Context context)
        {
            return string.Equals(expectedValue, ExtractValue(expression, expressionLength, index, expectedValue, context), context.StringComparison);
        }

        private static string ExtractValue(string expression, int expressionLength, int index, string expectedValue, Context context)
        {
            string result = null;
            var valueLength = expectedValue.Length;

            if (expressionLength >= index + valueLength)
            {
                var valueString = expression.Substring(index, valueLength);
                bool isValidValue = true;
                if (expressionLength > index + valueLength)
                {
                    // If the next character is not a continuation of the word then we are valid.
                    isValidValue = !char.IsLetterOrDigit(expression[index + valueLength]);
                }

                if (string.Equals(valueString, expectedValue, context.StringComparison) &&
                    isValidValue)
                {
                    result = valueString;
                }
            }

            return result;
        }
    }
}
