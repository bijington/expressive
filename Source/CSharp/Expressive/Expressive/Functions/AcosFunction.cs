using Expressive.Expressions;
using System;
using System.Collections.Generic;

namespace Expressive.Functions
{
    internal class AcosFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Acos"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            return Math.Acos(Convert.ToDouble(participants[0].Evaluate(Arguments)));
        }

        #endregion
    }
}
