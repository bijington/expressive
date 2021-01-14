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

        /// <inheritdoc />
        public object Evaluate(IDictionary<string, object> variables)
        {
            if (variables is null ||
                !variables.TryGetValue(this.variableName, out var variableValue))
            {
                throw new ArgumentException("The variable '" + this.variableName + "' has not been supplied.");
            }

            // Check to see if we have to referred to another expression.
            if (variableValue is IExpression expression)
            {
                return expression.Evaluate(variables);
            }

            return variableValue;
        }

        #endregion
    }
}
