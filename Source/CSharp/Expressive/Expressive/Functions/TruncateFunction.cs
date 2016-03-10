using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class TruncateFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Truncate"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            return Math.Truncate(Convert.ToDouble(participants[0].Evaluate(Arguments)));
        }

        #endregion
    }
}
