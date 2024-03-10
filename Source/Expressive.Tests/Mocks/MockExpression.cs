using Expressive.Expressions;
using System.Collections.Generic;

namespace Expressive.Tests
{
    public class MockExpression : IExpression
    {
        private readonly object result;

        private MockExpression(object result)
        {
            this.result = result;
        }

        public static MockExpression ThatEvaluatesTo(object result) => new MockExpression(result);

        public object Evaluate(IDictionary<string, object> variables) => this.result;
    }
}