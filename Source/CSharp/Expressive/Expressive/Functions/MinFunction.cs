using Expressive.Expressions;
using Expressive.Helpers;

namespace Expressive.Functions
{
    internal class MinFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Min"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 2, 2);

            return Numbers.Min(participants[0].Evaluate(Arguments), participants[1].Evaluate(Arguments));
        }

        #endregion
    }
}
