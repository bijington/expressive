using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddHoursFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "AddHours"; } }

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            DateTime date = Convert.ToDateTime(parameters[0].Evaluate(Variables));
            double hours = Convert.ToDouble(parameters[1].Evaluate(Variables));

            return date.AddHours(hours);
        }

        #endregion
    }
}
