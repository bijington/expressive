using System;
using System.Collections.Generic;

namespace Expressive.Expressions.Unary.Logical
{
    internal class NotExpression : UnaryExpressionBase
    {
        #region Constructors

        public NotExpression(IExpression expression) : base(expression)
        {
        }

        #endregion

        #region UnaryExpressionBase Members

        public override object Evaluate(IDictionary<string, object> variables)
        {
            var value = this.expression.Evaluate(variables);

            switch (value)
            {
                case null:
                    return null;
                case bool boolValue:
                    return !boolValue;
                default:
                    return !Convert.ToBoolean(value);
            }
        }

        #endregion
    }
}
