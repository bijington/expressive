using Expressive.Expressions;
using Expressive.Helpers;
using System.Collections;
using System.Linq;

namespace Expressive.Functions.Logical
{
    internal class InFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "In"; } }

        public override object Evaluate(IExpression[] parameters, Context context)
        {
            this.ValidateParameterCount(parameters, -1, 2);

            var found = false;

            var parameter = parameters[0].Evaluate(Variables);

            // Goes through any values, and stop whe one is found
            for (var i = 1; i < parameters.Length; i++)
            {
                var value = parameters[i].Evaluate(Variables);

                if (value is ICollection values)
                {
                    if (values.Cast<object>().Any(innerValue => Comparison.CompareUsingMostPreciseType(parameter, innerValue, context) == 0))
                    {
                        found = true;
                        break;
                    }
                }
                else
                {
                    found = Comparison.CompareUsingMostPreciseType(parameter, value, context) == 0;
                    
                    if (found)
                    {
                        break;
                    }
                }
            }

            return found;
        }

        #endregion
    }
}
