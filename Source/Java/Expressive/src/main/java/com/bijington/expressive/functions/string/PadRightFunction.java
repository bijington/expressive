package com.bijington.expressive.functions.string;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;
import com.bijington.expressive.helpers.Strings;

/**
 * Created by shaun on 18/07/2016.
 */
public class PadRightFunction extends FunctionBase {
    @Override
    public String getName() {
        return "PadRight";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 3, 3);

        Object value = parameters[0].evaluate(this.getVariables());
        Object length = parameters[1].evaluate(this.getVariables());
        Object character = parameters[2].evaluate(this.getVariables());

        if (value == null || length == null) return null;

        String stringValue;
        if (value.getClass().equals(String.class)) {
            stringValue = String.class.cast(value);
        }
        else {
            stringValue = value.toString();
        }

        int totalLength = Convert.toInteger(length);

        Character actualCharacter = ' ';
        if (character != null) {
            if (character.getClass().equals(Character.class)) {
                actualCharacter = (Character) character;
            }
            else if (character.getClass().equals(String.class)) {
                actualCharacter = String.class.cast(character).charAt(0);
            }
        }

        return Strings.padRight(stringValue, totalLength, actualCharacter);
    }
}