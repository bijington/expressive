using Expressive.Expressions;
using System.Collections.Generic;

namespace Expressive.Operators
{
    /// <summary>
    /// Definition for all Operators (i.e. +, -, etc.) that are available in Expressive.
    /// </summary>
    internal interface IOperator
    {
        /// <summary>
        /// Gets the list of tags that can be used to identify this IOperator.
        /// </summary>
        string[] Tags { get; }

        IExpression BuildExpression(string previousToken, IExpression[] expressions);

        bool CanGetCaptiveTokens(string previousToken, string token, Queue<string> remainingTokens);
        string[] GetCaptiveTokens(string previousToken, string token, Queue<string> remainingTokens);
        string[] GetInnerCaptiveTokens(string[] allCaptiveTokens);
        OperatorPrecedence GetPrecedence(string previousToken);
    }
}
