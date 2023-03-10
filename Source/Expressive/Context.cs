
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
    /// <summary>
    /// Represents context related details about compiling and evaluating an <see cref="IExpression"/>.
    /// </summary>
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

        internal CultureInfo DecimalCurrentCulture { get; }

        internal char DecimalSeparator { get; }

        internal IEnumerable<string> FunctionNames => this.registeredFunctions.Keys.OrderByDescending(k => k.Length);

        internal IEnumerable<string> OperatorNames => this.registeredOperators.Keys.OrderByDescending(k => k.Length);

        private bool IsCaseInsensitiveEqualityEnabled => 
#pragma warning disable 618 // As it is our own warning this is safe enough until we actually get rid
            Options.HasFlag(ExpressiveOptions.IgnoreCase) || Options.HasFlag(ExpressiveOptions.IgnoreCaseForEquality);
#pragma warning restore 618

        internal StringComparison EqualityStringComparison => IsCaseInsensitiveEqualityEnabled
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;

        internal bool IsCaseInsensitiveParsingEnabled =>
#pragma warning disable 618 // As it is our own warning this is safe enough until we actually get rid
            Options.HasFlag(ExpressiveOptions.IgnoreCase) || Options.HasFlag(ExpressiveOptions.IgnoreCaseForParsing);
#pragma warning restore 618

        internal IEqualityComparer<string> ParsingStringComparer => IsCaseInsensitiveParsingEnabled
            ? StringComparer.OrdinalIgnoreCase
            : (IEqualityComparer<string>)EqualityComparer<string>.Default;

        internal StringComparison ParsingStringComparison => IsCaseInsensitiveParsingEnabled
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class with the specified <paramref name="options"/>.
        /// </summary>
        /// <param name="options">The <see cref="ExpressiveOptions"/> to use when evaluating.</param>
        public Context(ExpressiveOptions options) : this(options, CultureInfo.CurrentCulture, CultureInfo.InvariantCulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class with the specified <paramref name="options"/>.
        /// </summary>
        /// <param name="options">The <see cref="ExpressiveOptions"/> to use when evaluating.</param>
        /// <param name="mainCurrentCulture">The <see cref="CultureInfo"/> for use in general parsing/conversions.</param>
        /// <param name="decimalCurrentCulture">The <see cref="CultureInfo"/> for use in decimal parsing/conversions.</param>
        public Context(ExpressiveOptions options, CultureInfo mainCurrentCulture, CultureInfo decimalCurrentCulture)
        {
            Options = options;

            this.CurrentCulture = mainCurrentCulture ?? throw new ArgumentNullException(nameof(mainCurrentCulture));
            // For now we will ignore any specific cultures but keeping it in a single place to simplify changing later if required.
            this.DecimalCurrentCulture = decimalCurrentCulture ?? throw new ArgumentNullException(nameof(decimalCurrentCulture));

            DecimalSeparator = Convert.ToChar(this.DecimalCurrentCulture.NumberFormat.NumberDecimalSeparator, this.DecimalCurrentCulture);
            this.registeredFunctions = new Dictionary<string, Func<IExpression[], IDictionary<string, object>, object>>(this.ParsingStringComparer);
            this.registeredOperators = new Dictionary<string, IOperator>(this.ParsingStringComparer);

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
            // Getting all types implementing "IFunction" interface
            var functionTypeInfos = Utils.Reflective.GetClassesImplementingInterface(typeof(IFunction));
            // Getting our instances from types 
            var functions = Utils.Reflective.GetInstances<IFunction>(functionTypeInfos).Select(element => (IFunction)element).ToList();
            // Register our functions
            this.RegisterFunctions(functions);
            #endregion
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Registers the supplied <paramref name="functions"/> for use within compiling and evaluating an <see cref="Expression"/>.
        /// </summary>
        /// <param name="functions">The list of <see cref="IFunction"/> to perform the function evaluation.</param>
        /// <param name="force">Whether to forcefully override any existing function.</param>
        public void RegisterFunctions(List<IFunction> functions, bool force = false)
        {
            if (functions is null)
            {
                throw new ArgumentNullException(nameof(functions));
            }
            functions.ForEach(function => RegisterFunction(function, force));
        }

        /// <summary>
        /// Registers the supplied <paramref name="function"/> for use within compiling and evaluating an <see cref="Expression"/>.
        /// </summary>
        /// <param name="functionName">The name of the function to register.</param>
        /// <param name="function">A <see cref="Func{T}"/> to perform the function evaluation.</param>
        /// <param name="force">Whether to forcefully override any existing function.</param>
        public void RegisterFunction(string functionName, Func<IExpression[], IDictionary<string, object>, object> function, bool force = false)
        {
            this.CheckForExistingFunctionName(functionName, force);

            this.registeredFunctions[functionName] = function;
        }

        /// <summary>
        /// Registers the supplied <paramref name="function"/> for use within compiling and evaluating an <see cref="Expression"/>.
        /// </summary>
        /// <param name="function">The <see cref="IFunction"/> to perform the function evaluation.</param>
        /// <param name="force">Whether to forcefully override any existing function.</param>
        public void RegisterFunction(IFunction function, bool force = false)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            this.RegisterFunction(
                function.Name,
                (p, a) =>
                {
                    function.Variables = a;

                    return function.Evaluate(p, this);
                },
                force);
        }

        /// <summary>
        /// Registers the supplied <paramref name="op"/> for use within compiling and evaluating an <see cref="Expression"/>.
        /// </summary>
        /// <param name="op">The <see cref="IOperator"/> implementation to register.</param>
        /// <param name="force">Whether to forcefully override any existing <see cref="IOperator"/>.</param>
        /// <remarks>
        /// Please if you are calling this with your own <see cref="IOperator"/> implementations do seriously consider raising an issue to add it in to the general framework:
        /// https://github.com/bijington/expressive
        /// </remarks>
        public void RegisterOperator(IOperator op, bool force = false)
        {
            if (op is null)
            {
                throw new ArgumentNullException(nameof(op));
            }

            foreach (var tag in op.Tags)
            {
                if (!force && this.registeredOperators.ContainsKey(tag))
                {
                    throw new OperatorNameAlreadyRegisteredException(tag);
                }

                this.registeredOperators[tag] = op;
            }
        }

        /// <summary>
        /// Removes the function from the available set of functions when evaluating. 
        /// </summary>
        /// <param name="functionName">The name of the function to remove.</param>
        public void UnregisterFunction(string functionName)
        {
            if (!this.registeredFunctions.ContainsKey(functionName))
            {
                // TODO: do we throw?
            }

            this.registeredFunctions.Remove(functionName);
        }

        /// <summary>
        /// Removes the operator from the available set of operators when evaluating. 
        /// </summary>
        /// <param name="tag">The tag of the operator to remove.</param>
        public void UnregisterOperator(string tag)
        {
            if (!this.registeredOperators.ContainsKey(tag))
            {
                // TODO: do we throw?
            }

            this.registeredOperators.Remove(tag);
        }

        #endregion

        #region Internal Methods

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

        private void CheckForExistingFunctionName(string functionName, bool force)
        {
            if (!force && this.registeredFunctions.ContainsKey(functionName))
            {
                throw new FunctionNameAlreadyRegisteredException(functionName);
            }
        }

        #endregion
    }
}