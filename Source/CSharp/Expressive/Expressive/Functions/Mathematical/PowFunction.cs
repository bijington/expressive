using Expressive.Expressions;
using System;

namespace Expressive.Functions.Mathematical
{
    internal class PowFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Pow"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);

            return Math.Pow(Convert.ToDouble(parameters[0].Evaluate(Variables)), Convert.ToDouble(parameters[1].Evaluate(Variables)));
        }

        #endregion
    }
}
