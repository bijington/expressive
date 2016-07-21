package com.bijington.expressive.expressions;

import com.bijington.expressive.Expression;

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
    public Object evaluate(Map<String, Object> variables) {
        if (variables == null ||
            !variables.containsKey(_variableName))
        {
            //throw new ArgumentException("The variable '" + _variableName + "' has not been supplied.");
        }

        // Check to see if we have to referred to another expression.
//        Expression expression = variables[_variableName] as Expression;
//        if (expression != null)
//        {
//            return expression.evaluate(variables);
//        }

        return variables.get(_variableName);
    }
}
