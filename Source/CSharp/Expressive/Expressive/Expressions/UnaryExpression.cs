using Expressive.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Expressions
{
    internal class UnaryExpression : IExpression
    {
        private readonly IExpression _expression;
        private readonly UnaryExpressionType _expressionType;

        internal UnaryExpression(UnaryExpressionType type, IExpression expression)
        {
            _expressionType = type;
            _expression = expression;
        }

        #region IExpression Members

        public object Evaluate(IDictionary<string, object> arguments)
        {
            switch (_expressionType)
            {
                case UnaryExpressionType.Minus:
                    return Numbers.Subtract(0, _expression.Evaluate(arguments));
                case UnaryExpressionType.Not:
                    var value = _expression.Evaluate(arguments);

                    if (value != null)
                    {
                        var valueType = Type.GetTypeCode(value.GetType());

                        if (value is bool)
                        {
                            return !(bool)value;
                        }

                        return Convert.ToBoolean(value);
                    }
                    break;
                case UnaryExpressionType.Plus:
                    return Numbers.Add(0, _expression.Evaluate(arguments));
            }

            return null;
        }

        #endregion
    }

    internal enum UnaryExpressionType
    {
        Minus,
        Not,
        Plus,
    }
}
