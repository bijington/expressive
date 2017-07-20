using Expressive.Expressions;
using Expressive.Helpers;
using System.Collections;
using System.Linq;

namespace Expressive.Functions.Mathematical
{
    internal class MaxFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Max"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, -1, 1);

            object result = parameters[0].Evaluate(Variables);

            if (result is IEnumerable)
            {
                result = this.Max((IEnumerable)result);
            }

            // Null means we should bail out.
            if (result == null)
            {
                return null;
            }

            // Skip the first item in the list as it has already been evaluated.
            foreach (var value in parameters.Skip(1))
            {
                object evaluatedValue = value.Evaluate(Variables);
                IEnumerable enumerable = evaluatedValue as IEnumerable;

                if (enumerable != null)
                {
                    evaluatedValue = this.Max(enumerable);
                }
                
                result = Numbers.Max(result, evaluatedValue);

                // Null means we should bail out.
                if (result == null)
                {
                    return null;
                }
            }

            return result;
        }

        #endregion

        private object Max(IEnumerable enumerable)
        {
            object enumerableResult = null;

            foreach (var item in enumerable)
            {
                // Null means we should bail out.
                if (item == null)
                {
                    return null;
                }

                if (enumerableResult == null)
                {
                    enumerableResult = item;
                    continue;
                }
                
                enumerableResult = Numbers.Max(enumerableResult, item);
            }

            return enumerableResult;
        }
    }
}
