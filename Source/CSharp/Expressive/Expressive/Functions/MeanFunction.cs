using Expressive.Expressions;
using Expressive.Helpers;
using System;

namespace Expressive.Functions
{
    internal class MeanFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Mean"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, -1, 1);

            object result = 0;

            foreach (var value in participants)
            {
                result = Numbers.Add(result, value.Evaluate(Arguments));
            }

            return Convert.ToDouble(result) / participants.Length;
        }

        #endregion
    }
}
