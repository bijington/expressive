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
                        enumerableSum = Numbers.Add(enumerableSum, item);
                    }
                    evaluatedValue = enumerableSum;
                }

                result = Numbers.Add(result, evaluatedValue);
            }

            return result;
        }

        #endregion
    }
}
