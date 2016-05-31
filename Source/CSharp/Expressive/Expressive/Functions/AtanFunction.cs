using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class AtanFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Atan"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            return Math.Atan(Convert.ToDouble(participants[0].Evaluate(Arguments)));
        }

        #endregion
    }
}
