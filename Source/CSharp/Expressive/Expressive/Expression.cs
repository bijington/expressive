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

using Expressive.Expressions;
using Expressive.Functions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Expressive
{
    /// <summary>
    /// Class definition for an Expression that can be evaluated.
    /// </summary>
    public sealed class Expression
    {
        #region Fields

        private IExpression _compiledExpression;
        private readonly ExpressiveOptions _options;
        private readonly string _originalExpression;
        private readonly ExpressionParser _parser;
        private string[] _variables;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of the Variable names that are contained within this Expression.
        /// </summary>
        public string[] ReferencedVariables
        {
            get
            {
                this.CompileExpression();

                return _variables;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Expression"/> class with no options.
        /// </summary>
        /// <param name="expression">The expression to be evaluated.</param>
        public Expression(string expression) : this (expression, ExpressiveOptions.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expression"/> class with the specified options.
        /// </summary>
        /// <param name="expression">The expression to be evaluated.</param>
        /// <param name="options">The options to use when evaluating.</param>
        public Expression(string expression, ExpressiveOptions options)
        {
            _originalExpression = expression;
            _options = options;

            _parser = new ExpressionParser(_options);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Evaluates the expression and returns the result.
        /// </summary>
        /// <exception cref="Exceptions.MissingTokenException">Thrown when the evaluator detects that a token has not been supplied.</exception>
        /// <exception cref="Exceptions.ParameterCountMismatchException">Thrown when the evaluator detects a function does not have the expected number of parameters supplied.</exception>
        /// <exception cref="Exceptions.UnrecognisedTokenException">Thrown when the evaluator is unable to process a token in the expression.</exception>
        /// <returns>The result of the expression.</returns>
        public object Evaluate()
        {
            return Evaluate(null);
        }

        /// <summary>
        /// Evaluates the expression using the supplied variables and returns the result.
        /// </summary>
        /// <exception cref="Exceptions.MissingTokenException">Thrown when the evaluator detects that a token has not been supplied.</exception>
        /// <exception cref="Exceptions.ParameterCountMismatchException">Thrown when the evaluator detects a function does not have the expected number of parameters supplied.</exception>
        /// <exception cref="Exceptions.UnrecognisedTokenException">Thrown when the evaluator is unable to process a token in the expression.</exception>
        /// <param name="variables">The variables to be used in the evaluation.</param>
        /// <returns>The result of the expression.</returns>
        public object Evaluate(IDictionary<string, object> variables)
        {
            object result = null;
            
            this.CompileExpression();

            if (variables != null &&
                _options.HasFlag(ExpressiveOptions.IgnoreCase))
            {
                variables = new Dictionary<string, object>(variables, StringComparer.OrdinalIgnoreCase);
            }

            result = _compiledExpression.Evaluate(variables);

            return result;
        }

        /// <summary>
        /// Evaluates the expression asynchronously and returns the result via the callback.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Thrown if the callback is not supplied.</exception>
        /// <param name="callback">Provides the result once the evaluation has completed.</param>
        public void EvaluateAsync(Action<object> callback)
        {
            EvaluateAsync(callback, null);
        }

        /// <summary>
        /// Evaluates the expression using the supplied variables asynchronously and returns the result via the callback.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Thrown if the callback is not supplied.</exception>
        /// <param name="callback">Provides the result once the evaluation has completed.</param>
        /// <param name="variables">The variables to be used in the evaluation.</param>
        public void EvaluateAsync(Action<object> callback, IDictionary<string, object> variables)
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
        }

        /// <summary>
        /// Registers a custom function for use in evaluating an expression.
        /// </summary>
        /// <param name="functionName">The name of the function (NOTE this is also the tag that will be used to extract the function from an expression).</param>
        /// <param name="function">The method of evaluating the function.</param>
        public void RegisterFunction(string functionName, Func<IExpression[], IDictionary<string, object>, object> function)
        {
            _parser.RegisterFunction(functionName, function);
        }

        /// <summary>
        /// Registers a custom function inheriting from <see cref="IFunction"/> for use in evaluating an expression.
        /// </summary>
        /// <param name="function">The <see cref="IFunction"/> implementation.</param>
        public void RegisterFunction(IFunction function)
        {
            _parser.RegisterFunction(function);
        }

        #endregion

        #region Private Methods

        private void CompileExpression()
        {
            // Cache the expression to save us having to recompile.
            if (_compiledExpression == null ||
                _options.HasFlag(ExpressiveOptions.NoCache))
            {
                var variables = new List<string>();

                _compiledExpression = _parser.CompileExpression(_originalExpression, variables);

                _variables = variables.ToArray();
            }
        }

        #endregion
    }
}
