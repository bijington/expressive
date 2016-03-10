using Expressive.Expressions;
using System.Collections.Generic;

namespace Expressive.Functions
{
    internal class CountFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Count"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, -1, 1);

            return participants != null ? participants.Length : 0;
        }

        #endregion
    }
}
