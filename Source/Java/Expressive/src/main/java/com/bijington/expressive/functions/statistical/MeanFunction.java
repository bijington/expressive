package com.bijington.expressive.functions.statistical;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.functions.FunctionBase;
import com.bijington.expressive.helpers.Convert;
import com.bijington.expressive.helpers.Numbers;

import java.lang.reflect.Array;

/**
 * Created by shaun on 18/07/2016.
 */
public class MeanFunction extends FunctionBase {
    @Override
    public String getName() {
        return "Mean";
    }

    @Override
    public Object evaluate(IExpression[] parameters) throws ExpressiveException {
        this.validateParameterCount(parameters, -1, 1);

        int count = 0;
        Object result = 0;

        for (int parameterIndex = 0; parameterIndex < parameters.length; parameterIndex++) {
            IExpression value = parameters[parameterIndex];

            int increment = 1;
            Object evaluatedValue = value.evaluate(this.getVariables());

            if (evaluatedValue != null &&
                evaluatedValue.getClass().isArray()) {
                int enumerableCount = 0;
                Object enumerableSum = 0;

                for (int j = 0; j < Array.getLength(evaluatedValue); j++) {
                    Object arrayValue = Array.get(evaluatedValue, j);

                    enumerableCount++;
                    enumerableSum = Numbers.add(enumerableSum, arrayValue);
                }

                increment = enumerableCount;
                evaluatedValue = enumerableSum;
            }

            result = Numbers.add(result, evaluatedValue);
            count += increment;
        }

        return Convert.toDouble(result) / count;
    }
}
