using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expressive.Expressions.Unary
{
    internal abstract class UnaryExpressionBase : IExpression
    {
        #region Fields

        protected readonly IExpression expression;

        #endregion

        internal UnaryExpressionBase(IExpression expression)
        {
            this.expression = expression;
        }

        #region IExpression Members

        public abstract object Evaluate(IDictionary<string, object> variables);
        //{
        //    switch (_expressionType)
        //    {
        //        case UnaryExpressionType.Minus:
        //            return Numbers.Subtract(0, _expression.Evaluate(variables));
        //        case UnaryExpressionType.Not:
        //            var value = _expression.Evaluate(variables);

        //            if (value != null)
        //            {
        //                if (value is bool)
        //                {
        //                    return !(bool)value;
        //                }

        //                return !Convert.ToBoolean(value);
        //            }
        //            break;
        //        case UnaryExpressionType.Plus:
        //            return Numbers.Add(0, _expression.Evaluate(variables));
        //    }

        //    return null;
        //}

        #endregion
    }
}
