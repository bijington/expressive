using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class IEEERemainderFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "IEEERemainder"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 2, 2);

            return Math.IEEERemainder(Convert.ToDouble(participants[0].Evaluate(Arguments)), Convert.ToDouble(participants[1].Evaluate(Arguments)));
        }

        #endregion
    }
}
