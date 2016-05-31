using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class SinFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Sin"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            return Math.Sin(Convert.ToDouble(participants[0].Evaluate(Arguments)));
        }

        #endregion
    }
}
