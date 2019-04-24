﻿using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class HoursBetweenFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "HoursBetween";

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            var startObject = parameters[0].Evaluate(this.Variables);
            var endObject = parameters[1].Evaluate(this.Variables);

            if (startObject == null || endObject == null) return null;

            if (startObject.ToString() == "" || endObject.ToString() == "") return null;

            DateTime start = Convert.ToDateTime(startObject);
            DateTime end = Convert.ToDateTime(endObject);

            return (end - start).TotalHours;
        }

        #endregion
    }
}
