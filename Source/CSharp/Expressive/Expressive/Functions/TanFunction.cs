using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class TanFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Tan"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            return Math.Tan(Convert.ToDouble(participants[0].Evaluate(Arguments)));
        }

        #endregion
    }
}
