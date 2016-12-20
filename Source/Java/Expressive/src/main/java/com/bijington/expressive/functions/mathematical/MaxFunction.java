package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Numbers;

import java.lang.reflect.Array;

/**
 * Created by shaun on 13/12/2016.
 */
public class MaxFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Max";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, -1, 1);

        Object result = null;

        for (int parameterIndex = 0; parameterIndex < parameters.length; parameterIndex++) {
            IExpression value = parameters[parameterIndex];

            Object evaluatedValue = value.evaluate(this.getVariables());

            if (evaluatedValue != null &&
                    evaluatedValue.getClass().isArray()) {

                for (int j = 0; j < Array.getLength(evaluatedValue); j++) {
                    Object arrayValue = Array.get(evaluatedValue, j);

                    result = Numbers.max(arrayValue, result);
                }
            }
            else {
                result = Numbers.max(evaluatedValue, result);
            }
        }

        return result;
    }
}
