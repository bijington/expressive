package com.bijington.expressive.functions.date;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Dates;

import java.util.Calendar;

/**
 * Created by shaun on 12/12/2016.
 */
public class AddComponentFunction extends FunctionBase {
    private int componentType;
    private String name;

    @Override
    public String getName() {
        return this.name;
    }

    public AddComponentFunction(String name, int componentType) {
        this.name = name;
        this.componentType = componentType;
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, 2, 2);

        return Dates.add(parameters[0].evaluate(this.getVariables()), parameters[1].evaluate(this.getVariables()), this.componentType);
    }
}
