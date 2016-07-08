package com.bijington.expressive;

import com.bijington.expressive.exceptions.MissingTokenException;
import com.bijington.expressive.exceptions.UnrecognisedTokenException;
import com.bijington.expressive.expressions.ConstantValueExpression;
import com.bijington.expressive.expressions.FunctionExpression;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.expressions.VariableExpression;
import com.bijington.expressive.functions.IFunction;
import com.bijington.expressive.helpers.Strings;
import com.bijington.expressive.operators.Conditional.NullCoalescingOperator;
import com.bijington.expressive.operators.IOperator;
import com.bijington.expressive.operators.OperatorPrecedence;

import java.text.DateFormat;
import java.text.NumberFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.function.Function;

/**
 * Created by shaun on 27/06/2016.
 */
class ExpressionParser {

    private static final Character DATE_SEPARATOR = '#';
    private static final Character DECIMAL_SEPARATOR = '.';
    private static final Character PARAMETER_SEPARATOR = ',';

    private char _decimalSeparator;
    private final EnumSet<ExpressiveOptions> _options;
    private final Map<String, IFunction> _registeredFunctions;
    //private Map<String, Function<IExpression[], Map<String, Object>, Object>> _registeredFunctions;
    private final Map<String, IOperator> _registeredOperators;

    ExpressionParser(EnumSet<ExpressiveOptions> options) {
        _options = options;

        _registeredFunctions = new HashMap<>();
        _registeredOperators = new HashMap<>();

        registerOperator(new NullCoalescingOperator());
    }

    void registerFunction(IFunction function) {
        _registeredFunctions.put(function.getName(), function);
    }
    /*void RegisterFunction(String functionName, Function<IExpression[], Map<String, Object>, Object> function)
    {
        _registeredFunctions.Add(functionName, function);
    }

    void RegisterFunction(IFunction function)
    {
        _registeredFunctions.Add(function.getName(), (p, a) ->
        {
            function.setVariables(a);

            return function.Evaluate(p);
        });
    }*/

    void registerOperator(IOperator op) {
        for (String tag: op.getTags()) {
            _registeredOperators.put(tag, op);
        }
    }

    void unregisterFunction(String name) {
        if (_registeredFunctions.containsKey(name)) {
            _registeredFunctions.remove(name);
        }
    }

    IExpression compileExpression(String expression, List<String> variables) throws MissingTokenException, UnrecognisedTokenException {
        if (expression == null)
        {
            //throw new ArgumentNullException("expression");
        }

        List<String> tokens = tokenise(expression);

        int openCount = 0;
        int closeCount = 0;

        for (String t: tokens) {
            if (t == "(") {
                openCount++;
            }
            else if (t == ")") {
                closeCount++;
            }
        }
//        var openCount = tokens.Count(t => string.Equals(t, "(", StringComparison.Ordinal));
//        var closeCount = tokens.Count(t => string.Equals(t, ")", StringComparison.Ordinal));
//
//        // Bail out early if there isn't a matching set of ( and ) characters.
//        if (openCount > closeCount) {
//            throw new ArgumentException("There aren't enough ')' symbols. Expected " + openCount + " but there is only " + closeCount);
//        }
//        else if (openCount < closeCount) {
//            throw new ArgumentException("There are too many ')' symbols. Expected " + openCount + " but there is " + closeCount);
//        }

        return compileExpression(tokens, OperatorPrecedence.Minimum, variables);
    }

    private static Boolean canGetString(String expression, int startIndex, char quoteCharacter) {
        return !Strings.isNullOrEmpty(getString(expression, startIndex, quoteCharacter));
    }

    private static Boolean canExtractValue(String expression, int expressionLength, int index, String value) {
        // TODO should this be case insensitive?
        return value.equalsIgnoreCase(extractValue(expression, expressionLength, index, value));
    }

    private static Boolean checkForTag(String tag, String lookAhead, EnumSet<ExpressiveOptions> options) {
        return (options.contains(ExpressiveOptions.IGNORE_CASE) &&
                lookAhead.equalsIgnoreCase(tag)) ||
                lookAhead.equals(tag);
    }

    private static void checkForUnrecognised(StringBuilder unrecognised, List<String> tokens) {
        if (unrecognised != null) {
            tokens.add(unrecognised.toString());
        }
    }

