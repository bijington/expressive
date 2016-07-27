using Expressive.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Expressive.Tests
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void Debugging()
        {
            var complex = new Expression("((Sum([ConsumingQty]) * Sum([ConsumingUV])) / Sum([ModifyingQty]))");
            var variables = new Dictionary<string, object>();
            variables.Add("ConsumingQty", new List<object> { 12, 1 });
            variables.Add("ModifyingQty", new List<object> { 5, 3 });
            variables.Add("ConsumingUV", new List<object> { 2, 2 });

            var r = complex.Evaluate(variables);

            //var a = new Expression("(true == '(true')").Evaluate();

            var expression = new Expression("[number1] + [date1]");
            object value = expression.Evaluate(new Dictionary<string, object> { ["number1"] = 1, ["date1"] = DateTime.Now });
            
            Assert.AreEqual(null, value);

            var nullResult = new Expression("[number1] == null").Evaluate(new Dictionary<string, object> { ["number1"] = null });
            Assert.AreEqual(true, nullResult);
        }

        #region Operators

        #region Plus Operator

        [TestMethod]
        public void SimpleIntegerAddition()
        {
            Expression expression = new Expression("1+3");

            object value = expression.Evaluate();

            Assert.AreEqual(4, value);
        }

        [TestMethod]
        public void SimpleDecimalAddition()
        {
            Expression expression = new Expression("1.3+3.5");

            object value = expression.Evaluate();

            Assert.AreEqual(4.8M, value);
        }

        [TestMethod]
        public void ShouldAddDoubleAndDecimal()
        {
            var expression = new Expression("1.8 + Abs([var1])");

            object value = expression.Evaluate(new Dictionary<string, object> { { "var1", 9.2 } });

            Assert.AreEqual(11M, value);
        }

        [TestMethod]
        public void ShouldConcatenateStrings()
        {
            var expression = new Expression("'1.8' + 'suffix'");

            object value = expression.Evaluate();

            Assert.AreEqual("1.8suffix", value);
        }

        [TestMethod]
        public void ShouldHandleUnaryPlus()
        {
            var expression = new Expression("1.8++0.2");

            object value = expression.Evaluate();

            Assert.AreEqual(2.0M, value);

            Assert.AreEqual(-2, new Expression("+(1 * -2)").Evaluate());
        }

        #endregion

        [TestMethod]
        public void SimpleBodmas()
        {
            Expression expression = new Expression("1-3*2");

            object value = expression.Evaluate();

            Assert.AreEqual(-5, value);
        }

        #region Subtract Operator

        [TestMethod]
        public void SimpleIntegerSubtraction()
        {
            Expression expression = new Expression("3-1");

            object value = expression.Evaluate();

            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void SimpleDecimalSubtraction()
        {
            Expression expression = new Expression("3.5-1.2");

            object value = expression.Evaluate();

            Assert.AreEqual(2.3M, value);
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Operator '-' can't be applied to operands of types 'decimal' and 'double'")]
        public void ShouldNotSubtractDoubleAndDecimal()
        {
            var expression = new Expression("1.8 - Abs([var1])");

            object value = expression.Evaluate(new Dictionary<string, object> { { "var1", 0.2 } });
        }

        [TestMethod]
        public void ShouldHandleUnarySubtraction()
        {
            var expression = new Expression("1.8--0.2");

            object value = expression.Evaluate();

            Assert.AreEqual(2.0M, value);

            Assert.AreEqual(2, new Expression("-(1 * -2)").Evaluate());
        }

        #endregion
        
        [TestMethod]
        public void LogicTests()
        {
            Assert.AreEqual(true, new Expression("1 && 1").Evaluate());
            Assert.AreEqual(false, new Expression("false and true").Evaluate());
            Assert.AreEqual(false, new Expression("not true").Evaluate());
            Assert.AreEqual(true, new Expression("!false").Evaluate());
            Assert.AreEqual(true, new Expression("false || 1").Evaluate());
            Assert.AreEqual(false, new Expression("false || !(true && true)").Evaluate());
        }

        [TestMethod]
        public void RelationalTests()
        {
            Assert.AreEqual(true, new Expression("1 == 1").Evaluate());
            Assert.AreEqual(false, new Expression("1 != 1").Evaluate());
            Assert.AreEqual(true, new Expression("1 <> 2").Evaluate());
            Assert.AreEqual(true, new Expression("7 >= 2").Evaluate());
            Assert.AreEqual(false, new Expression("1 >= 2").Evaluate());
            Assert.AreEqual(true, new Expression("7 > 2").Evaluate());
            Assert.AreEqual(false, new Expression("2 > 2").Evaluate());
            Assert.AreEqual(false, new Expression("7 <= 2").Evaluate());
            Assert.AreEqual(true, new Expression("1 <= 2").Evaluate());
            Assert.AreEqual(false, new Expression("7 < 2").Evaluate());
            Assert.AreEqual(true, new Expression("1 < 2").Evaluate());

            // Dates can be parsed to string.
            Assert.AreEqual(true, new Expression("[date1] == '2016-01-01'").Evaluate(new Dictionary<string, object> { ["date1"] = new DateTime(2016, 01, 01) }));
            Assert.AreEqual(true, new Expression("[date1] == '01/01/2016 00:00:00'").Evaluate(new Dictionary<string, object> { ["date1"] = new DateTime(2016, 01, 01) }));

            // Null safety
            Assert.AreEqual(true, new Expression("[number1] == null").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(false, new Expression("[number1] == null").Evaluate(new Dictionary<string, object> { ["number1"] = 2 }));
            Assert.AreEqual(false, new Expression("[number1] != null").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(true, new Expression("[number1] != null").Evaluate(new Dictionary<string, object> { ["number1"] = 2 }));
            Assert.AreEqual(true, new Expression("[number1] <> 2").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(null, new Expression("[number1] >= 2").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(null, new Expression("[number1] > 2").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(null, new Expression("[number1] <= 2").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(null, new Expression("[number1] < 2").Evaluate(new Dictionary<string, object> { ["number1"] = null }));

            Assert.AreEqual(true, new Expression("null == [number1]").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(false, new Expression("null == [number1]").Evaluate(new Dictionary<string, object> { ["number1"] = 2 }));
            Assert.AreEqual(false, new Expression("null != [number1]").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(true, new Expression("null != [number1]").Evaluate(new Dictionary<string, object> { ["number1"] = 2 }));
            Assert.AreEqual(true, new Expression("2 <> [number1]").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(null, new Expression("2 >= [number1]").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(null, new Expression("2 > [number1]").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(null, new Expression("2 <= [number1]").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
            Assert.AreEqual(null, new Expression("2 < [number1]").Evaluate(new Dictionary<string, object> { ["number1"] = null }));
        }

        [TestMethod]
        public void NullCoalescingTests()
        {
            Assert.AreEqual(1, new Expression("null ?? 1").Evaluate());
            Assert.AreEqual(null, new Expression("null ?? null").Evaluate());

            Assert.AreEqual(54, new Expression("[empty] ?? 54").Evaluate(new Dictionary<string, object> { ["empty"] = null }));
        }

        #endregion

        #region Functions

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Abs() takes only 1 argument(s)")]
        public void AbsShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1, new Expression("Abs(-1)").Evaluate());
            Assert.AreEqual(12, new Expression("abs(1,2,4,5)", ExpressiveOptions.IgnoreCase).Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Acos() takes only 1 argument(s)")]
        public void AcosShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Acos(1)").Evaluate());
            Assert.AreEqual(12, new Expression("Acos(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Asin() takes only 1 argument(s)")]
        public void AsinShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Asin(0)").Evaluate());
            Assert.AreEqual(12, new Expression("asin(1,2,4,5)", ExpressiveOptions.IgnoreCase).Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Atan() takes only 1 argument(s)")]
        public void AtanShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Atan(0)").Evaluate());
            Assert.AreEqual(12, new Expression("atan(1,2,4,5)", ExpressiveOptions.IgnoreCase).Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Average() expects at least 1 argument(s)")]
        public void AverageShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(3d, new Expression("Average(1,2,4,5)").Evaluate());
            Assert.AreEqual(1d, new Expression("average(1)", ExpressiveOptions.IgnoreCase).Evaluate());
            Assert.AreEqual(12.5, new Expression("Average(10, 20, 5, 15)").Evaluate());

            new Expression("average()", ExpressiveOptions.IgnoreCase).Evaluate();
        }

        [TestMethod]
        public void AverageShouldHandleIEnumerable()
        {
            Assert.AreEqual(3d, new Expression("Average(1,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 5 } }));
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Ceiling() takes only 1 argument(s)")]
        public void CeilingShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(2M, new Expression("Ceiling(1.5)").Evaluate());
            Assert.AreEqual(12, new Expression("ceiling(1,2,4,5)", ExpressiveOptions.IgnoreCase).Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Cos() takes only 1 argument(s)")]
        public void CosShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1d, new Expression("Cos(0)").Evaluate());
            Assert.AreEqual(12, new Expression("Cos(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Count() expects at least 1 argument(s)")]
        public void CountShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(1, new Expression("Count(0)").Evaluate());
            Assert.AreEqual(4, new Expression("count(1,2,4,5)", ExpressiveOptions.IgnoreCase).Evaluate());

            new Expression("Count()").Evaluate();
        }

        [TestMethod]
        public void CountShouldHandleIEnumerable()
        {
            Assert.AreEqual(5, new Expression("Count([array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 0, 1, 2, 3, 4 } }));
            Assert.AreEqual(5, new Expression("Count([array])").Evaluate(new Dictionary<string, object> { ["array"] = new List<int> { 0, 1, 2, 3, 4 } }));
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Exp() takes only 1 argument(s)")]
        public void ExpShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1d, new Expression("Exp(0)").Evaluate());
            Assert.AreEqual(12, new Expression("exp(1,2,4,5)", ExpressiveOptions.IgnoreCase).Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Floor() takes only 1 argument(s)")]
        public void FloorShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1d, new Expression("Floor(1.5)").Evaluate());
            Assert.AreEqual(12, new Expression("Floor(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "IEEERemainder() takes only 2 argument(s)")]
        public void IEEERemainderShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(-1d, new Expression("IEEERemainder(3, 2)").Evaluate());
            Assert.AreEqual(12, new Expression("IEEERemainder(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "If() takes only 3 argument(s)")]
        public void IfShouldHandleOnlyThreeArguments()
        {
            Assert.AreEqual("Low risk", new Expression("If(1 > 8, 'High risk', 'Low risk')").Evaluate());
            Assert.AreEqual("Low risk", new Expression("If(1 > 8, 1 / 0, 'Low risk')").Evaluate());
            Assert.AreEqual(12, new Expression("If(1 > 9, 2, 4, 5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "In() expects at least 2 argument(s)")]
        public void InShouldHandleAtLeastTwoArguments()
        {
            Assert.AreEqual(false, new Expression("In('abc','def','ghi','jkl')").Evaluate());
            Assert.AreEqual(1, new Expression("in(0)", ExpressiveOptions.IgnoreCase).Evaluate());

            new Expression("In()", ExpressiveOptions.IgnoreCase).Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Length() takes only 1 argument(s)")]
        public void LengthShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(5, new Expression("Length('abcde')").Evaluate());

            new Expression("Length()", ExpressiveOptions.IgnoreCase).Evaluate();
        }

        [TestMethod]
        public void LengthShouldHandleNotJustStrings()
        {
            Assert.AreEqual(3, new Expression("Length(123)").Evaluate());
            Assert.AreEqual(null, new Expression("Length(null)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Log() takes only 2 argument(s)")]
        public void LogShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(0d, new Expression("Log(1, 10)").Evaluate());
            Assert.AreEqual(12, new Expression("Log(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Log10() takes only 1 argument(s)")]
        public void Log10ShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Log10(1)").Evaluate());
            Assert.AreEqual(12, new Expression("Log10(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Max() expects at least 1 argument(s)")]
        public void MaxShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(3, new Expression("Max(3, 2)").Evaluate());
            Assert.AreEqual(12, new Expression("Max()").Evaluate());
        }

        [TestMethod]
        public void MaxShouldHandleIEnumerable()
        {
            Assert.AreEqual(50, new Expression("Max(1,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 50 } }));
        }

        [TestMethod]
        public void MaxShouldIgnoreNull()
        {
            Assert.AreEqual(null, new Expression("Max(null,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 50 } }));
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Mean() expects at least 1 argument(s)")]
        public void MeanShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(3d, new Expression("Mean(1,2,4,5)").Evaluate());
            Assert.AreEqual(1d, new Expression("mean(1)", ExpressiveOptions.IgnoreCase).Evaluate());
            Assert.AreEqual(12.5, new Expression("Mean(10, 20, 5, 15)").Evaluate());

            new Expression("mean()", ExpressiveOptions.IgnoreCase).Evaluate();
        }

        [TestMethod]
        public void MeanShouldHandleIEnumerable()
        {
            Assert.AreEqual(3d, new Expression("Mean(1,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 5 } }));
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Median() expects at least 1 argument(s)")]
        public void MedianShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(3.0M, new Expression("Median(1,2,4,5)").Evaluate());
            Assert.AreEqual(1.0M, new Expression("median(1)", ExpressiveOptions.IgnoreCase).Evaluate());
            Assert.AreEqual(12.5M, new Expression("Median(10, 20, 5, 15)").Evaluate());

            new Expression("median()", ExpressiveOptions.IgnoreCase).Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Min() expects at least 1 argument(s)")]
        public void MinShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(2, new Expression("Min(3, 2)").Evaluate());
            Assert.AreEqual(12, new Expression("Min()").Evaluate());
        }

        [TestMethod]
        public void MinShouldHandleIEnumerable()
        {
            Assert.AreEqual(1, new Expression("Min(1,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 50 } }));
        }

        [TestMethod]
        public void MinShouldIgnoreNull()
        {
            Assert.AreEqual(null, new Expression("Min(null,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 50 } }));
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Mode() expects at least 1 argument(s)")]
        public void ModeShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(2, new Expression("Mode(1,2,4,5,2)").Evaluate());
            Assert.AreEqual(1, new Expression("mode(1)", ExpressiveOptions.IgnoreCase).Evaluate());
            Assert.AreEqual(10, new Expression("Mode(10, 20, 5, 15)").Evaluate());

            new Expression("mode()", ExpressiveOptions.IgnoreCase).Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "PadLeft() takes only 3 argument(s)")]
        public void PadLeftShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual("0000abc", new Expression("PadLeft('abc', 7, '0')").Evaluate());
            Assert.AreEqual("abcdefghi", new Expression("PadLeft('abcdefghi', 7, '0')").Evaluate());
            Assert.AreEqual(null, new Expression("PadLeft(null, 7, '0')").Evaluate());
            Assert.AreEqual(null, new Expression("PadLeft('abcdefghi', null, '0')").Evaluate());
            Assert.AreEqual("   abcd", new Expression("PadLeft('abcd', 7, null)").Evaluate());
            Assert.AreEqual(12, new Expression("PadLeft(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "PadRight() takes only 3 argument(s)")]
        public void PadRightShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual("abc0000", new Expression("PadRight('abc', 7, '0')").Evaluate());
            Assert.AreEqual("abcdefghi", new Expression("PadRight('abcdefghi', 7, '0')").Evaluate());
            Assert.AreEqual(null, new Expression("PadRight(null, 7, '0')").Evaluate());
            Assert.AreEqual(null, new Expression("PadRight('abcdefghi', null, '0')").Evaluate());
            Assert.AreEqual("abcd   ", new Expression("PadRight('abcd', 7, null)").Evaluate());
            Assert.AreEqual(12, new Expression("PadRight(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Pow() takes only 2 argument(s)")]
        public void PowShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(9d, new Expression("Pow(3, 2)").Evaluate());
            Assert.AreEqual(12, new Expression("Pow(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Regex() takes only 2 argument(s)")]
        public void RegexShouldHandleOnlyTwoArguments()
        {
            Expression expression = new Expression(@"Regex('text', '^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$')");
            Assert.AreEqual(false, expression.Evaluate());

            expression = new Expression(@"Regex('text', '^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$', '')");
            Assert.AreEqual(false, expression.Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Round() takes only 2 argument(s)")]
        public void RoundShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(3.22d, new Expression("Round(3.222222, 2)").Evaluate());
            Assert.AreEqual(12, new Expression("Round(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Sign() takes only 1 argument(s)")]
        public void SignShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(-1, new Expression("Sign(-10)").Evaluate());
            Assert.AreEqual(12, new Expression("Sign(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Sin() takes only 1 argument(s)")]
        public void SinShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Sin(0)").Evaluate());
            Assert.AreEqual(12, new Expression("Sin(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Sqrt() takes only 1 argument(s)")]
        public void SqrtShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(5d, new Expression("Sqrt(25)").Evaluate());
            Assert.AreEqual(12, new Expression("Sqrt(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Sum() expects at least 1 argument(s)")]
        public void SumShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(12, new Expression("Sum(1,2,4,5)").Evaluate());
            Assert.AreEqual(1, new Expression("sum(1)", ExpressiveOptions.IgnoreCase).Evaluate());
            Assert.AreEqual(72, new Expression("sum(1,2,4,5,10,20,30)", ExpressiveOptions.IgnoreCase).Evaluate());

            new Expression("Sum()").Evaluate();
        }

        [TestMethod]
        public void SumShouldHandleIEnumerable()
        {
            Assert.AreEqual(10, new Expression("Sum([array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 0, 1, 2, 3, 4 } }));
        }

        [TestMethod]
        public void SumShouldHandleIEnumerableWithNulls()
        {
            var arguments = new Dictionary<string, object>
            {
                ["Value"] = new List<decimal?> { 1.0m, null, 2.5m },
                ["Value1"] = 3m
            };
            var expression = new Expressive.Expression("Sum([Value], [Value1])");

            var result = expression.Evaluate(arguments);

            Assert.IsInstanceOfType(result, typeof(decimal?));
            Assert.AreEqual(6.5m, result);
        }

        [TestMethod]
        public void SumShouldUseZeroInsteadOfNull()
        {
            var arguments = new Dictionary<string, object>
            {
                ["Value1"] = 1.1m,
                ["Value2"] = null
            };
            var expression = new Expressive.Expression("Sum([Value1], [Value2])");

            var result = expression.Evaluate(arguments);

            Assert.IsInstanceOfType(result, typeof(decimal?));
            Assert.AreEqual(1.1m, result);
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Tan() takes only 1 argument(s)")]
        public void TanShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Tan(0)").Evaluate());
            Assert.AreEqual(12, new Expression("Tan(1,2,4,5)").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Truncate() takes only 1 argument(s)")]
        public void TruncateShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1d, new Expression("Truncate(1.7)").Evaluate());
            Assert.AreEqual(12, new Expression("Truncate(1,2,4,5)").Evaluate());
        }

        [TestMethod]
        public void CustomFunctionsWithLambda()
        {
            var exp = new Expression("myfunc('abc')");
            exp.RegisterFunction("myfunc", (p, a) =>
            {
                return 1;
            });

            Assert.AreEqual(1, exp.Evaluate());
        }

        #endregion

        #region General

        [TestMethod]
        public void TestAsync()
        {
            Expression expression = new Expression("1+3");

            AutoResetEvent waitHandle = new AutoResetEvent(false);

            object result = null;

            expression.EvaluateAsync((m, r) =>
            {
                Assert.IsNull(m);
                result = r;
                waitHandle.Set();
            });

            waitHandle.WaitOne();
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void TestAsyncSafety()
        {
            Expression expression = new Expression("1+3+[abc]");

            AutoResetEvent waitHandle = new AutoResetEvent(false);

            object result = null;

            expression.EvaluateAsync((m, r) =>
            {
                Assert.AreEqual(m, "The variable 'abc' has not been supplied.");
                result = r;
                waitHandle.Set();
            });

            waitHandle.WaitOne();
            Assert.IsNull(result);
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "There aren't enough ')' symbols. Expected 2 but there is only 1")]
        public void ShouldIdentifyParenthesisMismatch()
        {
            Expression expression = new Expression("(a + b) * (4 - 2");
            
            object value = expression.Evaluate(new Dictionary<string, object> { { "a", 2 }, { "b", 3 } });
        }

        [TestMethod]
        public void ShouldShortCircuitBooleanExpressions()
        {
            var expression = new Expression("([a] != 0) && ([b]/[a]>2)");

            Assert.AreEqual(false, expression.Evaluate(new Dictionary<string, object> { { "a", 0 } }));
        }

        [TestMethod]
        public void ShouldCompareDates()
        {
            Assert.AreEqual(true, new Expression("#1/1/2009#==#1/1/2009#").Evaluate());
            Assert.AreEqual(false, new Expression("#2/1/2009#==#1/1/2009#").Evaluate());
        }

        [TestMethod]
        public void ShouldEvaluateSubExpressions()
        {
            var volume = new Expression("[surface] * [h]");
            var surface = new Expression("[l] * [K]");

            Assert.AreEqual(6, volume.Evaluate(new Dictionary<string, object> { { "surface", surface }, { "h", 3 }, { "l", 1 }, { "K", 2 } }));
        }

        [TestMethod]
        public void ShouldParseValues()
        {
            Assert.AreEqual(123456, new Expression("123456").Evaluate());
            Assert.AreEqual(new DateTime(2001, 01, 01), new Expression("#01/01/2001#").Evaluate());
            Assert.AreEqual(123.456M, new Expression("123.456").Evaluate());
            Assert.AreEqual(true, new Expression("true").Evaluate());
            Assert.AreEqual("true", new Expression("'true'").Evaluate());
            Assert.AreEqual("qwerty", new Expression("'qwerty'").Evaluate());
        }

        [TestMethod]
        public void ShouldEscapeCharacters()
        {
            Assert.AreEqual("'hello'", new Expression(@"'\'hello\''").Evaluate());
            Assert.AreEqual(" ' hel lo ' ", new Expression(@"' \' hel lo \' '").Evaluate());
            System.Diagnostics.Debug.WriteLine("hel\nlo");
            System.Diagnostics.Debug.WriteLine(new Expression(@"'hel\nlo'").Evaluate());
            Assert.AreEqual("hel\nlo", new Expression(@"'hel\nlo'").Evaluate());
        }

        [TestMethod]
        public void ShouldHandleOperatorsPriority()
        {
            Assert.AreEqual(8, new Expression("2+2+2+2").Evaluate());
            Assert.AreEqual(16, new Expression("2*2*2*2").Evaluate());
            Assert.AreEqual(6, new Expression("2*2+2").Evaluate());
            Assert.AreEqual(6, new Expression("2+2*2").Evaluate());

            Assert.AreEqual(9d, new Expression("1 + 2 + 3 * 4 / 2").Evaluate());
            Assert.AreEqual(13.5M, new Expression("18.0/2.0/2.0*3.0").Evaluate());
        }

        [TestMethod]
        public void ShouldNotLosePrecision()
        {
            Assert.AreEqual(0.5, new Expression("3/6").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Unrecognised token 'blarsh'")]
        public void ShouldFailOnUnrecognisedToken()
        {
            Assert.AreEqual(0.5, new Expression("1 + blarsh + 4").Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "The variable 'a' has not been supplied.")]
        public void ShouldHandleCaseSensitivity()
        {
            new Expression("([a] + [b]) * (4 - 2)", ExpressiveOptions.IgnoreCase).Evaluate(new Dictionary<string, object> { { "A", 2 }, { "b", 3 } });
            new Expression("IF(true, true, false)", ExpressiveOptions.IgnoreCase).Evaluate();

            try
            {
                new Expression("IF(true, true, false)").Evaluate();
            }
            catch (UnrecognisedTokenException ute)
            {
                Assert.AreEqual("Unrecognised token 'IF'", ute.Message);
            }

            Expression expression = new Expression("([a] + [b]) * (4 - 2)");

            object value = expression.Evaluate(new Dictionary<string, object> { { "A", 2 }, { "b", 3 } });
        }

        [TestMethod]
        public void ShouldReturnCorrectVariables()
        {
            var expression = new Expression("([a] + [b] * [c]) + ([a] * [b])");

            Assert.AreEqual(3, expression.ReferencedVariables.Length);
        }

        [TestMethod]
        public void CheckNullValuesAreHandledCorrectly()
        {
            Assert.IsNull(new Expression("2 + [a]").Evaluate(new Dictionary<string, object> { ["a"] = null }));
            Assert.IsNull(new Expression("2 * [a]").Evaluate(new Dictionary<string, object> { ["a"] = null }));
            Assert.IsNull(new Expression("2 / [a]").Evaluate(new Dictionary<string, object> { ["a"] = null }));
            Assert.IsNull(new Expression("2 - [a]").Evaluate(new Dictionary<string, object> { ["a"] = null }));
            Assert.IsNull(new Expression("2 % [a]").Evaluate(new Dictionary<string, object> { ["a"] = null }));
        }

        [TestMethod]
        public void CheckNaNIsHandledCorrectly()
        {
            Assert.AreEqual(double.NaN, new Expression("2 + [a]").Evaluate(new Dictionary<string, object> { ["a"] = double.NaN }));
            Assert.AreEqual(double.NaN, new Expression("2 * [a]").Evaluate(new Dictionary<string, object> { ["a"] = double.NaN }));
            Assert.AreEqual(double.NaN, new Expression("2 / [a]").Evaluate(new Dictionary<string, object> { ["a"] = double.NaN }));
            Assert.AreEqual(double.NaN, new Expression("2 - [a]").Evaluate(new Dictionary<string, object> { ["a"] = double.NaN }));
            Assert.AreEqual(double.NaN, new Expression("2 % [a]").Evaluate(new Dictionary<string, object> { ["a"] = double.NaN }));
        }

        [TestMethod]
        public void CheckComplicatedDepth()
        {
            // This was a previous bug (Issue #6) so this is in place to make sure it does not re-occur.
            var expression = new Expression("((1 + 2) * 3) + (([d] / [e]) * [f]) - ([a] * [b])");

            object value = expression.Evaluate(new Dictionary<string, object> { ["a"] = 1, ["b"] = 2, ["c"] = 3, ["d"] = 6, ["e"] = 2, ["f"] = 6 });

            Assert.AreEqual(25d, value);
        }

        [TestMethod]
        public void ShouldThrowExceptionIfEitherSideIsMissing()
        {
            try
            {
                new Expression("<= #today#").Evaluate();
            }
            catch (ExpressiveException ee)
            {
                Assert.AreEqual("The left hand side of the operation is missing.", ee.Message);
            }

            try
            {
                new Expression("#today# <=").Evaluate();
            }
            catch (ExpressiveException ee)
            {
                Assert.AreEqual("The right hand side of the operation is missing.", ee.Message);
            }
        }

        [TestMethod]
        public void ShouldThrowExceptionIfNoContents()
        {
            try
            {
                new Expression("()").Evaluate();
            }
            catch (ExpressiveException ee)
            {
                Assert.AreEqual("Missing contents inside ().", ee.Message);
            }

            try
            {
                new Expression("").Evaluate();
            }
            catch (ExpressiveException ee)
            {
                Assert.IsTrue(string.Equals("An Expression cannot be empty.", ee.Message, StringComparison.Ordinal));
            }
        }

        #endregion
    }
}
