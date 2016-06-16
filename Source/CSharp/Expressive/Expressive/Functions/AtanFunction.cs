using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class AtanFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Atan"; } }

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 1, 1);

            return Math.Atan(Convert.ToDouble(parameters[0].Evaluate(Variables)));
        }

        #endregion
    }
}
