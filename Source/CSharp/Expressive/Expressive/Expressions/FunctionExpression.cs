using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expressive.Expressions
{
    internal class FunctionExpression : IExpression
    {
        private readonly Func<IExpression[], IDictionary<string, object>, object> _function;
        private readonly string _name;
        private readonly IExpression[] _parameters;

        internal FunctionExpression(string name, Func<IExpression[], IDictionary<string, object>, object> function, IExpression[] parameters)
        {
            _name = name;
            _function = function;
            _parameters = parameters;
        }

        #region IExpression Members

        public object Evaluate(IDictionary<string, object> arguments)
        {
            //IList<object> evaluatedParameters = new List<object>();

            //foreach (var p in _parameters)
            //{
            //    evaluatedParameters.Add(p.Evaluate(arguments));
            //}

            return _function(_parameters, arguments);
        }

        #endregion
    }
}
