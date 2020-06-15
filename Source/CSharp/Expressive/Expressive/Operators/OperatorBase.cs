using Expressive.Expressions;
using System.Collections.Generic;

namespace Expressive.Operators
{
    internal abstract class OperatorBase : IOperator
    {
        #region IOperator Members

        public abstract string[] Tags { get; }

        public abstract IExpression BuildExpression(Token previousToken, IExpression[] expressions, Context context);

        public virtual bool CanGetCaptiveTokens(Token previousToken, Token token, Queue<Token> remainingTokens)
        {
            return true;
        }

        public virtual Token[] GetCaptiveTokens(Token previousToken, Token token, Queue<Token> remainingTokens)
        {
            return new[] { token };
        }

        public virtual Token[] GetInnerCaptiveTokens(Token[] allCaptiveTokens)
        {
#pragma warning disable CA1825 // Avoid zero-length array allocations. - Array.Empty does not exist in net 4.5
            return new Token[0];
#pragma warning restore CA1825 // Avoid zero-length array allocations.
        }

        public abstract OperatorPrecedence GetPrecedence(Token previousToken);

        #endregion
    }
}
