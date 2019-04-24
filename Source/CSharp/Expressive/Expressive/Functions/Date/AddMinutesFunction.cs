using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddMinutesFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "AddMinutes";

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            var dateObject = parameters[0].Evaluate(this.Variables);
            var minutesObject = parameters[1].Evaluate(this.Variables);

            if (dateObject == null || minutesObject == null) return null;

            if(dateObject.ToString() == "" || minutesObject.ToString() == "") return null;
            
            DateTime date = Convert.ToDateTime(dateObject);
            double days = Convert.ToDouble(minutesObject);

            return date.AddMinutes(days);
        }

        #endregion
    }
}
