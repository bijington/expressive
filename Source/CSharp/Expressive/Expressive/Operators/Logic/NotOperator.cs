using Expressive.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Operators.Logic
{
    internal class NotOperator : IOperator
    {
        #region IOperator Members

        public string[] Tags { get { return new[] { "!", "not" }; } }

        public IExpression BuildExpression(string previousToken, IExpression[] expressions)
        {
            return new UnaryExpression(UnaryExpressionType.Not, expressions[0] ?? expressions[1]);
        }

        public bool CanGetCaptiveTokens(string previousToken, string token, Queue<string> remainingTokens)
        {
            var remainingTokensCopy = new Queue<string>(remainingTokens.ToArray());

            return this.GetCaptiveTokens(previousToken, token, remainingTokensCopy).Any();
        }

        public string[] GetCaptiveTokens(string previousToken, string token, Queue<string> remainingTokens)
        {
            string[] result = null;
            
            if (remainingTokens.FirstOrDefault() != null)
            {
                result = new string[] { token, remainingTokens.Dequeue() };
            }
            else
            {
                result = new[] { token };
            }

            return result;
        }

        public string[] GetInnerCaptiveTokens(string[] allCaptiveTokens)
        {
            return allCaptiveTokens.Skip(1).ToArray();
        }

        public OperatorPrecedence GetPrecedence(string previousToken)
        {
            return OperatorPrecedence.Not;
        }

        #endregion
    }
}
