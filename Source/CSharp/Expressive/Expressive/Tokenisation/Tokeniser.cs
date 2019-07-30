using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Expressive.Exceptions;
using Expressive.Expressions;
using Expressive.Operators;
using Expressive.Standard.Tokenisation;

namespace Expressive.Tokenisation
{
    internal class Tokeniser
    {
        #region Fields

        private readonly Context context;
        private readonly IList<ITokenIdentifier> tokenIdentifiers;

        #endregion

        public Tokeniser(Context context)
        {
            this.context = context;
            this.tokenIdentifiers = new List<ITokenIdentifier>();
            this.tokenIdentifiers.Add(new FunctionTokenIdentifier());
        }

        internal IList<Token> Tokenise(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            var expressionLength = expression.Length;
            var functionNames = this.context.FunctionNames;
            var operatorNames = this.context.OperatorNames;
            var tokens = new List<Token>();
            IList<char> unrecognised = null;

            var index = 0;

            while (index < expressionLength)
            {
                var foundUnrecognisedCharacter = false;

                

                // Functions would tend to have longer tags so check for these first.
                var lengthProcessed = FindMatch(
                    expression,
                    functionNames,
                    this.context.Options,
                    expressionLength,
                    index,
                    tokens,
                    unrecognised);

                if (lengthProcessed == 0)
                {
                    lengthProcessed = FindMatch(
                        expression,
                        operatorNames,
                        this.context.Options,
                        expressionLength,
                        index,
                        tokens,
                        unrecognised);
                }

                // If an operator wasn't found then process the current character.
                if (lengthProcessed == 0)
                {
                    var character = expression[index];

                    if (character == '[')
                    {
                        const char closingCharacter = ']';

                        if (!CanGetString(expression, index, closingCharacter))
                        {
                            throw new MissingTokenException($"Missing closing token '{closingCharacter}'", closingCharacter);
                        }

                        var variable = expression.SubstringUpTo(index, closingCharacter);

                        CheckForUnrecognised(unrecognised, tokens, index);
                        tokens.Add(new Token(variable, index));
                        lengthProcessed = variable.Length;
                    }
                    else if (char.IsDigit(character))
                    {
                        var number = GetNumber(expression, index);

                        CheckForUnrecognised(unrecognised, tokens, index);
                        tokens.Add(new Token(number, index));
                        lengthProcessed = number.Length;
                    }
                    else if (IsQuote(character))
                    {
                        if (!CanGetString(expression, index, character))
                        {
                            throw new MissingTokenException($"Missing closing token '{character}'", character);
                        }

                        var text = GetString(expression, index, character);

                        CheckForUnrecognised(unrecognised, tokens, index);
                        tokens.Add(new Token(text, index));
                        lengthProcessed = text.Length;
                    }
                    else if (character == Context.DateSeparator)
                    {
                        if (!CanGetString(expression, index, character))
                        {
                            throw new MissingTokenException($"Missing closing token '{character}'", character);
                        }

                        // Ignore the first # when checking to allow us to find the second.
                        var dateString = "#" + expression.SubstringUpTo(index + 1, Context.DateSeparator);

                        CheckForUnrecognised(unrecognised, tokens, index);
                        tokens.Add(new Token(dateString, index));
                        lengthProcessed = dateString.Length;
                    }
                    else if (character == Context.ParameterSeparator)
                    {
                        CheckForUnrecognised(unrecognised, tokens, index);
                        tokens.Add(new Token(character.ToString(), index));
                        lengthProcessed = 1;
                    }
                    else if ((character == 't' || character == 'T') && CanExtractValue(expression, expressionLength, index, "true"))
                    {
                        CheckForUnrecognised(unrecognised, tokens, index);
                        var trueString = ExtractValue(expression, expressionLength, index, "true");

                        if (!string.IsNullOrWhiteSpace(trueString))
                        {
                            tokens.Add(new Token(trueString, index));
                            lengthProcessed = 4;
                        }
                    }
                    else if ((character == 'f' || character == 'F') && CanExtractValue(expression, expressionLength, index, "false"))
                    {
                        CheckForUnrecognised(unrecognised, tokens, index);
                        var falseString = ExtractValue(expression, expressionLength, index, "false");

                        if (!string.IsNullOrWhiteSpace(falseString))
                        {
                            tokens.Add(new Token(falseString, index));
                            lengthProcessed = 5;
                        }
                    }
                    else if ((character == 'n' || character == 'N') && CanExtractValue(expression, expressionLength, index, "null")) // Check for null
                    {
                        CheckForUnrecognised(unrecognised, tokens, index);
                        var nullString = ExtractValue(expression, expressionLength, index, "null");

                        if (!string.IsNullOrWhiteSpace(nullString))
                        {
                            tokens.Add(new Token(nullString, index));
                            lengthProcessed = 4;
                        }
                    }
                    else if (!char.IsWhiteSpace(character))
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
                index += (lengthProcessed == 0) ? 1 : lengthProcessed;
            }

            // Double check whether the last part is unrecognised.
            CheckForUnrecognised(unrecognised, tokens, index);

            return tokens;
        }

        private static int FindMatch(
            string expression,
            IEnumerable<string> possibleNames,
            ExpressiveOptions options,
            int expressionLength,
            int index,
            IList<Token> tokens,
            IList<char> unrecognised)
        {
            foreach (var possibleName in possibleNames)
            {
                var lookAhead = expression.Substring(index, Math.Min(possibleName.Length, expressionLength - index));

                if (!CheckForTag(possibleName, lookAhead, options))
                {
                    continue;
                }

                CheckForUnrecognised(unrecognised, tokens, index);

                tokens.Add(new Token(lookAhead, index));

                return possibleName.Length;
            }

            return 0;
        }

        private static bool CanExtractValue(string expression, int expressionLength, int index, string value)
        {
            return string.Equals(value, ExtractValue(expression, expressionLength, index, value), StringComparison.OrdinalIgnoreCase);
        }

        private static bool CanGetString(string expression, int startIndex, char quoteCharacter)
        {
            return !string.IsNullOrWhiteSpace(GetString(expression, startIndex, quoteCharacter));
        }

        private static bool CheckForTag(string tag, string lookAhead, ExpressiveOptions options)
        {
            return (options.HasFlag(ExpressiveOptions.IgnoreCase) &&
                    string.Equals(lookAhead, tag, StringComparison.OrdinalIgnoreCase)) ||
                   string.Equals(lookAhead, tag, StringComparison.Ordinal);
        }

        private static void CheckForUnrecognised(IList<char> unrecognised, IList<Token> tokens, int index)
        {
            if (unrecognised == null)
            {
                return;
            }

            var currentToken = new string(unrecognised.ToArray());
            tokens.Add(new Token(currentToken, index - currentToken.Length)); // The index supplied is the current location not the start of the unrecoginsed token.
        }

        private static string ExtractValue(string expression, int expressionLength, int index, string expectedValue)
        {
            string result = null;
            int valueLength = expectedValue.Length;

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

        private string GetNumber(string expression, int startIndex)
        {
            bool hasDecimalPoint = false;
            var index = startIndex;
            var character = expression[index];

            while ((index < expression.Length) &&
                   (char.IsDigit(character) || (!hasDecimalPoint && character == this.context.DecimalSeparator)))
            {
                if (!hasDecimalPoint && character == this.context.DecimalSeparator)
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

        private static string GetString(string expression, int startIndex, char quoteCharacter)
        {
            int index = startIndex;
            bool foundEndingQuote = false;
            var character = expression[index];
            bool isEscape = false;

            char[] escapableCharacters = new char[] { '\\', '"', '\'', 't', 'n', 'r' };

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

        private static bool IsQuote(char character)
        {
            return character == '\'' || character == '\"';
        }
    }
}
