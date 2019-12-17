using System.Collections.Generic;
using System.Linq;

namespace Expressive.Tokenisation
{
    internal sealed class Tokeniser
    {
        #region Fields

        private readonly Context context;
        private readonly IList<ITokenExtractor> tokenExtractors;

        #endregion

        #region Constructors

        // TODO: This should be passed a collection of ITokenExtractors rather than constructing them itself.
        public Tokeniser(Context context)
        {
            this.context = context;

            this.tokenExtractors = new List<ITokenExtractor>
            {
                new KeywordTokenExtractor(context.FunctionNames),
                new KeywordTokenExtractor(context.OperatorNames),
                // Variables
                new ParenthesisedTokenExtractor('[', ']'),
                new NumericTokenExtractor(),
                // Dates
                new ParenthesisedTokenExtractor('#'),
                new ValueTokenExtractor(","),
                new ParenthesisedTokenExtractor('"'),
                new ParenthesisedTokenExtractor('\''),
                // TODO: Probably a better way to achieve this.
                new ValueTokenExtractor("true"),
                new ValueTokenExtractor("TRUE"),
                new ValueTokenExtractor("false"),
                new ValueTokenExtractor("FALSE"),
                new ValueTokenExtractor("null"),
                new ValueTokenExtractor("NULL")
            };
        }

        #endregion

        #region Internal Methods

        internal IList<Token> Tokenise(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            var expressionLength = expression.Length;
            var tokens = new List<Token>();
            IList<char> unrecognised = null;

            var index = 0;

            while (index < expressionLength)
            {
                var foundUnrecognisedCharacter = false;

                var token = this.tokenExtractors.Select(t => t.ExtractToken(expression, index, this.context)).FirstOrDefault(t => t != null);

                if (token != null)
                {
                    tokens.Add(token);
                }
                else
                {
                    var character = expression[index];

                    if (!char.IsWhiteSpace(character))
                    {
                        // If we don't recognise this item then we had better keep going until we find something we know about.
                        if (unrecognised == null)
                        {
                            unrecognised = new List<char>();
                        }

                        foundUnrecognisedCharacter = true;
                        unrecognised.Add(character);
                    }
                }

                // Clear down the unrecognised buffer;
                if (!foundUnrecognisedCharacter)
                {
                    CheckForUnrecognised(unrecognised, tokens, index);
                    unrecognised = null;
                }

                index += token?.Length ?? 1;
            }

            // Double check whether the last part is unrecognised.
            CheckForUnrecognised(unrecognised, tokens, index);

            return tokens;
        }

        #endregion

        #region Private Methods

        private static void CheckForUnrecognised(IList<char> unrecognised, IList<Token> tokens, int index)
        {
            if (unrecognised == null)
            {
                return;
            }

            var currentToken = new string(unrecognised.ToArray());
            tokens.Add(new Token(currentToken, index - currentToken.Length)); // The index supplied is the current location not the start of the unrecoginsed token.
        }

        #endregion
    }
}