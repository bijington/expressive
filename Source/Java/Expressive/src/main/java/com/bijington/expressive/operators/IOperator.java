package com.bijington.expressive.operators;

import com.bijington.expressive.expressions.IExpression;

import java.util.List;

/**
 * Created by shaun on 27/06/2016.
 */
    /// <summary>
    /// Definition for all Operators (i.e. +, -, etc.) that are available in Expressive.
    /// </summary>
public interface IOperator {
    /// <summary>
    /// Gets the list of tags that can be used to identify this IOperator.
    /// </summary>
    String[] getTags();

    IExpression buildExpression(String previousToken, IExpression[] expressions);

    Boolean canGetCaptiveTokens(String previousToken, String token, List<String> remainingTokens);
    String[] getCaptiveTokens(String previousToken, String token, List<String> remainingTokens);
    String[] getInnerCaptiveTokens(String[] allCaptiveTokens);
    int getPrecedence(String previousToken);
}
