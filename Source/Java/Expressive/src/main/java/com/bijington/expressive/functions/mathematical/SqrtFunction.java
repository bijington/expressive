package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;

/**
 * Created by shaun on 20/12/2016.
 */
public class SqrtFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Sqrt";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 1, 1);

        Object value = parameters[0].evaluate(this.getVariables());

        if (value == null) return null;

        return Math.sqrt(Convert.toDouble(value));
    }
}
