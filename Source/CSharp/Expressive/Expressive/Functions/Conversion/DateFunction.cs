using Expressive.Expressions;
using System;
using System.Globalization;

namespace Expressive.Functions.Conversion
{
    internal sealed class DateFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Date"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, -1, 1);

            var objectToConvert = parameters[0].Evaluate(Variables);
            
            // No point converting if there is nothing to convert.
            if (objectToConvert == null) return null;

            // Safely check for a format parameter.
            if (parameters.Length > 1 &&
                objectToConvert is string dateString)
            {
                var format = parameters[1].Evaluate(Variables);

                if (format is string formatString)
                {
                    return DateTime.ParseExact(dateString, formatString, CultureInfo.CurrentCulture);
                }
            }

            return Convert.ToDateTime(objectToConvert);
        }

        #endregion
    }
}
