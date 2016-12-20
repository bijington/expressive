package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Numbers;

import java.lang.reflect.Array;

/**
 * Created by shaun on 20/12/2016.
 */
public class SumFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Sum";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, -1, 1);

        Object result = 0;

        for (int parameterIndex = 0; parameterIndex < parameters.length; parameterIndex++) {
            IExpression value = parameters[parameterIndex];

            Object evaluatedValue = value.evaluate(this.getVariables());

            if (evaluatedValue != null &&
                    evaluatedValue.getClass().isArray()) {

                for (int j = 0; j < Array.getLength(evaluatedValue); j++) {
                    Object arrayValue = Array.get(evaluatedValue, j);

                    // When summing we don't want to bail out early with a null value.
                    result = Numbers.add(arrayValue != null ? arrayValue : 0.0, result != null ? result : 0.0);
                }
            }
            else {
                // When summing we don't want to bail out early with a null value.
                result = Numbers.add(evaluatedValue != null ? evaluatedValue : 0.0, result != null ? result : 0.0);
            }
        }

        return result;
    }
}
