package com.bijington.expressive.expressions;

import java.util.Map;

/**
 * Created by shaun on 27/06/2016.
 */
/// <summary>
/// Interface definition for an expression that can be evaluated.
/// </summary>
/// <remarks>
/// These are exposed as parameters inside a function.
/// </remarks>
public interface IExpression
{
    /// <summary>
    /// Forces this <see cref="IExpression"/> to be evaluated.
    /// </summary>
    /// <param name="variables">The list of variables for use in evaluating.</param>
    /// <returns>The result of the <see cref="IExpression"/>.</returns>
    Object evaluate(Map<String, Object> variables);
}
