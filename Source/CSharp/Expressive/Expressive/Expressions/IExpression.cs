using System.Collections.Generic;

namespace Expressive.Expressions
{
    internal interface IExpression
    {
        object Evaluate(IDictionary<string, object> arguments);
    }
}
