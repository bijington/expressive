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

        public object Evaluate(IDictionary<string, object> arguments)
        {
            if (_leftHandSide == null || _rightHandSide == null)
            {
                return null;
            }

            // We will evaluate the left hand side but hold off on the right hand side as it may not be necessary
            var lhsResult = _leftHandSide.Evaluate(arguments);

            switch (_expressionType)
            {
                case BinaryExpressionType.Unknown:
                    break;
                case BinaryExpressionType.And:
                    return Convert.ToBoolean(lhsResult) && Convert.ToBoolean(_rightHandSide.Evaluate(arguments));
                case BinaryExpressionType.Or:
                    return Convert.ToBoolean(lhsResult) || Convert.ToBoolean(_rightHandSide.Evaluate(arguments));
                case BinaryExpressionType.NotEqual:
                    // Use the type of the left operand to make the comparison
                    return Comparison.CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) != 0;
                case BinaryExpressionType.LessThanOrEqual:
                    // Use the type of the left operand to make the comparison
                    return Comparison.CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) <= 0;
                case BinaryExpressionType.GreaterThanOrEqual:
                    // Use the type of the left operand to make the comparison
                    return Comparison.CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) >= 0;
                case BinaryExpressionType.LessThan:
                    // Use the type of the left operand to make the comparison
                    return Comparison.CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) < 0;
                case BinaryExpressionType.GreaterThan:
                    // Use the type of the left operand to make the comparison
                    return Comparison.CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) > 0;
                case BinaryExpressionType.Equal:
                    // Use the type of the left operand to make the comparison
                    return Comparison.CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) == 0;
                case BinaryExpressionType.Subtract:
                    return Numbers.Subtract(lhsResult, _rightHandSide.Evaluate(arguments));
                case BinaryExpressionType.Add:
                    if (lhsResult is string)
                    {
                        return ((string)lhsResult) + _rightHandSide.Evaluate(arguments) as string;
                    }

                    return Numbers.Add(lhsResult, _rightHandSide.Evaluate(arguments));
                case BinaryExpressionType.Modulus:
                    return Numbers.Modulus(lhsResult, _rightHandSide.Evaluate(arguments));
                case BinaryExpressionType.Divide:
                    var rhsResult = _rightHandSide.Evaluate(arguments);

                    return (lhsResult == null || rhsResult == null || IsReal(lhsResult) || IsReal(rhsResult))
                                 ? Numbers.Divide(lhsResult, rhsResult)
                                 : Numbers.Divide(Convert.ToDouble(lhsResult), rhsResult);
                case BinaryExpressionType.Multiply:
                    return Numbers.Multiply(lhsResult, _rightHandSide.Evaluate(arguments));
                case BinaryExpressionType.BitwiseOr:
                    return Convert.ToUInt16(lhsResult) | Convert.ToUInt16(_rightHandSide.Evaluate(arguments));
                case BinaryExpressionType.BitwiseAnd:
                    return Convert.ToUInt16(lhsResult) & Convert.ToUInt16(_rightHandSide.Evaluate(arguments));
                case BinaryExpressionType.BitwiseXOr:
                    return Convert.ToUInt16(lhsResult) ^ Convert.ToUInt16(_rightHandSide.Evaluate(arguments));
                case BinaryExpressionType.LeftShift:
                    return Convert.ToUInt16(lhsResult) << Convert.ToUInt16(_rightHandSide.Evaluate(arguments));
                case BinaryExpressionType.RightShift:
                    return Convert.ToUInt16(lhsResult) >> Convert.ToUInt16(_rightHandSide.Evaluate(arguments));
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
    }
}
