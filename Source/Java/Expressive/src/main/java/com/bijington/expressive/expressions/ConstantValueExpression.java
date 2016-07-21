package com.bijington.expressive.expressions;

import java.util.Map;

/**
 * Created by shaun on 05/07/2016.
 */
public class ConstantValueExpression implements IExpression {
    //private final ConstantValueExpressionType _expressionType;
    private final Object _value;

    public ConstantValueExpression(Object value) {
        //_expressionType = type;
        _value = value;
    }

    @Override
    public Object evaluate(Map<String, Object> variables) { return _value; }
}
