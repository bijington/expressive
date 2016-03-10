using Expressive.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Functions
{
    internal class SinFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Sin"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            return Math.Sin(Convert.ToDouble(participants[0].Evaluate(Arguments)));
        }

        #endregion
    }
}
