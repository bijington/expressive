using Expressive.Expressions;
using System;

namespace Expressive.Functions.String
{
    internal class PadLeftFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name
        {
            get
            {
                return "PadLeft";
            }
        }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, 3, 3);

            object value = parameters[0].Evaluate(Variables);
            object length = parameters[1].Evaluate(Variables);

            if (value == null || length == null) return null;

            string text = null;
            if (value is string)
            {
                text = (string)value;
            }
            else
            {
                text = value.ToString();
            }

            int totalLength = Convert.ToInt32(length);

            var third = parameters[2].Evaluate(Variables);
            char character = ' ';

            if (third is char)
            {
                character = (char)third;
            }
            else if (third is string)
            {
                character = (char)((string)third)[0];
            }
            
            return text.PadLeft(totalLength, character);
        }

        #endregion
    }
}
