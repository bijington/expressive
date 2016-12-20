package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;

/**
 * Created by shaun on 18/07/2016.
 */
public class IEEERemainderFunction extends FunctionBase {
    @Override
    public String getName() {
        return "IEEERemainder";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 2, 2);

        return Math.IEEEremainder(Convert.toDouble(parameters[0].evaluate(this.getVariables())), Convert.toDouble(parameters[1].evaluate(this.getVariables())));
    }
}