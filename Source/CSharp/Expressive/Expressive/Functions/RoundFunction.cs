using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class RoundFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Round"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 2, 2);

            return Math.Round(Convert.ToDouble(participants[0].Evaluate(Arguments)), Convert.ToInt32(participants[1].Evaluate(Arguments)));
        }

        #endregion
    }
}
