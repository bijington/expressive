using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddMillisecondsFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "AddMilliseconds"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            var dateObject = parameters[0].Evaluate(Variables);
            var millisecondsObject = parameters[1].Evaluate(Variables);

            if (dateObject == null || millisecondsObject == null) return null;

            DateTime date = Convert.ToDateTime(dateObject);
            double milliseconds = Convert.ToDouble(millisecondsObject);

            return date.AddMilliseconds(milliseconds);
        }

        #endregion
    }
}
