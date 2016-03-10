using Expressive.Expressions;
using Expressive.Helpers;

namespace Expressive.Functions
{
    internal class InFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "In"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, -1, 2);

            bool found = false;

            object parameter = participants[0].Evaluate(Arguments);

            // Goes through any values, and stop whe one is found
            for (int i = 1; i < participants.Length; i++)
            {
                if (Comparison.CompareUsingMostPreciseType(parameter, participants[i].Evaluate(Arguments)) == 0)
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
