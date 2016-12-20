package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;

/**
 * Created by shaun on 20/12/2016.
 */
public class PowFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Pow";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 2, 2);

        Object baseValue = parameters[0].evaluate(this.getVariables());
        Object exponentValue = parameters[1].evaluate(this.getVariables());

        if (baseValue == null || exponentValue == null) return null;

        return Math.pow(Convert.toDouble(baseValue), Convert.toDouble(exponentValue));
    }
}
