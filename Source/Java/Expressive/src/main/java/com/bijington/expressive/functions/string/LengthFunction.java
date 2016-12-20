package com.bijington.expressive.functions.string;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;

/**
 * Created by shaun on 18/07/2016.
 */
public class LengthFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Length";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 1, 1);

        Object value = parameters[0].evaluate(this.getVariables());

        if (value == null) return null;

        if (value.getClass().equals(String.class)) {
            return String.class.cast(value).length();
        }
        else {
            return value.toString().length();
        }
    }
}
