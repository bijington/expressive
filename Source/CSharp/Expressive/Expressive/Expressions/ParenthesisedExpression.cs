using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Expressions
{
    internal class ParenthesisedExpression : IExpression
    {
        private readonly IExpression _innerExpression;

        internal ParenthesisedExpression(IExpression innerExpression)
        {
            _innerExpression = innerExpression;
        }

        #region IExpression Members

        public object Evaluate(IDictionary<string, object> arguments)
        {
            if (_innerExpression == null)
            {
                // TODO should this be an exception?
                return null;
            }

            return _innerExpression.Evaluate(arguments);
        }

        #endregion
    }
}
