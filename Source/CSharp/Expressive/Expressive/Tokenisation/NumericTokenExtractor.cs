namespace Expressive.Tokenisation
{
    internal class NumericTokenExtractor : ITokenExtractor
    {
        public Token ExtractToken(string expression, int currentIndex, Context context)
        {
            var character = expression[currentIndex];

            if (!char.IsDigit(character))
            {
                return null;
            }

            var number = GetNumber(expression, currentIndex, context);

            return new Token(number, currentIndex);
        }

        private static string GetNumber(string expression, int startIndex, Context context)
        {
            var hasDecimalPoint = false;
            var index = startIndex;
            var character = expression[index];

            while (index < expression.Length &&
                   (char.IsDigit(character) || !hasDecimalPoint && character == context.DecimalSeparator))
            {
                if (!hasDecimalPoint && character == context.DecimalSeparator)
                {
                    hasDecimalPoint = true;
                }

                index++;

                if (index == expression.Length)
                {
                    break;
                }

                character = expression[index];
            }

            return expression.Substring(startIndex, index - startIndex);
        }
    }
}