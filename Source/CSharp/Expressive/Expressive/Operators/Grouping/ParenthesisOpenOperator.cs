using Expressive.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expressive.Operators.Grouping
{
    internal class ParenthesisOpenOperator : IOperator
    {
        #region IOperator Members

        public string[] Tags { get { return new[] { "(" }; } }

        public IExpression BuildExpression(string previousToken, IExpression[] expressions)
        {
            return new ParenthesisedExpression(expressions[0] ?? expressions[1]);
        }

        public bool CanGetCaptiveTokens(string previousToken, string token, Queue<string> remainingTokens)
        {
            var remainingTokensCopy = new Queue<string>(remainingTokens.ToArray());

            return this.GetCaptiveTokens(previousToken, token, remainingTokensCopy).Any();
        }

        public string[] GetCaptiveTokens(string previousToken, string token, Queue<string> remainingTokens)
        {
            IList<string> result = new List<string>();

            result.Add(token);

            var parenCount = 1;

            while (remainingTokens.Any())
            {
                var nextToken = remainingTokens.Dequeue();

                result.Add(nextToken);

                if (string.Equals(nextToken, "(", StringComparison.Ordinal))
                {
                    parenCount++;
                }
                else if (string.Equals(nextToken, ")", StringComparison.Ordinal))
                {
                    parenCount--;
                }

                if (parenCount <= 0)
                {
                    break;
                }
            }

            return result.ToArray();
        }

        public string[] GetInnerCaptiveTokens(string[] allCaptiveTokens)
        {
            return allCaptiveTokens.Skip(1).Take(allCaptiveTokens.Length - 2).ToArray();
        }

        public OperatorPrecedence GetPrecedence(string previousToken)
        {
            return OperatorPrecedence.ParenthesisOpen;
        }

        #endregion
    }
}
