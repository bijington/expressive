using Expressive.Expressions;
using System;

namespace Expressive.Functions.Mathematical
{
    internal class TanFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Tan"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 1, 1);

            return Math.Tan(Convert.ToDouble(parameters[0].Evaluate(Variables)));
        }

        #endregion
    }
}
