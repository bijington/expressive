using Expressive.Expressions;
using System;

namespace Expressive.Functions
{
    internal class FloorFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Floor"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            return Math.Floor(Convert.ToDouble(participants[0].Evaluate(Arguments)));
        }

        #endregion
    }
}
