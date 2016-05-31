using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class SqrtFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Sqrt"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            return Math.Sqrt(Convert.ToDouble(participants[0].Evaluate(Arguments)));
        }

        #endregion
    }
}
