package com.bijington.expressive.expressions;

import com.bijington.expressive.functions.IFunction;

import java.util.Map;

/**
 * Created by shaun on 30/06/2016.
 */
public class FunctionExpression implements IExpression {
    private final IFunction _function;
    private final String _name;
    private final IExpression[] _parameters;

    public FunctionExpression(String name, IFunction function, IExpression[] parameters)
    {
        _name = name;
        _function = function;
        _parameters = parameters;
    }

    @Override
    public Object evaluate(Map<String, Object> variables) {
        return _function.evaluate(_parameters);
    }
}
