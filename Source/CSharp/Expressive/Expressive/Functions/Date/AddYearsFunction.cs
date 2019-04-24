﻿using Expressive.Expressions;
using System;

namespace Expressive.Functions.Date
{
    internal sealed class AddYearsFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "AddYears";

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            var dateObject = parameters[0].Evaluate(this.Variables);
            var yearsObject = parameters[1].Evaluate(this.Variables);

            if (dateObject == null || yearsObject == null) return null;

            if (dateObject.ToString() == "" || yearsObject.ToString() == "") return null;

            DateTime date = Convert.ToDateTime(dateObject);
            int years = Convert.ToInt32(yearsObject);

            return date.AddYears(years);
        }

        #endregion
    }
}
