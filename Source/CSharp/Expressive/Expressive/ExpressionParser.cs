using Expressive.Exceptions;
using Expressive.Expressions;
using Expressive.Functions;
using Expressive.Functions.Conversion;
using Expressive.Functions.Date;
using Expressive.Functions.Logical;
using Expressive.Functions.Mathematical;
using Expressive.Functions.Statistical;
using Expressive.Functions.String;
using Expressive.Operators;
using Expressive.Operators.Additive;
using Expressive.Operators.Bitwise;
using Expressive.Operators.Conditional;
using Expressive.Operators.Grouping;
using Expressive.Operators.Logical;
using Expressive.Operators.Multiplicative;
using Expressive.Operators.Relational;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Expressive.Functions.Relational;

namespace Expressive
{
    internal sealed class ExpressionParser
    {
        #region Constants

        private const char DateSeparator = '#';
        //private const char DecimalSeparator = '.';
        private const char ParameterSeparator = ',';

        #endregion

        #region Fields

        private readonly CultureInfo currentCulture;
        private readonly char decimalSeparator;
        private readonly ExpressiveOptions options;
        private readonly IDictionary<string, Func<IExpression[], IDictionary<string, object>, object>> registeredFunctions;
        private readonly IDictionary<string, IOperator> registeredOperators;
        private readonly StringComparer stringComparer;

        #endregion

        #region Constructors

        internal ExpressionParser(ExpressiveOptions options)
        {
            this.options = options;
            
            // For now we will ignore any specific cultures but keeping it in a single place to simplify changing later if required.
            this.currentCulture = CultureInfo.InvariantCulture;

            // Initialise the string comparer only once.
            this.stringComparer = this.options.HasFlag(ExpressiveOptions.IgnoreCase) ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;

            this.decimalSeparator = Convert.ToChar(this.currentCulture.NumberFormat.NumberDecimalSeparator);
            this.registeredFunctions = new Dictionary<string, Func<IExpression[], IDictionary<string, object>, object>>(this.GetDictionaryComparer(options));
            this.registeredOperators = new Dictionary<string, IOperator>(this.GetDictionaryComparer(options));

            #region Operators
            // TODO: Do we allow for turning off operators?
            // Additive
            this.RegisterOperator(new PlusOperator());
            this.RegisterOperator(new SubtractOperator());
            // Bitwise
            this.RegisterOperator(new BitwiseAndOperator());
            this.RegisterOperator(new BitwiseOrOperator());
            this.RegisterOperator(new BitwiseExclusiveOrOperator());
            this.RegisterOperator(new LeftShiftOperator());
            this.RegisterOperator(new RightShiftOperator());
            // Conditional
            this.RegisterOperator(new NullCoalescingOperator());
            // Grouping
            this.RegisterOperator(new ParenthesisCloseOperator());
            this.RegisterOperator(new ParenthesisOpenOperator());
            // Logic
            this.RegisterOperator(new AndOperator());
            this.RegisterOperator(new NotOperator());
            this.RegisterOperator(new OrOperator());
            // Multiplicative
            this.RegisterOperator(new DivideOperator());
            this.RegisterOperator(new ModulusOperator());
            this.RegisterOperator(new MultiplyOperator());
            // Relational
            this.RegisterOperator(new EqualOperator());
            this.RegisterOperator(new GreaterThanOperator());
            this.RegisterOperator(new GreaterThanOrEqualOperator());
            this.RegisterOperator(new LessThanOperator());
            this.RegisterOperator(new LessThanOrEqualOperator());
            this.RegisterOperator(new NotEqualOperator());
            #endregion

            #region Functions
            // Conversion
            this.RegisterFunction(new DateFunction());
            this.RegisterFunction(new DecimalFunction());
            this.RegisterFunction(new DoubleFunction());
            this.RegisterFunction(new IntegerFunction());
            this.RegisterFunction(new LongFunction());
            this.RegisterFunction(new StringFunction());
            // Date
            this.RegisterFunction(new AddDaysFunction());
            this.RegisterFunction(new AddHoursFunction());
            this.RegisterFunction(new AddMillisecondsFunction());
            this.RegisterFunction(new AddMinutesFunction());
            this.RegisterFunction(new AddMonthsFunction());
            this.RegisterFunction(new AddSecondsFunction());
            this.RegisterFunction(new AddYearsFunction());
            this.RegisterFunction(new DayOfFunction());
            this.RegisterFunction(new DaysBetweenFunction());
            this.RegisterFunction(new HourOfFunction());
            this.RegisterFunction(new HoursBetweenFunction());
            this.RegisterFunction(new MillisecondOfFunction());
            this.RegisterFunction(new MillisecondsBetweenFunction());
            this.RegisterFunction(new MinuteOfFunction());
            this.RegisterFunction(new MinutesBetweenFunction());
            this.RegisterFunction(new MonthOfFunction());
            this.RegisterFunction(new SecondOfFunction());
            this.RegisterFunction(new SecondsBetweenFunction());
            this.RegisterFunction(new YearOfFunction());
            // Mathematical
            this.RegisterFunction(new AbsFunction());
            this.RegisterFunction(new AcosFunction());
            this.RegisterFunction(new AsinFunction());
            this.RegisterFunction(new AtanFunction());
            this.RegisterFunction(new CeilingFunction());
            this.RegisterFunction(new CosFunction());
            this.RegisterFunction(new CountFunction());
            this.RegisterFunction(new ExpFunction());
            this.RegisterFunction(new FloorFunction());
            this.RegisterFunction(new IEEERemainderFunction());
            this.RegisterFunction(new Log10Function());
            this.RegisterFunction(new LogFunction());
            this.RegisterFunction(new PowFunction());
            this.RegisterFunction(new RandomFunction());
            this.RegisterFunction(new RoundFunction());
            this.RegisterFunction(new SignFunction());
            this.RegisterFunction(new SinFunction());
            this.RegisterFunction(new SqrtFunction());
            this.RegisterFunction(new SumFunction());
            this.RegisterFunction(new TanFunction());
            this.RegisterFunction(new TruncateFunction());
            // Logical
            this.RegisterFunction(new IfFunction());
            this.RegisterFunction(new InFunction());
            // Relational
            this.RegisterFunction(new MaxFunction());
            this.RegisterFunction(new MinFunction());
            // Statistical
            this.RegisterFunction(new AverageFunction());
            this.RegisterFunction(new MeanFunction());
            this.RegisterFunction(new MedianFunction());
            this.RegisterFunction(new ModeFunction());
            // String
            this.RegisterFunction(new ContainsFunction());
            this.RegisterFunction(new EndsWithFunction());
            this.RegisterFunction(new LengthFunction());
            this.RegisterFunction(new PadLeftFunction());
            this.RegisterFunction(new PadRightFunction());
            this.RegisterFunction(new RegexFunction());
            this.RegisterFunction(new StartsWithFunction());
            this.RegisterFunction(new SubstringFunction());            
            #endregion
        }

