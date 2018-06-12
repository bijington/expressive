using Expressive.Expressions;

namespace Expressive.Functions.Conversion
{
    internal sealed class StringFunction : FunctionBase
    {
        #region FunctionBase Members

        public override string Name { get { return "String"; } }

        public override object Evaluate(IExpression[] parameters, ExpressiveOptions options)
        {
            this.ValidateParameterCount(parameters, -1, 1);

            var objectToConvert = parameters[0].Evaluate(Variables);

            // No point converting if there is nothing to convert.
            if (objectToConvert == null) return null;
            
            // Safely check for a format parameter.
            if (parameters.Length > 1)
            {
                var format = parameters[1].Evaluate(Variables);

                if (format is string formatString)
                {
                    return string.Format($"{{0:{formatString}}}", objectToConvert);
                }
            }

            return objectToConvert.ToString();
        }

        #endregion
    }
}
