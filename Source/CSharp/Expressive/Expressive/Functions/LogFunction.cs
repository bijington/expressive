using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class LogFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Log"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 2, 2);

            return Math.Log(Convert.ToDouble(participants[0].Evaluate(Arguments)), Convert.ToDouble(participants[1].Evaluate(Arguments)));
        }

        #endregion
    }
}
