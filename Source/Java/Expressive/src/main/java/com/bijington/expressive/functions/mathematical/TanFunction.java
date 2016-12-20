package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;

/**
 * Created by shaun on 20/12/2016.
 */
public class TanFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Tan";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 1, 1);

        Object value = parameters[0].evaluate(this.getVariables());

        if (value == null) return null;

        if (value.getClass().equals(Integer.class)) {
            return Math.tan(Integer.class.cast(value));
        }
        else if (value.getClass().equals(Long.class)) {
            return Math.tan(Long.class.cast(value));
        }
        else if (value.getClass().equals(Double.class)) {
            return Math.tan(Double.class.cast(value));
        }
        else if (value.getClass().equals(Float.class)) {
            return Math.tan(Float.class.cast(value));
        }

        return null;
    }
}
