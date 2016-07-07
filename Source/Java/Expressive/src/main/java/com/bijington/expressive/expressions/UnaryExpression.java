package com.bijington.expressive.expressions;

import java.util.Map;

/**
 * Created by shaun on 06/07/2016.
 */
public class UnaryExpression implements IExpression {
    private final IExpression _expression;
    private final UnaryExpressionType _expressionType;

    public UnaryExpression(UnaryExpressionType type, IExpression expression)
    {
        _expressionType = type;
        _expression = expression;
    }

    @Override
    public Object evaluate(Map<String, Object> variables) {
//        switch (_expressionType)
//        {
//            case UnaryExpressionType.Minus:
//                return Numbers.Subtract(0, _expression.evaluate(variables));
//            case UnaryExpressionType.Not:
//                Object value = _expression.evaluate(variables);
//
//                if (value != null)
//                {
//                    var valueType = Type.GetTypeCode(value.GetType());
//
//                    if (value is bool)
//                    {
//                        return !(bool)value;
//                    }
//
//                    return Convert.ToBoolean(value);
//                }
//                break;
//            case UnaryExpressionType.Plus:
//                return Numbers.Add(0, _expression.evaluate(variables));
//        }

        return null;
    }
}
