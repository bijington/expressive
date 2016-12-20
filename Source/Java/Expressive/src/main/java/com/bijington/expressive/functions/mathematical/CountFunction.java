package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;

import java.lang.reflect.Array;

/**
 * Created by shaun on 18/07/2016.
 */
public class CountFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Count";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, -1, 1);

        int count = 0;

        for (int parameterIndex = 0; parameterIndex < parameters.length; parameterIndex++) {
            IExpression value = parameters[parameterIndex];

            int increment = 1;
            Object evaluatedValue = value.evaluate(this.getVariables());

            if (evaluatedValue != null &&
                    evaluatedValue.getClass().isArray()) {
                increment = Array.getLength(evaluatedValue);
            }

            count += increment;
        }

        return count;
    }
}
