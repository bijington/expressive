package com.bijington.expressive.functions.mathematical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;

import java.math.BigDecimal;

/**
 * Created by shaun on 20/12/2016.
 */
public class RoundFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Round";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 2, 2);

        Object value = parameters[0].evaluate(this.getVariables());
        Object decimalsValue = parameters[1].evaluate(this.getVariables());

        if (value == null || decimalsValue == null) return null;

        BigDecimal bd = new BigDecimal(Convert.toDouble(value));
        bd = bd.setScale(Convert.toInteger(decimalsValue), BigDecimal.ROUND_HALF_UP);
        return bd.doubleValue();
    }
}
