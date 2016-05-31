using Expressive.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expressive.Operators.Additive
{
    internal class PlusOperator : IOperator
    {
        #region IOperator Members

        public string[] Tags { get { return new[] { "+" }; } }

        public IExpression BuildExpression(string previousToken, IExpression[] expressions)
        {
            if (IsUnary(previousToken))
            {
                return new UnaryExpression(UnaryExpressionType.Plus, expressions[0] ?? expressions[1]);
            }

            return new BinaryExpression(BinaryExpressionType.Add, expressions[0], expressions[1]);
        }

        public bool CanGetCaptiveTokens(string previousToken, string token, Queue<string> remainingTokens)
        {
            var remainingTokensCopy = new Queue<string>(remainingTokens.ToArray());

            return this.GetCaptiveTokens(previousToken, token, remainingTokensCopy).Any();
        }

        public string[] GetCaptiveTokens(string previousToken, string token, Queue<string> remainingTokens)
        {
            string[] result = null;

            if (IsUnary(previousToken))
            {
                if (remainingTokens.FirstOrDefault() != null)
                {
                    result = new string[] { token, remainingTokens.Dequeue() };
                }
                else
                {
                    result = new[] { token };
                }
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
            if (IsUnary(previousToken))
            {
                return OperatorPrecedence.UnaryPlus;
            }
            return OperatorPrecedence.Add;
        }

        #endregion

        private bool IsUnary(string previousToken)
        {
            return string.IsNullOrEmpty(previousToken) ||
                string.Equals(previousToken, "(", StringComparison.Ordinal) ||
                previousToken.IsArithmeticOperator();
        }
    }
}
