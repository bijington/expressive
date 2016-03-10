using Expressive.Expressions;
using Expressive.Helpers;

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
                result = Numbers.Add(result, value.Evaluate(Arguments));
            }

            return result;
        }

        #endregion
    }
}
