using Expressive.Expressions;
using Expressive.Helpers;
using System;


namespace Expressive.Functions.Mathematical
{
    internal class PIFunction : FunctionBase
    {
        public override string Name { get { return "PI"; } }

        public override object Evaluate(IExpression[] parameters, Context context)
        {
            this.ValidateParameterCount(parameters, 0, 0);
            return Math.PI;
        }
    }
}
