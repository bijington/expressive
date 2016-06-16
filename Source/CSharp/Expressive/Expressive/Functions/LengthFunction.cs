using Expressive.Expressions;

namespace Expressive.Functions
{
    internal class LengthFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name
        {
            get
            {
                return "Length";
            }
        }

        public override object Evaluate(IExpression[] participants)
        {
            this.ValidateParameterCount(participants, 1, 1);

            string text = (string)participants[0].Evaluate(Arguments);

            // Not very safe at present but let's see for now.
            return text.Length;
        }

        #endregion
    }
}
