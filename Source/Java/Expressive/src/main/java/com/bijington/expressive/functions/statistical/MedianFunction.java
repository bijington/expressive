package com.bijington.expressive.functions.statistical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;
import com.bijington.expressive.helpers.Numbers;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Collections;

/**
 * Created by shaun on 12/12/2016.
 */
public class MedianFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Median";
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

        Collections.sort(numbers);
        return median(numbers.toArray(new Double[0]));
    }

    private static Double median(Double[] m) {
        int middle = m.length/2;
        if (m.length%2 == 1) {
            return m[middle];
        } else {
            return (m[middle-1] + m[middle]) / 2.0;
        }
    }

    /*private decimal Median(decimal[] xs)
    {
        var ys = xs.OrderBy(x => x).ToList();
        double mid = (ys.Count - 1) / 2.0;
        return (ys[(int)(mid)] + ys[(int)(mid + 0.5)]) / 2;
    }*/
}
