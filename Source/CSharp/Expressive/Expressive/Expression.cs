using Expressive.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Expressive
{
    public sealed class Expression
    {
        #region Fields

        private IExpression _compiledExpression;
        private string _originalExpression;
        private ExpressionParser _parser;

        #endregion

        #region Constructors

        public Expression(string expression)
        {
            _originalExpression = expression;
        }

        #endregion

        #region Public Methods

        public object Evaluate(IDictionary<string, object> parameters = null)
        {
            object result = null;

            if (_parser == null)
            {
                _parser = new ExpressionParser();
            }

            // Cache the expression to save us having to recompile.
            if (_compiledExpression == null)
            {
                _compiledExpression = _parser.CompileExpression(_originalExpression);
            }

            IDictionary<string, object> caseInsensitiveParameters = null;
            if (parameters != null)
            {
                caseInsensitiveParameters = new Dictionary<string, object>(parameters, StringComparer.OrdinalIgnoreCase);
            }

            result = _compiledExpression.Evaluate(caseInsensitiveParameters);

            return result;
        }

        public void EvaluateASync(Action<object> callback, IDictionary<string, object> parameters = null)
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
    }
}
