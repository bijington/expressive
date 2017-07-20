using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddSecondsFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "AddSeconds"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            var dateObject = parameters[0].Evaluate(Variables);
            var secondsObject = parameters[1].Evaluate(Variables);

            if (dateObject == null || secondsObject == null) return null;

            DateTime date = Convert.ToDateTime(dateObject);
            double seconds = Convert.ToDouble(secondsObject);

            return date.AddSeconds(seconds);
        }

        #endregion
    }
}
