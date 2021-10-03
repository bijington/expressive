using Expressive.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Expressive.Functions.Statistical
{
    internal class MedianFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name => "Median";

        public override object Evaluate(IExpression[] parameters, Context context)
        {
            this.ValidateParameterCount(parameters, -1, 1);

            IList<decimal> decimalValues = new List<decimal>();

            foreach (var p in parameters)
            {
                var value = p.Evaluate(this.Variables);

                if (value is IEnumerable enumerable)
                {
                    foreach (var item in enumerable)
                    {
                        AddValue(item, decimalValues);
                    }
                }
                else
                {
                    AddValue(value, decimalValues);
                }
            }

            return Median(decimalValues.ToArray());
        }

        #endregion

        private static void AddValue(object value, IList<decimal> decimalValues)
        {
            if (value is null)
            {
                return;
            }

            decimalValues.Add(Convert.ToDecimal(value));
        }

        private static decimal Median(IEnumerable<decimal> xs)
        {
            var ys = xs.OrderBy(x => x).ToList();
            var mid = (ys.Count - 1) / 2.0;
            return (ys[(int)(mid)] + ys[(int)(mid + 0.5)]) / 2;
        }
    }
}
