using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class Log10Function : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Log10"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            return Math.Log10(Convert.ToDouble(participants[0].Evaluate(Arguments)));
        }

        #endregion
    }
}
