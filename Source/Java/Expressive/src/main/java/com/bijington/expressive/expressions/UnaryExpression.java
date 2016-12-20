package com.bijington.expressive.expressions;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.helpers.Convert;
import com.bijington.expressive.helpers.Numbers;

import java.util.Map;

/**
 * Created by shaun on 06/07/2016.
 */
public class UnaryExpression implements IExpression {
    private final IExpression _expression;
    private final UnaryExpressionType _expressionType;

    public UnaryExpression(UnaryExpressionType type, IExpression expression) {
        _expressionType = type;
        _expression = expression;
    }

    @Override
    public Object evaluate(Map<String, Object> variables) throws ExpressiveException {
        switch (_expressionType) {
            case Minus:
                return Numbers.subtract(0, _expression.evaluate(variables));
            case Not:
                Object value = _expression.evaluate(variables);

                return !Convert.toBoolean(value);

//                if (value != null) {
//                    if (value.getClass().equals(Boolean.class)) {
//                        return Boolean.class.cast(value);
//                    }
////                    return Convert.ToBoolean(value);
//                }
                //break;
            case Plus:
                return Numbers.add(0, _expression.evaluate(variables));
        }

        return null;
    }
}
