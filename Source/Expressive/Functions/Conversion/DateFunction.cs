﻿using Expressive.Expressions;
using System;
using System.Globalization;

namespace Expressive.Functions.Conversion
{
    // begin-snippet: DateFunction
    //Converts and returns the parameter as a DateTime.

    //Date(value)

    //or

    //Date(value, format)
    // end-snippet

    internal sealed class DateFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "Date";

        public override object Evaluate(IExpression[] parameters, Context context)
        {
            this.ValidateParameterCount(parameters, -1, 1);

            var objectToConvert = parameters[0].Evaluate(this.Variables);
            
            // No point converting if there is nothing to convert.
            if (objectToConvert is null) { return null; }

            // Safely check for a format parameter.
            if (parameters.Length > 1 &&
                objectToConvert is string dateString)
            {
                var format = parameters[1].Evaluate(this.Variables);

                if (format is string formatString)
                {
                    return DateTime.ParseExact(dateString, formatString, context.CurrentCulture);
                }
            }

            return Convert.ToDateTime(objectToConvert, context.CurrentCulture);
        }

        #endregion
    }
}
