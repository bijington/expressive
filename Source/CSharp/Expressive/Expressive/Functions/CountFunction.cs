using Expressive.Expressions;
using System;
using System.Collections;

namespace Expressive.Functions
{
    internal class CountFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Count"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, -1, 1);

            int count = 0;

            foreach (var value in participants)
            {
                int increment = 1;
                object evaluatedValue = value.Evaluate(Arguments);
                IEnumerable enumerable = evaluatedValue as IEnumerable;

                if (enumerable != null)
                {
                    int enumerableCount = 0;
                    foreach (var item in enumerable)
                    {
                        enumerableCount++;
                    }
                    
                    increment = enumerableCount;
                }

                count += increment;
            }

            return count;
        }

        #endregion
    }
}
