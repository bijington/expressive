using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class PowFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Pow"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 2, 2);

            return Math.Pow(Convert.ToDouble(participants[0].Evaluate(Arguments)), Convert.ToDouble(participants[1].Evaluate(Arguments)));
        }

        #endregion
    }
}
