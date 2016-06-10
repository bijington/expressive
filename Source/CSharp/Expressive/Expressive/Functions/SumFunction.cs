using Expressive.Expressions;
using Expressive.Helpers;
using System.Collections;

namespace Expressive.Functions
{
    internal class SumFunction : FunctionBase
    {
        #region IFunction Members

        public override string Name { get { return "Sum"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, -1, 1);

            object result = 0;

            foreach (var value in participants)
            {
                object evaluatedValue = value.Evaluate(Arguments);
                IEnumerable enumerable = evaluatedValue as IEnumerable;

                if (enumerable != null)
                {
                    object enumerableSum = 0;
                    foreach (var item in enumerable)
                    {
                        // When summing we don't want to bail out early with a null value.
                        enumerableSum = Numbers.Add(enumerableSum ?? 0, item ?? 0);
                    }
                    evaluatedValue = enumerableSum;
                }

                // When summing we don't want to bail out early with a null value.
                result = Numbers.Add(result ?? 0, evaluatedValue ?? 0);
            }

            return result;
        }

        #endregion
    }
}
