package com.bijington.expressive.functions.logical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Comparison;

/**
 * Created by shaun on 13/07/2016.
 */
public class InFunction extends FunctionBase {
    @Override
    public String getName() {
        return "In";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, -1, 2);

        Boolean found = false;
        Object parameter = parameters[0].evaluate(this.getVariables());

        // Goes through any values, and stop whe one is found
        for (int parameterIndex = 1; parameterIndex < parameters.length; parameterIndex++) {
            if (Comparison.compareUsingMostPreciseType(parameter, parameters[parameterIndex].evaluate(this.getVariables())) == 0) {
                found = true;
                break;
            }
        }

        return found;
    }
}
