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

            if (character == initialToken &&
                CanExtractValue(expression, expressionLength, currentIndex, this.value))
            {
                return new Token(this.value, currentIndex);
            }

            return null;
        }

        private static bool CanExtractValue(string expression, int expressionLength, int index, string value)
        {
            return string.Equals(value, ExtractValue(expression, expressionLength, index, value), StringComparison.OrdinalIgnoreCase);
        }

        private static string ExtractValue(string expression, int expressionLength, int index, string expectedValue)
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

                if (string.Equals(valueString, expectedValue, StringComparison.OrdinalIgnoreCase) &&
                    isValidValue)
                {
                    result = valueString;
                }
            }

            return result;
        }
    }
}
