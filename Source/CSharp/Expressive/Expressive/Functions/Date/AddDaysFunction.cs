using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddDaysFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "AddDays";

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            var dateObject = parameters[0].Evaluate(this.Variables);
            var daysObject = parameters[1].Evaluate(this.Variables);

            if (dateObject == null || daysObject == null) return null;

            DateTime date = Convert.ToDateTime(dateObject);
            double days = Convert.ToDouble(daysObject);

            return date.AddDays(days);
        }

        #endregion
    }
}
