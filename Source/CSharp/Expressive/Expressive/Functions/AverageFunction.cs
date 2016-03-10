using Expressive.Expressions;
using Expressive.Helpers;
using System;
using System.Collections.Generic;

namespace Expressive.Functions
{
    internal class AverageFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Average"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, -1, 1);

            object result = 0;

            foreach (var value in participants)
            {
                result = Numbers.Add(result, value.Evaluate(Arguments));
            }

            return Convert.ToDouble(result) / ((double)participants.Length);
        }

        #endregion
    }
}
