using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Expressive.Exceptions;
using Expressive.Expressions;
using Expressive.Functions;
using Expressive.Functions.Conversion;
using Expressive.Functions.Date;
using Expressive.Functions.Logical;
using Expressive.Functions.Mathematical;
using Expressive.Functions.Relational;
using Expressive.Functions.Statistical;
using Expressive.Functions.String;
using Expressive.Operators;
using Expressive.Operators.Additive;
using Expressive.Operators.Bitwise;
using Expressive.Operators.Conditional;
using Expressive.Operators.Grouping;
using Expressive.Operators.Logical;
using Expressive.Operators.Multiplicative;
using Expressive.Operators.Relational;

namespace Expressive
{
    public class Context
    {
        internal const char DateSeparator = '#';
        internal const char ParameterSeparator = ',';

        #region Fields

        // TODO: Perhaps this knowledge is better held under specific expression compilers? Or is that a level too far?
        private readonly IDictionary<string, Func<IExpression[], IDictionary<string, object>, object>> registeredFunctions;
        private readonly IDictionary<string, IOperator> registeredOperators;

        #endregion

        #region Properties

        internal ExpressiveOptions Options { get; }

        internal CultureInfo CurrentCulture { get; }

        internal char DecimalSeparator { get; }

        internal IEnumerable<string> FunctionNames => this.registeredFunctions.Keys.OrderByDescending(k => k.Length);

        internal IEnumerable<string> OperatorNames => this.registeredOperators.Keys.OrderByDescending(k => k.Length);

        internal StringComparer StringComparer => Options.HasFlag(ExpressiveOptions.IgnoreCase)
            ? StringComparer.OrdinalIgnoreCase
            : StringComparer.Ordinal;

        internal StringComparison StringComparison => Options.HasFlag(ExpressiveOptions.IgnoreCase)
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;

        #endregion

        #region Constructors

