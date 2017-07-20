package com.bijington.expressive.functions;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.exceptions.ParameterCountMismatchException;
import com.bijington.expressive.expressions.IExpression;

import java.util.Map;

/**
 * Created by shaun on 12/07/2016.
 */
public abstract class FunctionBase implements IFunction {
    protected Map<String, Object> _variables;

    @Override
    public Map<String, Object> getVariables() {
        return _variables;
    }

    @Override
    public void setVariables(Map<String, Object> variables) {
        _variables = variables;
    }

    @Override
    public abstract String getName();

    @Override
    public abstract Object evaluate(IExpression[] parameters) throws ExpressiveException;

    /// <summary>
    /// Validates whether the expected number of parameters are present.
    /// </summary>
    /// <param name="parameters">The parameters.</param>
    /// <param name="expectedCount">The expected number of parameters, use -1 for an unknown number.</param>
    /// <param name="minimumCount">The minimum number of parameters.</param>
    /// <returns>True if the correct number are present, false otherwise.</returns>
    protected Boolean validateParameterCount(IExpression[] parameters, int expectedCount, int minimumCount) throws ExpressiveException {
        if (expectedCount != -1 && (parameters == null || parameters.length != expectedCount)) {
            throw new ExpressiveException(new ParameterCountMismatchException(this.getName() + "() takes only " + expectedCount + " argument(s)"));
        }

        if (minimumCount > 0 && (parameters == null || parameters.length < minimumCount)) {
            throw new ExpressiveException(new ParameterCountMismatchException(this.getName() + "() expects at least " + minimumCount + " argument(s)"));
        }

        return true;
    }
}
