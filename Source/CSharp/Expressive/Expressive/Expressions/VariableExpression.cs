using System;
using System.Collections.Generic;

namespace Expressive.Expressions
{
    internal class VariableExpression : IExpression
    {
        private readonly string variableName;

        internal VariableExpression(string variableName)
        {
            this.variableName = variableName;
        }

        #region IExpression Members

        public object Evaluate(IDictionary<string, object> variables)
        {
            if (variables == null ||
                !variables.ContainsKey(this.variableName))
            {
                throw new ArgumentException("The variable '" + this.variableName + "' has not been supplied.");
            }

            // Check to see if we have to referred to another expression.
            var expression = variables[this.variableName] as Expression;
            if (expression != null)
            {
                return expression.Evaluate(variables);
            }

            return variables[this.variableName];
        }

        #endregion
    }
}
