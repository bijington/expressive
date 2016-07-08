package com.bijington.expressive.expressions;

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
//            case BinaryExpressionType.Unknown:
//                break;
//            case BinaryExpressionType.And:
//                return Convert.ToBoolean(lhsResult) && Convert.ToBoolean(_rightHandSide.Evaluate(variables));
//            case BinaryExpressionType.Or:
//                return Convert.ToBoolean(lhsResult) || Convert.ToBoolean(_rightHandSide.Evaluate(variables));
//            case BinaryExpressionType.NotEqual:
//            {
//                object rhsResult = null;
//
//                // Use the type of the left operand to make the comparison
//                if (lhsResult == null)
//                {
//                    rhsResult = _rightHandSide.Evaluate(variables);
//                    if (rhsResult != null)
//                    {
//                        // 2 nulls make a match!
//                        return true;
//                    }
//
//                    return false;
//                }
//                else
//                {
//                    rhsResult = _rightHandSide.Evaluate(variables);
//
//                    // If we got here then the lhsResult is not null.
//                    if (rhsResult == null)
//                    {
//                        return true;
//                    }
//                }
//
//                return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) != 0;
//            }
//            case BinaryExpressionType.LessThanOrEqual:
//            {
//                // Use the type of the left operand to make the comparison
//                if (lhsResult == null)
//                {
//                    return null;
//                }
//
//                var rhsResult = _rightHandSide.Evaluate(variables);
//                if (rhsResult == null)
//                {
//                    return null;
//                }
//
//                return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) <= 0;
//            }
//            case BinaryExpressionType.GreaterThanOrEqual:
//            {
//                // Use the type of the left operand to make the comparison
//                if (lhsResult == null)
//                {
//                    return null;
//                }
//
//                var rhsResult = _rightHandSide.Evaluate(variables);
//                if (rhsResult == null)
//                {
//                    return null;
//                }
//
//                return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) >= 0;
//            }
//            case BinaryExpressionType.LessThan:
//            {
//                // Use the type of the left operand to make the comparison
//                if (lhsResult == null)
//                {
//                    return null;
//                }
//
//                var rhsResult = _rightHandSide.Evaluate(variables);
//                if (rhsResult == null)
//                {
//                    return null;
//                }
//
//                return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) < 0;
//            }
//            case BinaryExpressionType.GreaterThan:
//            {
//                // Use the type of the left operand to make the comparison
//                if (lhsResult == null)
//                {
//                    return null;
//                }
//
//                var rhsResult = _rightHandSide.Evaluate(variables);
//                if (rhsResult == null)
//                {
//                    return null;
//                }
//
//                return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) > 0;
//            }
//            case BinaryExpressionType.Equal:
//            {
//                object rhsResult = null;
//
//                // Use the type of the left operand to make the comparison
//                if (lhsResult == null)
//                {
//                    rhsResult = _rightHandSide.Evaluate(variables);
//                    if (rhsResult == null)
//                    {
//                        // 2 nulls make a match!
//                        return true;
//                    }
//
//                    return false;
//                }
//                else
//                {
//                    rhsResult = _rightHandSide.Evaluate(variables);
//
//                    // If we got here then the lhsResult is not null.
//                    if (rhsResult == null)
//                    {
//                        return false;
//                    }
//                }
//
//                return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) == 0;
//            }
//            case BinaryExpressionType.Subtract:
//                return Numbers.Subtract(lhsResult, _rightHandSide.Evaluate(variables));
//            case BinaryExpressionType.Add:
//                if (lhsResult is string)
//            {
//                return ((string)lhsResult) + _rightHandSide.Evaluate(variables) as string;
//            }
//
//            return Numbers.Add(lhsResult, _rightHandSide.Evaluate(variables));
//            case BinaryExpressionType.Modulus:
//                return Numbers.Modulus(lhsResult, _rightHandSide.Evaluate(variables));
//            case BinaryExpressionType.Divide:
//            {
//                var rhsResult = _rightHandSide.Evaluate(variables);
//
//                return (lhsResult == null || rhsResult == null || IsReal(lhsResult) || IsReal(rhsResult))
//                        ? Numbers.Divide(lhsResult, rhsResult)
//                        : Numbers.Divide(Convert.ToDouble(lhsResult), rhsResult);
//            }
//            case BinaryExpressionType.Multiply:
//                return Numbers.Multiply(lhsResult, _rightHandSide.Evaluate(variables));
//            case BinaryExpressionType.BitwiseOr:
//                return Convert.ToUInt16(lhsResult) | Convert.ToUInt16(_rightHandSide.Evaluate(variables));
//            case BinaryExpressionType.BitwiseAnd:
//                return Convert.ToUInt16(lhsResult) & Convert.ToUInt16(_rightHandSide.Evaluate(variables));
//            case BinaryExpressionType.BitwiseXOr:
//                return Convert.ToUInt16(lhsResult) ^ Convert.ToUInt16(_rightHandSide.Evaluate(variables));
//            case BinaryExpressionType.LeftShift:
//                return Convert.ToUInt16(lhsResult) << Convert.ToUInt16(_rightHandSide.Evaluate(variables));
//            case BinaryExpressionType.RightShift:
//                return Convert.ToUInt16(lhsResult) >> Convert.ToUInt16(_rightHandSide.Evaluate(variables));
            case NullCoalescing:
                return (lhsResult != null) ? lhsResult : _rightHandSide.evaluate(variables);
            default:
                break;
        }

        return null;
    }
}
