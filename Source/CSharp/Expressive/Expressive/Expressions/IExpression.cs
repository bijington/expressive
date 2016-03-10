using System.Collections.Generic;

namespace Expressive.Expressions
{
    public interface IExpression
    {
        object Evaluate(IDictionary<string, object> arguments);
    }
}
