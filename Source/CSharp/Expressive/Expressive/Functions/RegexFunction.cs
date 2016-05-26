using Expressive.Expressions;
using System.Text.RegularExpressions;

namespace Expressive.Functions
{
    internal class RegexFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Regex"; } }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 2, 2);
            
            return new Regex(participants[1].Evaluate(Arguments) as string).IsMatch(participants[0].Evaluate(Arguments) as string);
        }

        #endregion
    }
}
