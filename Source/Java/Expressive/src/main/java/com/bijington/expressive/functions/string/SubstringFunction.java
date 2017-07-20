package com.bijington.expressive.functions.string;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;
import com.bijington.expressive.helpers.Strings;

/**
 * Created by shaun on 20/12/2016.
 */
public class SubstringFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Substring";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 3, 3);

        Object value = parameters[0].evaluate(this.getVariables());
        Object startIndexValue = parameters[1].evaluate(this.getVariables());
        Object length = parameters[2].evaluate(this.getVariables());

        if (value == null || startIndexValue == null || length == null) return null;

        String stringValue;
        if (value.getClass().equals(String.class)) {
            stringValue = String.class.cast(value);
        }
        else {
            stringValue = value.toString();
        }

        int startIndex = Convert.toInteger(startIndexValue);
        int totalLength = Convert.toInteger(length);

        return stringValue.substring(startIndex, startIndex + totalLength);
    }
}
