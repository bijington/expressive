using System;
using System.Collections.Generic;

namespace Expressive.Tokenisation
{
    internal class KeywordTokenExtractor : ITokenExtractor
    {
        private readonly IEnumerable<string> keywords;

        public KeywordTokenExtractor(IEnumerable<string> keywords)
        {
            if (keywords is null)
            {
                throw new ArgumentNullException(nameof(keywords));
            }

            this.keywords = keywords;
        }

        public Token ExtractToken(string expression, int currentIndex, Context context)
        {
            var expressionLength = expression.Length;

            foreach (var possibleName in this.keywords)
            {
                var lookAhead = expression.Substring(currentIndex, Math.Min(possibleName.Length, expressionLength - currentIndex));

                if (!string.Equals(lookAhead, possibleName, context.ParsingStringComparison))
                {
                    continue;
                }

                return new Token(lookAhead, currentIndex);
            }

            return null;
        }
    }
}