        #endregion

        #region Internal Methods

        internal IExpression CompileExpression(string expression, IList<string> variables)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ExpressiveException("An Expression cannot be empty.");
            }

            var tokens = this.Tokenise(expression);

            var openCount = tokens.Select(t => t.CurrentToken).Count(t => string.Equals(t, "(", StringComparison.Ordinal));
            var closeCount = tokens.Select(t => t.CurrentToken).Count(t => string.Equals(t, ")", StringComparison.Ordinal));

            // Bail out early if there isn't a matching set of ( and ) characters.
            if (openCount > closeCount)
            {
                throw new ArgumentException("There aren't enough ')' symbols. Expected " + openCount + " but there is only " + closeCount);
            }
            else if (openCount < closeCount)
            {
                throw new ArgumentException("There are too many ')' symbols. Expected " + openCount + " but there is " + closeCount);
            }

            return this.CompileExpression(new Queue<Token>(tokens), OperatorPrecedence.Minimum, variables, false);
        }

        internal void RegisterFunction(string functionName, Func<IExpression[], IDictionary<string, object>, object> function)
        {
            this.CheckForExistingFunctionName(functionName);

            this.registeredFunctions.Add(functionName, function);
        }

        internal void RegisterFunction(IFunction function)
        {
            this.CheckForExistingFunctionName(function.Name);

            this.registeredFunctions.Add(function.Name, (p, a) =>
            {
                function.Variables = a;

                return function.Evaluate(p, this.options);
            });
        }

        internal void RegisterOperator(IOperator op)
        {
            foreach (var tag in op.Tags)
            {
                this.registeredOperators.Add(tag, op);
            }
        }

        internal void UnregisterFunction(string name)
        {
            if (this.registeredFunctions.ContainsKey(name))
            {
                this.registeredFunctions.Remove(name);
            }
        }

        #endregion

        #region Private Methods

        private IExpression CompileExpression(Queue<Token> tokens, OperatorPrecedence minimumPrecedence, IList<string> variables, bool isWithinFunction)
        {
            if (tokens == null)
            {
                throw new ArgumentNullException(nameof(tokens), "You must call Tokenise before compiling");
            }
            
            IExpression leftHandSide = null;
            var currentToken = tokens.PeekOrDefault();
            Token previousToken = null;

            while (currentToken != null)
            {
                if (this.registeredOperators.TryGetValue(currentToken.CurrentToken, out var op)) // Are we an IOperator?
                {
                    var precedence = op.GetPrecedence(previousToken);

                    if (precedence > minimumPrecedence)
                    {
                        tokens.Dequeue();

                        if (!op.CanGetCaptiveTokens(previousToken, currentToken, tokens))
                        {
                            // Do it anyway to update the list of tokens
                            op.GetCaptiveTokens(previousToken, currentToken, tokens);
                            break;
                        }
                        else
                        {
                            IExpression rightHandSide = null;

                            var captiveTokens = op.GetCaptiveTokens(previousToken, currentToken, tokens);

                            if (captiveTokens.Length > 1)
                            {
                                var innerTokens = op.GetInnerCaptiveTokens(captiveTokens);
                                rightHandSide = this.CompileExpression(new Queue<Token>(innerTokens), OperatorPrecedence.Minimum, variables, isWithinFunction);

                                currentToken = captiveTokens[captiveTokens.Length - 1];
                            }
                            else
                            {
                                rightHandSide = this.CompileExpression(tokens, precedence, variables, isWithinFunction);
                                // We are at the end of an expression so fake it up.
                                currentToken = new Token(")", -1);
                            }

                            leftHandSide = op.BuildExpression(previousToken, new[] { leftHandSide, rightHandSide }, this.options);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                else if (this.registeredFunctions.TryGetValue(currentToken.CurrentToken, out var function)) // or an IFunction?
                {
                    this.CheckForExistingParticipant(leftHandSide, currentToken, isWithinFunction);

                    var expressions = new List<IExpression>();
                    var captiveTokens = new Queue<Token>();
                    var parenCount = 0;
                    tokens.Dequeue();

                    // Loop through the list of tokens and split by ParameterSeparator character
                    while (tokens.Count > 0)
                    {
                        var nextToken = tokens.Dequeue();

                        if (string.Equals(nextToken.CurrentToken, "(", StringComparison.Ordinal))
                        {
                            parenCount++;
                        }
                        else if (string.Equals(nextToken.CurrentToken, ")", StringComparison.Ordinal))
                        {
                            parenCount--;
                        }

                        if (!(parenCount == 1 && nextToken.CurrentToken == "(") &&
                                !(parenCount == 0 && nextToken.CurrentToken == ")"))
                        {
                            captiveTokens.Enqueue(nextToken);
                        }

                        if (parenCount == 0 &&
                            captiveTokens.Any())
                        {
                            expressions.Add(this.CompileExpression(captiveTokens, minimumPrecedence: OperatorPrecedence.Minimum, variables: variables, isWithinFunction: true));
                            captiveTokens.Clear();
                        }
                        else if (string.Equals(nextToken.CurrentToken, ParameterSeparator.ToString(), StringComparison.Ordinal) && parenCount == 1)
                        {
                            // TODO: Should we expect expressions to be null???
                            expressions.Add(this.CompileExpression(captiveTokens, minimumPrecedence: 0, variables: variables, isWithinFunction: true));
                            captiveTokens.Clear();
                        }

                        if (parenCount <= 0)
                        {
                            break;
                        }
                    }

                    leftHandSide = new FunctionExpression(currentToken.CurrentToken, function, expressions.ToArray());
                }
                else if (currentToken.CurrentToken.IsNumeric(this.currentCulture)) // Or a number
                {
                    this.CheckForExistingParticipant(leftHandSide, currentToken, isWithinFunction);

                    tokens.Dequeue();

                    if (int.TryParse(currentToken.CurrentToken, NumberStyles.Any, this.currentCulture, out var intValue))
                    {
                        leftHandSide = new ConstantValueExpression(intValue);
                    }
                    else if (decimal.TryParse(currentToken.CurrentToken, NumberStyles.Any, this.currentCulture, out var decimalValue))
                    {
                        leftHandSide = new ConstantValueExpression(decimalValue);
                    }
                    else if (double.TryParse(currentToken.CurrentToken, NumberStyles.Any, this.currentCulture, out var doubleValue))
                    {
                        leftHandSide = new ConstantValueExpression(doubleValue);
                    }
                    else if (float.TryParse(currentToken.CurrentToken, NumberStyles.Any, this.currentCulture, out var floatValue))
                    {
                        leftHandSide = new ConstantValueExpression(floatValue);
                    }
                    else if (long.TryParse(currentToken.CurrentToken, NumberStyles.Any, this.currentCulture, out var longValue))
                    {
                        leftHandSide = new ConstantValueExpression(longValue);
                    }
                }
                else if (currentToken.CurrentToken.StartsWith("[") && currentToken.CurrentToken.EndsWith("]")) // or a variable?
                {
                    this.CheckForExistingParticipant(leftHandSide, currentToken, isWithinFunction);

                    tokens.Dequeue();
                    var variableName = currentToken.CurrentToken.Replace("[", "").Replace("]", "");
                    leftHandSide = new VariableExpression(variableName);

                    if (!variables.Contains(variableName, this.stringComparer))
                    {
                        variables.Add(variableName);
                    }
                }
                else if (string.Equals(currentToken.CurrentToken, "true", StringComparison.OrdinalIgnoreCase)) // or a boolean?
                {
                    this.CheckForExistingParticipant(leftHandSide, currentToken, isWithinFunction);

                    tokens.Dequeue();
                    leftHandSide = new ConstantValueExpression(true);
                }
                else if (string.Equals(currentToken.CurrentToken, "false", StringComparison.OrdinalIgnoreCase))
                {
                    this.CheckForExistingParticipant(leftHandSide, currentToken, isWithinFunction);

                    tokens.Dequeue();
                    leftHandSide = new ConstantValueExpression(false);
                }
                else if (string.Equals(currentToken.CurrentToken, "null", StringComparison.OrdinalIgnoreCase)) // or a null?
                {
                    this.CheckForExistingParticipant(leftHandSide, currentToken, isWithinFunction);

                    tokens.Dequeue();
                    leftHandSide = new ConstantValueExpression(null);
                }
                else if (currentToken.CurrentToken.StartsWith(DateSeparator.ToString()) && currentToken.CurrentToken.EndsWith(DateSeparator.ToString())) // or a date?
                {
                    this.CheckForExistingParticipant(leftHandSide, currentToken, isWithinFunction);

                    tokens.Dequeue();

                    var dateToken = currentToken.CurrentToken.Replace(DateSeparator.ToString(), "");

                    // If we can't parse the date let's check for some known tags.
                    if (!DateTime.TryParse(dateToken, out var date))
                    {
                        if (string.Equals("TODAY", dateToken, StringComparison.OrdinalIgnoreCase))
                        {
                            date = DateTime.Today;
                        }
                        else if (string.Equals("NOW", dateToken, StringComparison.OrdinalIgnoreCase))
                        {
                            date = DateTime.Now;
                        }
                        else
                        {
                            throw new UnrecognisedTokenException(dateToken);
                        }
                    }

                    leftHandSide = new ConstantValueExpression(date);
                }
                else if ((currentToken.CurrentToken.StartsWith("'") && currentToken.CurrentToken.EndsWith("'")) ||
                    (currentToken.CurrentToken.StartsWith("\"") && currentToken.CurrentToken.EndsWith("\"")))
                {
                    this.CheckForExistingParticipant(leftHandSide, currentToken, isWithinFunction);

                    tokens.Dequeue();
                    leftHandSide = new ConstantValueExpression(CleanString(currentToken.CurrentToken.Substring(1, currentToken.Length - 2)));
                }
                else if (string.Equals(currentToken.CurrentToken, ParameterSeparator.ToString(), StringComparison.Ordinal)) // Make sure we ignore the parameter separator
                {
                    if (!isWithinFunction)
                    {
                        throw new ExpressiveException($"Unexpected token '{currentToken}'");
                    }
                    tokens.Dequeue();
                }
                else
                {
                    tokens.Dequeue();

                    throw new UnrecognisedTokenException(currentToken.CurrentToken);
                }

                previousToken = currentToken;
                currentToken = tokens.PeekOrDefault();
            }

            return leftHandSide;
        }

        private static string CleanString(string input)
        {
            if (input.Length <= 1) return input;

            // the input string can only get shorter
            // so init the buffer so we won't have to reallocate later
            char[] buffer = new char[input.Length];
            int outIdx = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '\\')
                {
                    if (i < input.Length - 1)
                    {
                        switch (input[i + 1])
                        {
                            case 'n':
                                buffer[outIdx++] = '\n';
                                i++;
                                continue;
                            case 'r':
                                buffer[outIdx++] = '\r';
                                i++;
                                continue;
                            case 't':
                                buffer[outIdx++] = '\t';
                                i++;
                                continue;
                            case '\'':
                                buffer[outIdx++] = '\'';
                                i++;
                                continue;
                            case '\"':
                                buffer[outIdx++] = '\"';
                                i++;
                                continue;
                            case '\\':
                                buffer[outIdx++] = '\\';
                                i++;
                                continue;
                        }
                    }
                }

                buffer[outIdx++] = c;
            }

            return new string(buffer, 0, outIdx);
        }

        private static bool CanExtractValue(string expression, int expressionLength, int index, string value)
        {
            return string.Equals(value, ExtractValue(expression, expressionLength, index, value), StringComparison.OrdinalIgnoreCase);
        }

        private static bool CanGetString(string expression, int startIndex, char quoteCharacter)
        {
            return !string.IsNullOrWhiteSpace(GetString(expression, startIndex, quoteCharacter));
        }

        private void CheckForExistingFunctionName(string functionName)
        {
            if (this.registeredFunctions.ContainsKey(functionName))
            {
                throw new FunctionNameAlreadyRegisteredException(functionName);
            }
        }

        private void CheckForExistingParticipant(IExpression participant, Token token, bool isWithinFunction)
        {
            if (participant != null)
            {
                if (isWithinFunction)
                {
                    throw new MissingTokenException("Missing token, expecting ','.", ',');
                }
                
                throw new ExpressiveException($"Unexpected token '{token.CurrentToken}' at index {token.StartIndex}");
            }
        }

        private static bool CheckForTag(string tag, string lookAhead, ExpressiveOptions options)
        {
            return (options.HasFlag(ExpressiveOptions.IgnoreCase) &&
                string.Equals(lookAhead, tag, StringComparison.OrdinalIgnoreCase)) ||
                string.Equals(lookAhead, tag, StringComparison.Ordinal);
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

        private StringComparer GetDictionaryComparer(ExpressiveOptions options)
        {
            return options.HasFlag(ExpressiveOptions.IgnoreCase) ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;
        }

        private string GetNumber(string expression, int startIndex)
        {
            bool hasDecimalPoint = false;
            var index = startIndex;
            var character = expression[index];

            while ((index < expression.Length) &&
                (char.IsDigit(character) || (!hasDecimalPoint && character == this.decimalSeparator)))
            {
                if (!hasDecimalPoint && character == this.decimalSeparator)
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

        private IList<Token> Tokenise(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            var expressionLength = expression.Length;
            var operators = this.registeredOperators.OrderByDescending(op => op.Key.Length);
            var tokens = new List<Token>();
            IList<char> unrecognised = null;

            var index = 0;

            while (index < expressionLength)
            {
                var lengthProcessed = 0;
                bool foundUnrecognisedCharacter = false;

                // Functions would tend to have longer tags so check for these first.
                foreach (var kvp in this.registeredFunctions.OrderByDescending(f => f.Key.Length))
                {
                    var lookAhead = expression.Substring(index, Math.Min(kvp.Key.Length, expressionLength - index));

                    if (CheckForTag(kvp.Key, lookAhead, this.options))
                    {
                        CheckForUnrecognised(unrecognised, tokens, index);
                        lengthProcessed = kvp.Key.Length;
                        tokens.Add(new Token(lookAhead, index));
                        break;
                    }
                }

                if (lengthProcessed == 0)
                {
                    // Loop through and find any matching operators.
                    foreach (var op in operators)
                    {
                        var lookAhead = expression.Substring(index, Math.Min(op.Key.Length, expressionLength - index));

                        if (CheckForTag(op.Key, lookAhead, this.options))
                        {
                            CheckForUnrecognised(unrecognised, tokens, index);
                            lengthProcessed = op.Key.Length;
                            tokens.Add(new Token(lookAhead, index));
                            break;
                        }
                    }
                }

                // If an operator wasn't found then process the current character.
                if (lengthProcessed == 0)
                {
                    var character = expression[index];

                    if (character == '[')
                    {
                        char closingCharacter = ']';

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
                    else if (character == DateSeparator)
                    {
                        if (!CanGetString(expression, index, character))
                        {
                            throw new MissingTokenException($"Missing closing token '{character}'", character);
                        }

                        // Ignore the first # when checking to allow us to find the second.
                        var dateString = "#" + expression.SubstringUpTo(index + 1, DateSeparator);

                        CheckForUnrecognised(unrecognised, tokens, index);
                        tokens.Add(new Token(dateString, index));
                        lengthProcessed = dateString.Length;
                    }
                    else if (character == ParameterSeparator)
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

        private static void CheckForUnrecognised(IList<char> unrecognised, IList<Token> tokens, int index)
        {
            if (unrecognised != null)
            {
                string currentToken = new string(unrecognised.ToArray());
                tokens.Add(new Token(currentToken, index - currentToken.Length)); // The index supplied is the current location not the start of the unrecoginsed token.
            }
        }

        #endregion
    }
}
