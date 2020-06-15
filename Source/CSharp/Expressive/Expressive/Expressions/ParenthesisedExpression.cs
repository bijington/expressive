using Expressive.Exceptions;
using System.Collections.Generic;

namespace Expressive.Expressions
{
    internal class ParenthesisedExpression : IExpression
    {
        private readonly IExpression innerExpression;

        internal ParenthesisedExpression(IExpression innerExpression)
        {
            this.innerExpression = innerExpression;
        }

        #region IExpression Members

        public object Evaluate(IDictionary<string, object> variables)
        {
            if (this.innerExpression is null)
            {
                throw new MissingParticipantException("Missing contents inside ().");
            }

            return this.innerExpression.Evaluate(variables);
        }

        #endregion
    }
}
