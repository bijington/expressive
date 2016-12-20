package com.bijington.expressive.functions.logical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;

/**
 * Created by shaun on 13/07/2016.
 */
public class IfFunction extends FunctionBase {
    @Override
    public String getName() {
        return "If";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 3, 3);

        Boolean condition = Convert.toBoolean(parameters[0].evaluate(this.getVariables()));

        return condition ? parameters[1].evaluate(this.getVariables()) : parameters[2].evaluate(this.getVariables());
    }
}
