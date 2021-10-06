using Expressive.Expressions;
using Expressive.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Expressive.Functions.Statistical
{
    internal class MeanFunction : FunctionBase
    {
        public override string Name => "Mean";

        public override object Evaluate(IExpression[] parameters, Context context)
        {
            this.ValidateParameterCount(parameters, -1, 1);

            return Evaluate(parameters, Variables);
        }

        internal static object Evaluate(IExpression[] parameters, IDictionary<string, object> variables)
        {
            var count = 0;
            object result = 0;

            foreach (var value in parameters)
            {
                var increment = 1;
                var evaluatedValue = value.Evaluate(variables);

                if (evaluatedValue is IEnumerable enumerable)
                {
                    var enumerableCount = 0;
                    object enumerableSum = 0;

                    foreach (var item in enumerable)
                    {
                        if (item is null)
                        {
                            continue;
                        }

                        enumerableCount++;
                        enumerableSum = Numbers.Add(enumerableSum, item);
                    }

                    increment = enumerableCount;
                    evaluatedValue = enumerableSum;
                }
                else if (evaluatedValue is null)
                {
                    continue;
                }

                result = Numbers.Add(result, evaluatedValue);
                count += increment;
            }

            return Convert.ToDouble(result) / count;
        }
    }
}
