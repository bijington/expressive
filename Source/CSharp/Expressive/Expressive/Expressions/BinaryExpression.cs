using Expressive.Exceptions;
using Expressive.Helpers;
using System;
using System.Collections.Generic;

namespace Expressive.Expressions
{
    internal class BinaryExpression : IExpression
    {
        private readonly BinaryExpressionType _expressionType;
        private readonly IExpression _leftHandSide;
        private readonly IExpression _rightHandSide;
        
        internal BinaryExpression(BinaryExpressionType type, IExpression lhs, IExpression rhs)
        {
            _expressionType = type;
            _leftHandSide = lhs;
            _rightHandSide = rhs;
        }

        #region IExpression Members

        public object Evaluate(IDictionary<string, object> variables)
        {
            if (_leftHandSide == null)
            {
                throw new MissingParticipantException("The left hand side of the operation is missing.");
            }
            else if (_rightHandSide == null)
            {
                throw new MissingParticipantException("The right hand side of the operation is missing.");
            }

            // We will evaluate the left hand side but hold off on the right hand side as it may not be necessary
            var lhsResult = _leftHandSide.Evaluate(variables);

            switch (_expressionType)
            {
                case BinaryExpressionType.Unknown:
                    break;
                case BinaryExpressionType.And:
                    return Convert.ToBoolean(lhsResult) && Convert.ToBoolean(_rightHandSide.Evaluate(variables));
                case BinaryExpressionType.Or:
                    return Convert.ToBoolean(lhsResult) || Convert.ToBoolean(_rightHandSide.Evaluate(variables));
                case BinaryExpressionType.NotEqual:
                    {
                        object rhsResult = null;

                        // Use the type of the left operand to make the comparison
                        if (lhsResult == null)
                        {
                            rhsResult = _rightHandSide.Evaluate(variables);
                            if (rhsResult != null)
                            {
                                // 2 nulls make a match!
                                return true;
                            }

                            return false;
                        }
                        else
                        {
                            rhsResult = _rightHandSide.Evaluate(variables);

                            // If we got here then the lhsResult is not null.
                            if (rhsResult == null)
                            {
                                return true;
                            }
                        }

                        return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) != 0;
                    }
                case BinaryExpressionType.LessThanOrEqual:
                    {
                        // Use the type of the left operand to make the comparison
                        if (lhsResult == null)
                        {
                            return null;
                        }

                        var rhsResult = _rightHandSide.Evaluate(variables);
                        if (rhsResult == null)
                        {
                            return null;
                        }

                        return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) <= 0;
                    }
                case BinaryExpressionType.GreaterThanOrEqual:
                    {
                        // Use the type of the left operand to make the comparison
                        if (lhsResult == null)
                        {
                            return null;
                        }

                        var rhsResult = _rightHandSide.Evaluate(variables);
                        if (rhsResult == null)
                        {
                            return null;
                        }

                        return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) >= 0;
                    }
                case BinaryExpressionType.LessThan:
                    {
                        // Use the type of the left operand to make the comparison
                        if (lhsResult == null)
                        {
                            return null;
                        }

                        var rhsResult = _rightHandSide.Evaluate(variables);
                        if (rhsResult == null)
                        {
                            return null;
                        }

                        return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) < 0;
                    }
                case BinaryExpressionType.GreaterThan:
                    {
                        // Use the type of the left operand to make the comparison
                        if (lhsResult == null)
                        {
                            return null;
                        }

                        var rhsResult = _rightHandSide.Evaluate(variables);
                        if (rhsResult == null)
                        {
                            return null;
                        }

                        return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) > 0;
                    }
                case BinaryExpressionType.Equal:
                    {
                        object rhsResult = null;

                        // Use the type of the left operand to make the comparison
                        if (lhsResult == null)
                        {
                            rhsResult = _rightHandSide.Evaluate(variables);
                            if (rhsResult == null)
                            {
                                // 2 nulls make a match!
                                return true;
                            }

                            return false;
                        }
                        else
                        {
                            rhsResult = _rightHandSide.Evaluate(variables);

                            // If we got here then the lhsResult is not null.
                            if (rhsResult == null)
                            {
                                return false;
                            }
                        }

                        return Comparison.CompareUsingMostPreciseType(lhsResult, rhsResult) == 0;
                    }
                case BinaryExpressionType.Subtract:
                    return Numbers.Subtract(lhsResult, _rightHandSide.Evaluate(variables));
                case BinaryExpressionType.Add:
                    if (lhsResult is string)
                    {
                        return ((string)lhsResult) + _rightHandSide.Evaluate(variables) as string;
                    }

                    return Numbers.Add(lhsResult, _rightHandSide.Evaluate(variables));
                case BinaryExpressionType.Modulus:
                    return Numbers.Modulus(lhsResult, _rightHandSide.Evaluate(variables));
                case BinaryExpressionType.Divide:
                    {
                        var rhsResult = _rightHandSide.Evaluate(variables);

                        return (lhsResult == null || rhsResult == null || IsReal(lhsResult) || IsReal(rhsResult))
                                     ? Numbers.Divide(lhsResult, rhsResult)
                                     : Numbers.Divide(Convert.ToDouble(lhsResult), rhsResult);
                    }
                case BinaryExpressionType.Multiply:
                    return Numbers.Multiply(lhsResult, _rightHandSide.Evaluate(variables));
                case BinaryExpressionType.BitwiseOr:
                    return Convert.ToUInt16(lhsResult) | Convert.ToUInt16(_rightHandSide.Evaluate(variables));
                case BinaryExpressionType.BitwiseAnd:
                    return Convert.ToUInt16(lhsResult) & Convert.ToUInt16(_rightHandSide.Evaluate(variables));
                case BinaryExpressionType.BitwiseXOr:
                    return Convert.ToUInt16(lhsResult) ^ Convert.ToUInt16(_rightHandSide.Evaluate(variables));
                case BinaryExpressionType.LeftShift:
                    return Convert.ToUInt16(lhsResult) << Convert.ToUInt16(_rightHandSide.Evaluate(variables));
                case BinaryExpressionType.RightShift:
                    return Convert.ToUInt16(lhsResult) >> Convert.ToUInt16(_rightHandSide.Evaluate(variables));
                case BinaryExpressionType.NullCoalescing:
                    return lhsResult ?? _rightHandSide.Evaluate(variables);
                default:
                    break;
            }

            return null;
        }

        #endregion

        private static bool IsReal(object value)
        {
            var typeCode = Type.GetTypeCode(value.GetType());

            return typeCode == TypeCode.Decimal || typeCode == TypeCode.Double || typeCode == TypeCode.Single;
        }
    }

    internal enum BinaryExpressionType
    {
        Unknown,
        And,
        Or,
        NotEqual,
        LessThanOrEqual,
        GreaterThanOrEqual,
        LessThan,
        GreaterThan,
        Equal,
        Subtract,
        Add,
        Modulus,
        Divide,
        Multiply,
        BitwiseOr,
        BitwiseAnd,
        BitwiseXOr,
        LeftShift,
        RightShift,
        NullCoalescing,
    }
}
