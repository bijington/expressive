package com.bijington.expressive.functions.string;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Created by shaun on 20/12/2016.
 */
public class RegexFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Regex";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 2, 2);

        Object value = parameters[0].evaluate(this.getVariables());
        Object patternValue = parameters[1].evaluate(this.getVariables());

        if (value == null || patternValue == null) return null;

        //return String.class.cast(value).matches(String.class.cast(patternValue));

        Pattern pattern = Pattern.compile(String.class.cast(patternValue), Pattern.CASE_INSENSITIVE);
        Matcher matcher = pattern.matcher(String.class.cast(value));

        return matcher.matches();
    }
}
