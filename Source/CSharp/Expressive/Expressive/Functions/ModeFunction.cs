using Expressive.Expressions;
using System.Linq;

namespace Expressive.Functions
{
    internal class ModeFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Mode"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, -1, 1);

            object result = 0;

            var values = participants.Select(p => p.Evaluate(this.Arguments));

            var groups = values.GroupBy(v => v);
            int maxCount = groups.Max(g => g.Count());
            return groups.First(g => g.Count() == maxCount).Key;
        }

        #endregion
    }
}
