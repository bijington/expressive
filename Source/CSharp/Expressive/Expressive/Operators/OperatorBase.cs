using Expressive.Expressions;
using System.Collections.Generic;

namespace Expressive.Operators
{
    internal abstract class OperatorBase : IOperator
    {
        #region IOperator Members

        public abstract string[] Tags { get; }

        public abstract IExpression BuildExpression(string previousToken, IExpression[] expressions);

        public virtual bool CanGetCaptiveTokens(string previousToken, string token, Queue<string> remainingTokens)
        {
            return true;
        }

        public virtual string[] GetCaptiveTokens(string previousToken, string token, Queue<string> remainingTokens)
        {
            return new[] { token };
        }

        public virtual string[] GetInnerCaptiveTokens(string[] allCaptiveTokens)
        {
            return new string[0];
        }

        public abstract OperatorPrecedence GetPrecedence(string previousToken);

        #endregion
    }
}
