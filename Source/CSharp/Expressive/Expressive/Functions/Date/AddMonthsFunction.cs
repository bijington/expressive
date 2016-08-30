using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddMonthsFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "AddMonths"; } }

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            DateTime date = Convert.ToDateTime(parameters[0].Evaluate(Variables));
            int months = Convert.ToInt32(parameters[1].Evaluate(Variables));

            return date.AddMonths(months);
        }

        #endregion
    }
}
