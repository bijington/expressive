using Expressive.Expressions;
using System;
using System.Linq;

namespace Expressive.Functions
{
    internal class MedianFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Median"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, -1, 1);

            object result = 0;

            var values = participants.Select(p => p.Evaluate(this.Arguments));

            return Median(values.Select(v => Convert.ToDecimal(v)).ToArray());
        }

        #endregion

        decimal Median(decimal[] xs)
        {
            var ys = xs.OrderBy(x => x).ToList();
            double mid = (ys.Count - 1) / 2.0;
            return (ys[(int)(mid)] + ys[(int)(mid + 0.5)]) / 2;
        }
    }
}
