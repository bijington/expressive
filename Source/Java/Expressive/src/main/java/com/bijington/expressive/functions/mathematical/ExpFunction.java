package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;

/**
 * Created by shaun on 18/07/2016.
 */
public class ExpFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Exp";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 1, 1);

        Object value = parameters[0].evaluate(this.getVariables());

        if (value == null) return null;

        if (value.getClass().equals(Integer.class)) {
            return Math.exp(Integer.class.cast(value));
        }
        else if (value.getClass().equals(Long.class)) {
            return Math.exp(Long.class.cast(value));
        }
        else if (value.getClass().equals(Double.class)) {
            return Math.exp(Double.class.cast(value));
        }
        else if (value.getClass().equals(Float.class)) {
            return Math.exp(Float.class.cast(value));
        }

        return null;
    }
}
