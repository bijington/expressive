using Expressive.Expressions;
using System;

namespace Expressive.Functions.Conversion
{
    internal sealed class IntegerFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Integer"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 1, 1);

            var objectToConvert = parameters[0].Evaluate(Variables);

            // No point converting if there is nothing to convert.
            if (objectToConvert == null) return null;

            return Convert.ToInt32(objectToConvert);
        }

        #endregion
    }
}
