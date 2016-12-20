package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;
import com.bijington.expressive.helpers.Numbers;

/**
 * Created by shaun on 18/07/2016.
 */
public class TruncateFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Truncate";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 1, 1);

        Object value = parameters[0].evaluate(this.getVariables());

        if (value == null) return null;

        return Numbers.truncate(Convert.toDouble(value));
    }
}
