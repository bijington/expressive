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

        IExpression BuildExpression(Token previousToken, IExpression[] expressions, ExpressiveOptions options);

        bool CanGetCaptiveTokens(Token previousToken, Token token, Queue<Token> remainingTokens);
        Token[] GetCaptiveTokens(Token previousToken, Token token, Queue<Token> remainingTokens);
        Token[] GetInnerCaptiveTokens(Token[] allCaptiveTokens);
        OperatorPrecedence GetPrecedence(Token previousToken);
    }
}