        internal Context(ExpressiveOptions options)
        {
            Options = options;

            // For now we will ignore any specific cultures but keeping it in a single place to simplify changing later if required.
            this.CurrentCulture = CultureInfo.InvariantCulture;

            DecimalSeparator = Convert.ToChar(this.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            this.registeredFunctions = new Dictionary<string, Func<IExpression[], IDictionary<string, object>, object>>(StringComparer);
            this.registeredOperators = new Dictionary<string, IOperator>(StringComparer);

            #region Operators
            // TODO: Do we allow for turning off operators?
            // Additive
            this.RegisterOperator(new PlusOperator());
            this.RegisterOperator(new SubtractOperator());
            // Bitwise
            this.RegisterOperator(new BitwiseAndOperator());
            this.RegisterOperator(new BitwiseOrOperator());
            this.RegisterOperator(new BitwiseExclusiveOrOperator());
            this.RegisterOperator(new LeftShiftOperator());
            this.RegisterOperator(new RightShiftOperator());
            // Conditional
            this.RegisterOperator(new NullCoalescingOperator());
            // Grouping
            this.RegisterOperator(new ParenthesisCloseOperator());
            this.RegisterOperator(new ParenthesisOpenOperator());
            // Logic
            this.RegisterOperator(new AndOperator());
            this.RegisterOperator(new NotOperator());
            this.RegisterOperator(new OrOperator());
            // Multiplicative
            this.RegisterOperator(new DivideOperator());
            this.RegisterOperator(new ModulusOperator());
            this.RegisterOperator(new MultiplyOperator());
            // Relational
            this.RegisterOperator(new EqualOperator());
            this.RegisterOperator(new GreaterThanOperator());
            this.RegisterOperator(new GreaterThanOrEqualOperator());
            this.RegisterOperator(new LessThanOperator());
            this.RegisterOperator(new LessThanOrEqualOperator());
            this.RegisterOperator(new NotEqualOperator());
            #endregion

            #region Functions
            // Conversion
            this.RegisterFunction(new DateFunction());
            this.RegisterFunction(new DecimalFunction());
            this.RegisterFunction(new DoubleFunction());
            this.RegisterFunction(new IntegerFunction());
            this.RegisterFunction(new LongFunction());
            this.RegisterFunction(new StringFunction());
            // Date
            this.RegisterFunction(new AddDaysFunction());
            this.RegisterFunction(new AddHoursFunction());
            this.RegisterFunction(new AddMillisecondsFunction());
            this.RegisterFunction(new AddMinutesFunction());
            this.RegisterFunction(new AddMonthsFunction());
            this.RegisterFunction(new AddSecondsFunction());
            this.RegisterFunction(new AddYearsFunction());
            this.RegisterFunction(new DayOfFunction());
            this.RegisterFunction(new DaysBetweenFunction());
            this.RegisterFunction(new HourOfFunction());
            this.RegisterFunction(new HoursBetweenFunction());
            this.RegisterFunction(new MillisecondOfFunction());
            this.RegisterFunction(new MillisecondsBetweenFunction());
            this.RegisterFunction(new MinuteOfFunction());
            this.RegisterFunction(new MinutesBetweenFunction());
            this.RegisterFunction(new MonthOfFunction());
            this.RegisterFunction(new SecondOfFunction());
            this.RegisterFunction(new SecondsBetweenFunction());
            this.RegisterFunction(new YearOfFunction());
            // Mathematical
            this.RegisterFunction(new AbsFunction());
            this.RegisterFunction(new AcosFunction());
            this.RegisterFunction(new AsinFunction());
            this.RegisterFunction(new AtanFunction());
            this.RegisterFunction(new CeilingFunction());
            this.RegisterFunction(new CosFunction());
            this.RegisterFunction(new CountFunction());
            this.RegisterFunction(new ExpFunction());
            this.RegisterFunction(new FloorFunction());
            this.RegisterFunction(new IEEERemainderFunction());
            this.RegisterFunction(new Log10Function());
            this.RegisterFunction(new LogFunction());
            this.RegisterFunction(new PowFunction());
            this.RegisterFunction(new RandomFunction());
            this.RegisterFunction(new RoundFunction());
            this.RegisterFunction(new SignFunction());
            this.RegisterFunction(new SinFunction());
            this.RegisterFunction(new SqrtFunction());
            this.RegisterFunction(new SumFunction());
            this.RegisterFunction(new TanFunction());
            this.RegisterFunction(new TruncateFunction());
            // Logical
            this.RegisterFunction(new IfFunction());
            this.RegisterFunction(new InFunction());
            // Relational
            this.RegisterFunction(new MaxFunction());
            this.RegisterFunction(new MinFunction());
            // Statistical
            this.RegisterFunction(new AverageFunction());
            this.RegisterFunction(new MeanFunction());
            this.RegisterFunction(new MedianFunction());
            this.RegisterFunction(new ModeFunction());
            // String
            this.RegisterFunction(new ContainsFunction());
            this.RegisterFunction(new EndsWithFunction());
            this.RegisterFunction(new LengthFunction());
            this.RegisterFunction(new PadLeftFunction());
            this.RegisterFunction(new PadRightFunction());
            this.RegisterFunction(new RegexFunction());
            this.RegisterFunction(new StartsWithFunction());
            this.RegisterFunction(new SubstringFunction());
            #endregion
        }

        #endregion

        #region Internal Methods

        internal void RegisterFunction(string functionName, Func<IExpression[], IDictionary<string, object>, object> function)
        {
            this.CheckForExistingFunctionName(functionName);

            this.registeredFunctions.Add(functionName, function);
        }

        internal void RegisterFunction(IFunction function)
        {
            this.CheckForExistingFunctionName(function.Name);

            this.registeredFunctions.Add(function.Name, (p, a) =>
            {
                function.Variables = a;

                return function.Evaluate(p, Options);
            });
        }

        internal bool TryGetFunction(string functionName, out Func<IExpression[], IDictionary<string, object>, object> value)
        {
            return this.registeredFunctions.TryGetValue(functionName, out value);
        }

        internal bool TryGetOperator(string operatorName, out IOperator value)
        {
            return this.registeredOperators.TryGetValue(operatorName, out value);
        }

        #endregion

        #region Private Methods

        private void CheckForExistingFunctionName(string functionName)
        {
            if (this.registeredFunctions.ContainsKey(functionName))
            {
                throw new FunctionNameAlreadyRegisteredException(functionName);
            }
        }

        private void RegisterOperator(IOperator op)
        {
            foreach (var tag in op.Tags)
            {
                this.registeredOperators.Add(tag, op);
            }
        }

        #endregion
    }
}