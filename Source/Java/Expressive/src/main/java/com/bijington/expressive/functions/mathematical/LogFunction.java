package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;

/**
 * Created by shaun on 18/07/2016.
 */
public class LogFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Log";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 2, 2);

        Object value = parameters[0].evaluate(this.getVariables());
        Object base = parameters[1].evaluate(this.getVariables());

        if (value == null) return null;

        return Math.log(Convert.toDouble(value)) / Math.log(Convert.toDouble(base));
    }
}