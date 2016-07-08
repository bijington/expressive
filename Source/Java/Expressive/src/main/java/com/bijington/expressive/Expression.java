//Copyright(c) 2016 Shaun Lawrence

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

package com.bijington.expressive;

import com.bijington.expressive.exceptions.MissingTokenException;
import com.bijington.expressive.exceptions.UnrecognisedTokenException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.IFunction;

import java.util.ArrayList;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.Map;
import java.util.function.Function;

/// <summary>
/// Class definition for an Expression that can be evaluated.
/// </summary>
public final class Expression {
    private IExpression _compiledExpression;
    private final EnumSet<ExpressiveOptions> _options;
    private final String _originalExpression;
    private final ExpressionParser _parser;
    private String[] _variables;

    /**
     * Gets a list of the Variable names that are contained within this Expression.
     * @return A list of the Variable names that are contained within this Expression.
     */
    public String[] getReferencedVariables() throws MissingTokenException, UnrecognisedTokenException {
        this.compileExpression();

        return _variables;
    }

    /**
     * Initializes a new instance of the Expression class with no options.
     * @param expression The expression to be evaluated.
     */
    public Expression(String expression) {
        this (expression, EnumSet.of(ExpressiveOptions.NONE));
    }

    /**
     * Initializes a new instance of the Expression class with the specified options.
     * @param expression The expression to be evaluated.
     * @param options The options to use when evaluating.
     */
    public Expression(String expression, EnumSet<ExpressiveOptions> options) {
        _originalExpression = expression;
        _options = options;

        _parser = new ExpressionParser(_options);
    }

    /**
     * Evaluates the expression and returns the result.
     * @exception MissingTokenException Thrown when the evaluator detects that a token has not been supplied.
     * @return The result of the expression.
     */
    /// <exception cref="Exceptions.ParameterCountMismatchException">Thrown when the evaluator detects a function does not have the expected number of parameters supplied.</exception>
    /// <exception cref="Exceptions.UnrecognisedTokenException">Thrown when the evaluator is unable to process a token in the expression.</exception>
    public Object evaluate() throws MissingTokenException, UnrecognisedTokenException {
        return evaluate(null);
    }

    /// <summary>
    /// Evaluates the expression using the supplied variables and returns the result.
    /// </summary>
    /// <exception cref="Exceptions.MissingTokenException">Thrown when the evaluator detects that a token has not been supplied.</exception>
    /// <exception cref="Exceptions.ParameterCountMismatchException">Thrown when the evaluator detects a function does not have the expected number of parameters supplied.</exception>
    /// <exception cref="Exceptions.UnrecognisedTokenException">Thrown when the evaluator is unable to process a token in the expression.</exception>
    /// <param name="variables">The variables to be used in the evaluation.</param>
    /// <returns>The result of the expression.</returns>
    public Object evaluate(Map<String, Object> variables) throws MissingTokenException, UnrecognisedTokenException {
        this.compileExpression();

        /*if (variables != null &&
                _options.HasFlag(ExpressiveOptions.IgnoreCase))
        {
            variables = new Dictionary<String, Object>(variables, StringComparer.OrdinalIgnoreCase);
        }*/

        Object result = _compiledExpression.evaluate(variables);

        return result;
    }

    /// <summary>
    /// Evaluates the expression asynchronously and returns the result via the callback.
    /// </summary>
    /// <exception cref="System.ArgumentNullException">Thrown if the callback is not supplied.</exception>
    /// <param name="callback">Provides the result once the evaluation has completed.</param>
    /*public void EvaluateAsync(Action<Object> callback)
    {
        EvaluateAsync(callback, null);
    }*/

    /// <summary>
    /// Evaluates the expression using the supplied variables asynchronously and returns the result via the callback.
    /// </summary>
    /// <exception cref="System.ArgumentNullException">Thrown if the callback is not supplied.</exception>
    /// <param name="callback">Provides the result once the evaluation has completed.</param>
    /// <param name="variables">The variables to be used in the evaluation.</param>
    /*public void EvaluateAsync(Action<Object> callback, IDictionary<String, Object> variables)
    {
        if (callback == null)
        {
            throw new ArgumentNullException("callback");
        }

        ThreadPool.QueueUserWorkItem((o) =>
                {
                        var result = this.Evaluate(variables);

        if (callback != null)
        {
            callback(result);
        }
        });
    }*/

    /// <summary>
    /// Registers a custom function for use in evaluating an expression.
    /// </summary>
    /// <param name="functionName">The name of the function (NOTE this is also the tag that will be used to extract the function from an expression).</param>
    /// <param name="function">The method of evaluating the function.</param>
//    public void RegisterFunction(String functionName, Function<IExpression[], Map<String, Object>, Object> function) {
//        _parser.RegisterFunction(functionName, function);
//    }

    /// <summary>
    /// Registers a custom function inheriting from <see cref="IFunction"/> for use in evaluating an expression.
    /// </summary>
    /// <param name="function">The <see cref="IFunction"/> implementation.</param>
    public void registerFunction(IFunction function) {
        _parser.registerFunction(function);
    }

    private void compileExpression() throws MissingTokenException, UnrecognisedTokenException {
        // Cache the expression to save us having to recompile.
        if (_compiledExpression == null ||
            _options.contains(ExpressiveOptions.NO_CACHE))
        {
            ArrayList<String> variables = new ArrayList<>();

            _compiledExpression = _parser.compileExpression(_originalExpression, variables);

            //_variables = variables.ToArray();
        }
    }
}
