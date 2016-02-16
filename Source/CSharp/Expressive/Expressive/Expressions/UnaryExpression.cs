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
                //switch value
                //{
                //    case let boolValue as Bool:
                //result = !boolValue
                //case let intValue as Int:
                //let boolValue = intValue > 0 ? true : false


                //result = !boolValue
                //default:
                //        break
                //}
                //break
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
