using Expressive.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Expressions
{
    internal class BinaryExpression : IExpression
    {
        private readonly BinaryExpressionType _expressionType;
        private readonly IExpression _leftHandSide;
        private readonly IExpression _rightHandSide;

        private static Type[] CommonTypes = new[] { typeof(Int64), typeof(Double), typeof(Boolean), typeof(String), typeof(Decimal) };

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
                    return CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) != 0;
                case BinaryExpressionType.LessThanOrEqual:
                    // Use the type of the left operand to make the comparison
                    return CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) <= 0;
                case BinaryExpressionType.GreaterThanOrEqual:
                    // Use the type of the left operand to make the comparison
                    return CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) >= 0;
                case BinaryExpressionType.LessThan:
                    // Use the type of the left operand to make the comparison
                    return CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) < 0;
                case BinaryExpressionType.GreaterThan:
                    // Use the type of the left operand to make the comparison
                    return CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) > 0;
                case BinaryExpressionType.Equal:
                    // Use the type of the left operand to make the comparison
                    return CompareUsingMostPreciseType(lhsResult, _rightHandSide.Evaluate(arguments)) == 0;
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

                    return (IsReal(lhsResult) || IsReal(rhsResult)) ? Numbers.Divide(lhsResult, rhsResult)
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

        private static int CompareUsingMostPreciseType(object a, object b)
        {
            Type mpt = GetMostPreciseType(a.GetType(), b.GetType());
            return Comparer.Default.Compare(Convert.ChangeType(a, mpt), Convert.ChangeType(b, mpt));
        }

        private static Type GetMostPreciseType(Type a, Type b)
        {
            foreach (Type t in CommonTypes)
            {
                if (a == t || b == t)
                {
                    return t;
                }
            }

            return a;
        }

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
