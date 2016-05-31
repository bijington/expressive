using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class AsinFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Asin"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            return Math.Asin(Convert.ToDouble(participants[0].Evaluate(Arguments)));
        }

        #endregion
    }
}
