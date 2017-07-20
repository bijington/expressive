using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddMinutesFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "AddMinutes"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            var dateObject = parameters[0].Evaluate(Variables);
            var minutesObject = parameters[1].Evaluate(Variables);

            if (dateObject == null || minutesObject == null) return null;

            DateTime date = Convert.ToDateTime(dateObject);
            double days = Convert.ToDouble(minutesObject);

            return date.AddMinutes(days);
        }

        #endregion
    }
}
