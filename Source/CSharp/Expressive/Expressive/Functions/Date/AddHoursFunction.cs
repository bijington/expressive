using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddHoursFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "AddHours";

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            var dateObject = parameters[0].Evaluate(this.Variables);
            var hoursObject = parameters[1].Evaluate(this.Variables);

            if (dateObject == null || hoursObject == null) return null;

            DateTime date = Convert.ToDateTime(dateObject);
            double hours = Convert.ToDouble(hoursObject);

            return date.AddHours(hours);
        }

        #endregion
    }
}
