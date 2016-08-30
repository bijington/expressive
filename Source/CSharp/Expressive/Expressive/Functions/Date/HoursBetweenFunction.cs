using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class HoursBetweenFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "HoursBetween"; } }

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            DateTime start = Convert.ToDateTime(parameters[0].Evaluate(Variables));
            DateTime end = Convert.ToDateTime(parameters[1].Evaluate(Variables));

            return (end - start).TotalHours;
        }

        #endregion
    }
}
