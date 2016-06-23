using Expressive.Expressions;

namespace Expressive.Functions
{
    internal class PadRightFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name
        {
            get
            {
                return "PadRight";
            }
        }

        public override object Evaluate(IExpression[] parameters)
        {
            this.ValidateParameterCount(parameters, 3, 3);

            object value = parameters[0].Evaluate(Variables);

            string text = null;
            if (value is string)
            {
                text = (string)value;
            }
            else
            {
                text = value.ToString();
            }

            int totalLength = (int)parameters[1].Evaluate(Variables);

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

            return text.PadRight(totalLength, character);
        }

        #endregion
    }
}
