package com.bijington.expressive.expressions;

import com.bijington.expressive.Expression;
import com.bijington.expressive.exceptions.ExpressiveException;

import java.util.Map;

/**
 * Created by shaun on 05/07/2016.
 */
public class VariableExpression implements IExpression {
    private final String _variableName;

    public VariableExpression(String variableName) {
        _variableName = variableName;
    }

    @Override
    public Object evaluate(Map<String, Object> variables) throws ExpressiveException {
        if (variables == null ||
            !variables.containsKey(_variableName))
        {
            throw new ExpressiveException("The variable '" + _variableName + "' has not been supplied.");
        }

        Object value = variables.get(_variableName);
        // Check to see if we have to referred to another expression.
        if (value != null && value.getClass().equals(Expression.class))
        {
            Expression expression = (Expression)value;

            return expression.evaluate(variables);
        }

        return value;
    }
}
