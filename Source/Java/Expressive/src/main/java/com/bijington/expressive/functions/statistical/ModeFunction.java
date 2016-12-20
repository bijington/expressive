package com.bijington.expressive.functions.statistical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;

import java.lang.reflect.Array;
import java.math.BigDecimal;
import java.util.ArrayList;

/**
 * Created by shaun on 12/12/2016.
 */
public class ModeFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Mode";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, -1, 1);

        ArrayList<Double> numbers = new ArrayList<>();

        for (int parameterIndex = 0; parameterIndex < parameters.length; parameterIndex++) {
            IExpression value = parameters[parameterIndex];

            Object evaluatedValue = value.evaluate(this.getVariables());

            if (evaluatedValue != null &&
                    evaluatedValue.getClass().isArray()) {

                for (int j = 0; j < Array.getLength(evaluatedValue); j++) {
                    Object arrayValue = Array.get(evaluatedValue, j);

                    numbers.add(Convert.toDouble(arrayValue));
                }
            }
            else {
                numbers.add(Convert.toDouble(evaluatedValue));
            }
        }

        return mode(numbers.toArray(new Double[0]));
    }

    public static Double mode(Double a[]) {
        double maxValue = 0.0;
        int maxCount = 0;

        for (int i = 0; i < a.length; ++i) {
            int count = 0;
            for (int j = 0; j < a.length; ++j) {
                if (a[j].compareTo(a[i]) == 0) {
                    count++;
                }
            }

            if (count > maxCount) {
                maxCount = count;
                maxValue = a[i];
            }
        }

        return maxValue;
    }
}
