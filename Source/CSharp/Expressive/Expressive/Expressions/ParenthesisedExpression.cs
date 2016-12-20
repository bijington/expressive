using Expressive.Exceptions;
using System.Collections.Generic;

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

        public object Evaluate(IDictionary<string, object> variables)
        {
            if (_innerExpression == null)
            {
                throw new MissingParticipantException("Missing contents inside ().");
            }

            return _innerExpression.Evaluate(variables);
        }

        #endregion
    }
}
