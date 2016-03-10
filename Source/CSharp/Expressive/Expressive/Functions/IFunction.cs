using Expressive.Expressions;
using System.Collections.Generic;

namespace Expressive.Functions
{
    public interface IFunction
    {
        IDictionary<string, object> Arguments { get; set; }

        string Name { get; }

        object Evaluate(IExpression[] participants);
    }
}
