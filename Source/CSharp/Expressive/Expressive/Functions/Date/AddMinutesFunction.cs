using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddMinutesFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "AddMinutes"; } }

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            DateTime date = Convert.ToDateTime(parameters[0].Evaluate(Variables));
            double days = Convert.ToDouble(parameters[1].Evaluate(Variables));

            return date.AddMinutes(days);
        }

        #endregion
    }
}
