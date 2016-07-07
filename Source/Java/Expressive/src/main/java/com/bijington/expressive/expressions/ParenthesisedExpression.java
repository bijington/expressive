package com.bijington.expressive.expressions;

import java.util.Map;

/**
 * Created by shaun on 05/07/2016.
 */
public class ParenthesisedExpression implements IExpression {
    private final IExpression _innerExpression;

    public ParenthesisedExpression(IExpression innerExpression) {
        _innerExpression = innerExpression;
    }

    @Override
    public Object evaluate(Map<String, Object> variables) {
        if (_innerExpression == null) {
            // TODO should this be an exception?
            return null;
        }

        return _innerExpression.evaluate(variables);
    }
}
