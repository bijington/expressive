package com.bijington.expressive.expressions;

import com.bijington.expressive.helpers.Comparison;
import com.bijington.expressive.helpers.Convert;
import com.bijington.expressive.helpers.Numbers;

import java.util.Map;

/**
 * Created by shaun on 06/07/2016.
 */
public class BinaryExpression implements IExpression {
    private final BinaryExpressionType _expressionType;
    private final IExpression _leftHandSide;
    private final IExpression _rightHandSide;

    public BinaryExpression(BinaryExpressionType type, IExpression lhs, IExpression rhs)
    {
        _expressionType = type;
        _leftHandSide = lhs;
        _rightHandSide = rhs;
    }

    @Override
    public Object evaluate(Map<String, Object> variables) {
        if (_leftHandSide == null || _rightHandSide == null)
        {
            return null;
        }

        // We will evaluate the left hand side but hold off on the right hand side as it may not be necessary
        Object lhsResult = _leftHandSide.evaluate(variables);

        switch (_expressionType)
        {
            case Unknown:
                break;
            case And:
                return Convert.toBoolean(lhsResult) && Convert.toBoolean(_rightHandSide.evaluate(variables));
            case Or:
                return Convert.toBoolean(lhsResult) || Convert.toBoolean(_rightHandSide.evaluate(variables));
            case NotEqual: {
                Object rhsResult = null;

                // Use the type of the left operand to make the comparison
                if (lhsResult == null) {
                    rhsResult = _rightHandSide.evaluate(variables);
                    if (rhsResult != null) {
                        // 2 nulls make a match!
                        return true;
                    }

                    return false;
                }
                else {
                    rhsResult = _rightHandSide.evaluate(variables);

                    // If we got here then the lhsResult is not null.
                    if (rhsResult == null) {
                        return true;
                    }
                }

                return !lhsResult.equals(rhsResult);
            }
            case LessThanOrEqual: {
                // Use the type of the left operand to make the comparison
                if (lhsResult == null) {
                    return null;
                }

                Object rhsResult = _rightHandSide.evaluate(variables);
                if (rhsResult == null) {
                    return null;
                }

                return Comparison.compareUsingMostPreciseType(lhsResult, rhsResult) <= 0;
            }
            case GreaterThanOrEqual: {
                // Use the type of the left operand to make the comparison
                if (lhsResult == null)
                {
                    return null;
                }

                Object rhsResult = _rightHandSide.evaluate(variables);
                if (rhsResult == null)
                {
                    return null;
                }

                return Comparison.compareUsingMostPreciseType(lhsResult, rhsResult) >= 0;
            }
            case LessThan: {
                // Use the type of the left operand to make the comparison
                if (lhsResult == null) {
                    return null;
                }

                Object rhsResult = _rightHandSide.evaluate(variables);
                if (rhsResult == null) {
                    return null;
                }

                return Comparison.compareUsingMostPreciseType(lhsResult, rhsResult) < 0;
            }
            case GreaterThan: {
                // Use the type of the left operand to make the comparison
                if (lhsResult == null) {
                    return null;
                }

                Object rhsResult = _rightHandSide.evaluate(variables);
                if (rhsResult == null) {
                    return null;
                }

                return Comparison.compareUsingMostPreciseType(lhsResult, rhsResult) > 0;
            }
            case Equal: {
                Object rhsResult = null;

                // Use the type of the left operand to make the comparison
                if (lhsResult == null)
                {
                    rhsResult = _rightHandSide.evaluate(variables);
                    if (rhsResult == null)
                    {
                        // 2 nulls make a match!
                        return true;
                    }

                    return false;
                }
                else
                {
                    rhsResult = _rightHandSide.evaluate(variables);

                    // If we got here then the lhsResult is not null.
                    if (rhsResult == null)
                    {
                        return false;
                    }
                }

                return lhsResult.equals(rhsResult);
                //return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) == 0;
            }
            case Subtract:
                return Numbers.subtract(lhsResult, _rightHandSide.evaluate(variables));
            case Add:
                // TODO what if the right hand side is a string?
                if (lhsResult.getClass().equals(String.class)) {
                    return ((String)lhsResult) + _rightHandSide.evaluate(variables);
                }

                return Numbers.add(lhsResult, _rightHandSide.evaluate(variables));
            case Modulus:
                return Numbers.modulus(lhsResult, _rightHandSide.evaluate(variables));
            case Divide:
                return Numbers.divide(lhsResult, _rightHandSide.evaluate(variables));
            case Multiply:
                return Numbers.multiply(lhsResult, _rightHandSide.evaluate(variables));
            case BitwiseOr:
                return Convert.toInteger(lhsResult) | Convert.toInteger(_rightHandSide.evaluate(variables));
            case BitwiseAnd:
                return Convert.toInteger(lhsResult) & Convert.toInteger(_rightHandSide.evaluate(variables));
            case BitwiseXOr:
                return Convert.toInteger(lhsResult) ^ Convert.toInteger(_rightHandSide.evaluate(variables));
            case LeftShift:
                return Convert.toInteger(lhsResult) << Convert.toInteger(_rightHandSide.evaluate(variables));
            case RightShift:
                return Convert.toInteger(lhsResult) >> Convert.toInteger(_rightHandSide.evaluate(variables));
            case NullCoalescing:
                return (lhsResult != null) ? lhsResult : _rightHandSide.evaluate(variables);
            default:
                break;
        }

        return null;
    }
}
