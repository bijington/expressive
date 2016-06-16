using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class AcosFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Acos"; } }

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 1, 1);

            return Math.Acos(Convert.ToDouble(parameters[0].Evaluate(Variables)));
        }

        #endregion
    }
}
