using Expressive.Helpers;
using System;
using System.Collections.Generic;

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

        public object Evaluate(IDictionary<string, object> variables)
        {
            switch (_expressionType)
            {
                case UnaryExpressionType.Minus:
                    return Numbers.Subtract(0, _expression.Evaluate(variables));
                case UnaryExpressionType.Not:
                    var value = _expression.Evaluate(variables);

                    if (value != null)
                    {
                        if (value is bool)
                        {
                            return !(bool)value;
                        }

                        return !Convert.ToBoolean(value);
                    }
                    break;
                case UnaryExpressionType.Plus:
                    return Numbers.Add(0, _expression.Evaluate(variables));
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