    private static String cleanString(String input) {
        int inputLength = input.length();

        if (inputLength <= 1) return input;

        // the input string can only get shorter
        // so init the buffer so we won't have to reallocate later
        char[] buffer = new char[inputLength];
        int outIdx = 0;
        for (int i = 0; i < inputLength; i++)
        {
            char c = input.charAt(i);
            if (c == '\\')
            {
                if (i < inputLength - 1)
                {
                    switch (input.charAt(i + 1))
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
                    }
                }
            }

            buffer[outIdx++] = c;
        }

        return new String(buffer, 0, outIdx);
    }

    private IExpression compileExpression(List<String> tokens, int minimumPrecedence, List<String> variables) throws UnrecognisedTokenException {
        IExpression leftHandSide = null;
        String currentToken = tokens.stream().findFirst().orElseGet(null);

        String previousToken = null;

        while (currentToken != null) {
            IOperator op = _registeredOperators.getOrDefault(currentToken, null);
            IFunction function = _registeredFunctions.getOrDefault(currentToken, null);
            Number number = Strings.parseNumber(currentToken);

            // Are we an IOperator?
            if (op != null) {
                int precedence = op.getPrecedence(previousToken);

                if (precedence > minimumPrecedence) {
                    tokens.remove(0);

                    if (!op.canGetCaptiveTokens(previousToken, currentToken, tokens)) {
                        // Do it anyway to update the list of tokens
                        op.getCaptiveTokens(previousToken, currentToken, tokens);
                        break;
                    }
                    else {
                        IExpression rightHandSide = null;

                        String[] captiveTokens = op.getCaptiveTokens(previousToken, currentToken, tokens);

                        if (captiveTokens.length > 1) {
                            String[] innerTokens = op.getInnerCaptiveTokens(captiveTokens);
                            rightHandSide = compileExpression(Arrays.asList(innerTokens), OperatorPrecedence.Minimum, variables);

                            currentToken = captiveTokens[captiveTokens.length - 1];
                        }
                        else {
                            rightHandSide = compileExpression(tokens, precedence, variables);
                            // We are at the end of an expression so fake it up.
                            currentToken = ")";
                        }

                        leftHandSide = op.buildExpression(previousToken, new IExpression[] { leftHandSide, rightHandSide });
                    }
                }
                else {
                    break;
                }
            }
            else if (function != null) {
                List<IExpression> expressions = new ArrayList<IExpression>();
                List<String> captiveTokens = new ArrayList<String>();
                int parenCount = 0;
                tokens.remove(0);

                // Loop through the list of tokens and split by ParameterSeparator character
                while (tokens.size() > 0) {
                    String nextToken = tokens.remove(0);

                    if (nextToken.equals("(")) {
                        parenCount++;
                    }
                    else if (nextToken.equals(")")) {
                       parenCount--;
                    }

                    if (!(parenCount == 1 && nextToken == "(") &&
                        !(parenCount == 0 && nextToken == ")")) {
                       captiveTokens.add(nextToken);
                    }

                    if (parenCount == 0 &&
                        captiveTokens.size() > 0) {
                        expressions.add(compileExpression(captiveTokens, OperatorPrecedence.Minimum, variables));
                        captiveTokens.clear();
                    }
                    else if (nextToken.equals(PARAMETER_SEPARATOR.toString()) && parenCount == 1) {
                       // TODO: Should we expect expressions to be null???
                       expressions.add(compileExpression(captiveTokens, OperatorPrecedence.Minimum, variables));
                       captiveTokens.clear();
                    }

                    if (parenCount <= 0)
                    {
                       break;
                    }
                }

                IExpression[] expressionArray = new IExpression[expressions.size()];
                expressionArray = expressions.toArray(expressionArray);

                leftHandSide = new FunctionExpression(currentToken, function, expressionArray);
            }
            else if (number != null) {
                tokens.remove(0);

                leftHandSide = new ConstantValueExpression(number);
            }
            else if (currentToken.startsWith("[") && currentToken.endsWith("]")) {
                tokens.remove(0);
                String variableName = currentToken.replace("[", "").replace("]", "");
                leftHandSide = new VariableExpression(variableName);

                if (!variables.contains(variableName)) {
                    variables.add(variableName);
                }
            }
            else if (currentToken.equalsIgnoreCase("true")) {
                tokens.remove(0);
                leftHandSide = new ConstantValueExpression(true);
            }
            else if (currentToken.equalsIgnoreCase("false")) {
                tokens.remove(0);
                leftHandSide = new ConstantValueExpression(false);
            }
            else if (currentToken.equalsIgnoreCase("null")) {
                tokens.remove(0);
                leftHandSide = new ConstantValueExpression(null);
            }
            else if (currentToken.startsWith(DATE_SEPARATOR.toString()) && currentToken.endsWith(DATE_SEPARATOR.toString())) {
                tokens.remove(0);

                String dateToken = currentToken.replace(DATE_SEPARATOR.toString(), "");
                Date date = null;

                DateFormat dateFormat = new SimpleDateFormat();

                // TODO this might be required.
                //dateFormat.setLenient(true);

                if (dateToken.equals("Today")) {

                }
                else if (dateToken.equals("Now")) {
                    date = new Date();
                }
                else {
                    try {
                        date = dateFormat.parse(dateToken);
                    } catch (ParseException pe) {
                        throw new UnrecognisedTokenException(dateToken);
                    }
                }

                leftHandSide = new ConstantValueExpression(date);
            }
            else if ((currentToken.startsWith("'") && currentToken.endsWith("'")) ||
                     (currentToken.startsWith("\"") && currentToken.endsWith("\""))) {
                tokens.remove(0);
                leftHandSide = new ConstantValueExpression(cleanString(currentToken.substring(1, currentToken.length() - 1)));
            }
            else if (currentToken.equals(PARAMETER_SEPARATOR.toString())) {
                // Make sure we ignore the parameter separator
                tokens.remove(0);
            }
            else {
                tokens.remove(0);

                throw new UnrecognisedTokenException(currentToken);
            }

            previousToken = currentToken;

            currentToken = !tokens.isEmpty() ? tokens.get(0) : null;
        }

        return leftHandSide;
    }

    private static String extractValue(String expression, int expressionLength, int index, String expectedValue) {
        String result = null;
        int valueLength = expectedValue.length();

        if (expressionLength >= index + valueLength) {
            String valueString = expression.substring(index, index + valueLength);
            Boolean isValidValue = true;

            if (expressionLength > index + valueLength) {
                // If the next character is not a continuation of the word then we are valid.
                isValidValue = !Character.isLetterOrDigit(expression.charAt(index + valueLength));
            }

            if (valueString.equals(expectedValue) &&
                isValidValue) {
                result = valueString;
            }
        }

        return result;
    }

    private String getNumber(String expression, int startIndex) {
        Boolean hasDecimalPoint = false;
        int index = startIndex;
        Character character = expression.charAt(index);
        int expressionLength = expression.length();

        while ((index < expressionLength) &&
                (Character.isDigit(character) || (!hasDecimalPoint && character == _decimalSeparator))) {
            if (!hasDecimalPoint && character == _decimalSeparator) {
                hasDecimalPoint = true;
            }

            index++;

            if (index == expressionLength) {
                break;
            }

            character = expression.charAt(index);
        }

        return expression.substring(startIndex, index);
    }

    private static String getString(String expression, int startIndex, char quoteCharacter) {
        int index = startIndex;
        Boolean foundEndingQuote = false;
        Character character = expression.charAt(index);
        Character previousCharacter = Character.MIN_VALUE;
        int expressionLength = expression.length();

        while (index < expressionLength && !foundEndingQuote) {
            // Make sure the escape character wasn't previously used.
            if (index != startIndex &&
                    character == quoteCharacter &&
                    previousCharacter != '\\') {
                foundEndingQuote = true;
            }

            previousCharacter = character;
            index++;

            if (index == expressionLength) {
                break;
            }

            character = expression.charAt(index);
        }

        if (foundEndingQuote) {
            return expression.substring(startIndex, index);
        }

        return null;
    }

    private static Boolean isQuote(Character character) {
        return character == '\'' || character == '\"';
    }

    private List<String> tokenise(String expression) throws MissingTokenException {
        if (expression == null || expression.length() == 0) {
            return  null;
        }

        int expressionLength = expression.length();
        List<String> operators = new ArrayList<>(_registeredOperators.keySet());
        Comparator<String> x = new Comparator<String>()
        {
            @Override
            public int compare(String o1, String o2)
            {
                if(o1.length() > o2.length())
                    return -1;

                if(o2.length() > o1.length())
                    return 1;

                return 0;
            }
        };
        Collections.sort(operators,  x);
        List<String> tokens = new ArrayList<>();
        StringBuilder unrecognised = null;

        int index = 0;

        while (index < expressionLength) {
            int lengthProcessed = 0;
            Boolean foundUnrecognisedCharacter = false;

            // Functions would tend to have longer tags so check for these first.
            for (String key: _registeredFunctions.keySet()) {
                String lookAhead = expression.substring(index, Math.min(index + key.length(), expressionLength));

                if (checkForTag(key, lookAhead, _options)) {
                    checkForUnrecognised(unrecognised, tokens);
                    lengthProcessed = key.length();
                    tokens.add(lookAhead);
                    break;
                }
            }

            if (lengthProcessed == 0) {
                for (String tag: operators) {
                    String lookAhead = expression.substring(index, Math.min(index + tag.length(), expressionLength));

                    if (checkForTag(tag, lookAhead, _options)) {
                        checkForUnrecognised(unrecognised, tokens);
                        lengthProcessed = tag.length();
                        tokens.add(lookAhead);
                        break;
                    }
                }
            }

            // If an operator wasn't found then process the current character.
            if (lengthProcessed == 0) {
                Character character = expression.charAt(index);

                if (character == '[') {
                    Character closingCharacter = ']';

                    if (!canGetString(expression, index, closingCharacter)) {
                        throw new MissingTokenException("Missing closing token '{closingCharacter}'", closingCharacter);
                    }

                    String variable = expression.substring(index, expression.indexOf(closingCharacter, index));

                    checkForUnrecognised(unrecognised, tokens);
                    tokens.add(variable);
                    lengthProcessed = variable.length();
                }
                else if (Character.isDigit(character)) {
                    String number = getNumber(expression, index);

                    checkForUnrecognised(unrecognised, tokens);
                    tokens.add(number);
                    lengthProcessed = number.length();
                }
                else if (isQuote(character)) {
                    if (!canGetString(expression, index, character)) {
                        throw new MissingTokenException("Missing closing token '{character}'", character);
                    }

                    String text = getString(expression, index, character);

                    checkForUnrecognised(unrecognised, tokens);
                    tokens.add(text);
                    lengthProcessed = text.length();
                }
                else if (character == DATE_SEPARATOR) {
                    if (!canGetString(expression, index, character)) {
                        throw new MissingTokenException("Missing closing token '{character}'", character);
                    }

                    // Ignore the first # when checking to allow us to find the second.
                    String dateString = "#" + expression.substring(index + 1, expression.indexOf(index + 1, DATE_SEPARATOR));

                    checkForUnrecognised(unrecognised, tokens);
                    tokens.add(dateString);
                    lengthProcessed = dateString.length();
                }
                else if (character == PARAMETER_SEPARATOR) {
                    checkForUnrecognised(unrecognised, tokens);
                    tokens.add(character.toString());
                    lengthProcessed = 1;
                }
                else if ((character == 't' || character == 'T') && canExtractValue(expression, expressionLength, index, "true")) {
                    checkForUnrecognised(unrecognised, tokens);
                    String trueString = extractValue(expression, expressionLength, index, "true");

                    if (!Strings.isNullOrEmpty(trueString)) {
                        tokens.add(trueString);
                        lengthProcessed = 4;
                    }
                }
                else if ((character == 'f' || character == 'F') && canExtractValue(expression, expressionLength, index, "false")) {
                    checkForUnrecognised(unrecognised, tokens);
                    String falseString = extractValue(expression, expressionLength, index, "false");

                    if (!Strings.isNullOrEmpty(falseString)) {
                        tokens.add(falseString);
                        lengthProcessed = 5;
                    }
                }
                else if (character == 'n' && canExtractValue(expression, expressionLength, index, "null")) {
                    checkForUnrecognised(unrecognised, tokens);
                    String nullString = extractValue(expression, expressionLength, index, "null");

                    if (!Strings.isNullOrEmpty(nullString))
                    {
                        tokens.add(nullString);
                        lengthProcessed = 4;
                    }
                }
                else if (!Character.isWhitespace(character))
                {
                    // If we don't recognise this item then we had better keep going until we find something we know about.
                    if (unrecognised == null)
                    {
                        unrecognised = new StringBuilder();
                    }

                    foundUnrecognisedCharacter = true;
                    unrecognised.append(character);
                }
            }

            // Clear down the unrecognised buffer;
            if (!foundUnrecognisedCharacter) {
                checkForUnrecognised(unrecognised, tokens);
                unrecognised = null;
            }
            index += (lengthProcessed == 0) ? 1 : lengthProcessed;
        }

        // Double check whether the last part is unrecognised.
        checkForUnrecognised(unrecognised, tokens);

        return tokens;
    }
}