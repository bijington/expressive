using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddMonthsFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "AddMonths";

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            var dateObject = parameters[0].Evaluate(this.Variables);
            var monthsObject = parameters[1].Evaluate(this.Variables);

            if (dateObject == null || monthsObject == null) return null;

            if (dateObject.ToString() == "" || monthsObject.ToString() == "") return null;

            DateTime date = Convert.ToDateTime(dateObject);
            int months = Convert.ToInt32(monthsObject);

            return date.AddMonths(months);
        }

        #endregion
    }
}
