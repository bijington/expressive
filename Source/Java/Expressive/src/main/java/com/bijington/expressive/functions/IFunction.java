package com.bijington.expressive.functions;

import com.bijington.expressive.expressions.IExpression;

import java.util.Map;

/**
 * Created by shaun on 27/06/2016.
 */
/// <summary>
/// Interface definition for a Function that can be evaluated.
/// </summary>
public interface IFunction
{
    /// <summary>
    /// Gets or sets the Variables and their values to be used in evaluating an <see cref="Expression"/>.
    /// </summary>
    Map<String, Object> getVariables();

    void setVariables(Map<String, Object> variables);

    /// <summary>
    /// Gets the name of the Function.
    /// </summary>
    String getName();

    /// <summary>
    /// Forces the Function to evaluate itself using the supplied parameters.
    /// </summary>
    /// <param name="parameters">The list of parameters inside the Function.</param>
    /// <returns>The result of the Function.</returns>
    Object evaluate(IExpression[] parameters);
}
