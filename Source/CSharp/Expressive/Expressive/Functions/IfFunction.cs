using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class IfFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "If"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 3, 3);

            bool condition = Convert.ToBoolean(participants[0].Evaluate(Arguments));

            return condition ? participants[1].Evaluate(Arguments) : participants[2].Evaluate(Arguments);
        }

        #endregion
    }
}
