using Expressive.Expressions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Expressive
{
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
        public string[] Variables
        {
            get
            {
                this.CompileExpression();

                return _variables;
            }
        }

        #endregion

        #region Constructors

        public Expression(string expression) : this (expression, ExpressiveOptions.None)
        {
        }

        public Expression(string expression, ExpressiveOptions options)
        {
            _originalExpression = expression;
            _options = options;

            _parser = new ExpressionParser(_options);
        }

        #endregion

        #region Public Methods

        public object Evaluate()
        {
            return Evaluate(null);
        }

        public object Evaluate(IDictionary<string, object> parameters)
        {
            object result = null;
            
            this.CompileExpression();

            if (parameters != null &&
                _options.HasFlag(ExpressiveOptions.IgnoreCase))
            {
                parameters = new Dictionary<string, object>(parameters, StringComparer.OrdinalIgnoreCase);
            }

            result = _compiledExpression.Evaluate(parameters);

            return result;
        }

        public void EvaluateAsync(Action<object> callback)
        {
            EvaluateAsync(callback, null);
        }

        public void EvaluateAsync(Action<object> callback, IDictionary<string, object> parameters)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            ThreadPool.QueueUserWorkItem((o) =>
            {
                var result = this.Evaluate(parameters);

                if (callback != null)
                {
                    callback(result);
                }
            });
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
