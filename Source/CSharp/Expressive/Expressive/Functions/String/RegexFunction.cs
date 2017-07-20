using Expressive.Expressions;
using System.Text.RegularExpressions;

namespace Expressive.Functions.String
{
    internal class RegexFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "Regex"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 2, 2);
            
            return new Regex(parameters[1].Evaluate(Variables) as string).IsMatch(parameters[0].Evaluate(Variables) as string);
        }

        #endregion
    }
}
