using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class SinFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Sin"; } }

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 1, 1);

            return Math.Sin(Convert.ToDouble(parameters[0].Evaluate(Variables)));
        }

        #endregion
    }
}
