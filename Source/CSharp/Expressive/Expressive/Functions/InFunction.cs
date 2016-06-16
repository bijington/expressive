using Expressive.Expressions;
using Expressive.Helpers;

namespace Expressive.Functions
{
    internal class InFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "In"; } }

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, -1, 2);

            bool found = false;

            object parameter = parameters[0].Evaluate(Variables);

            // Goes through any values, and stop whe one is found
            for (int i = 1; i < parameters.Length; i++)
            {
                if (Comparison.CompareUsingMostPreciseType(parameter, parameters[i].Evaluate(Variables)) == 0)
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        #endregion
    }
}
