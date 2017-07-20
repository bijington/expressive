using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class HourOfFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "HourOf"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 1, 1);

            var dateObject = parameters[0].Evaluate(Variables);

            if (dateObject == null) return null;

            DateTime date = Convert.ToDateTime(dateObject);

            return date.Hour;
        }

        #endregion
    }
}
