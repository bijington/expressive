using Expressive.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Expressive.Tests
{
    [TestClass]
    public class ExpressionTests
    {
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

        [TestMethod]
        public void ScientificNotation()
        {
            var expression = new Expression("1.23e2");

            Assert.AreEqual(123, expression.Evaluate());
        }

        [TestMethod]
        public void ScientificNotationWithLargeNumber()
        {
            var expression = new Expression("1.23e22");

            Assert.AreEqual(12300000000000000000000M, expression.Evaluate());
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
			Assert.AreEqual(false, new Expression("[child1] == true || [child2] == true").Evaluate(new Dictionary<string, object> { ["child1"] = new Expression("1 == 0"), ["child2"] = new Expression("1 == 0") }));
			Assert.AreEqual(true, new Expression("[child1] == false || [child2] == true").Evaluate(new Dictionary<string, object> { ["child1"] = new Expression("1 == 0"), ["child2"] = new Expression("1 == 0") }));
			Assert.AreEqual(false, new Expression("[child1] == true && [child2] == true").Evaluate(new Dictionary<string, object> { ["child1"] = new Expression("1 == 0"), ["child2"] = new Expression("1 == 0") }));
			Assert.AreEqual(true, new Expression("[child1] == false && [child2] == false").Evaluate(new Dictionary<string, object> { ["child1"] = new Expression("1 == 0"), ["child2"] = new Expression("1 == 0") }));
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
        }

        [TestMethod]
        public void NullCoalescingTests()
        {
            Assert.AreEqual(1, new Expression("null ?? 1").Evaluate());
            Assert.AreEqual(null, new Expression("null ?? null").Evaluate());

            Assert.AreEqual(54, new Expression("[empty] ?? 54").Evaluate(new Dictionary<string, object> { ["empty"] = null }));
        }

        #endregion

        [TestMethod]
        public void AggregatesHandledInOperators()
        {
            Expression expression = new Expression("[array]+2");

            object value = expression.Evaluate(new Dictionary<string, object> { ["array"] = new object[] { 1, 2, 3, 4 } });

            var valueArray = (object[])value;
            Assert.AreEqual(4, valueArray.Length);
            Assert.AreEqual(3, valueArray[0]);
            Assert.AreEqual(4, valueArray[1]);
            Assert.AreEqual(5, valueArray[2]);
            Assert.AreEqual(6, valueArray[3]);
        }

        #region Functions

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Abs() takes only 1 argument(s)")]
        public void AbsShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1, new Expression("Abs(-1)").Evaluate());
            Assert.AreEqual(12, new Expression("abs(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
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
            Assert.AreEqual(12, new Expression("asin(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Atan() takes only 1 argument(s)")]
        public void AtanShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Atan(0)").Evaluate());
            Assert.AreEqual(12, new Expression("atan(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Average() expects at least 1 argument(s)")]
        public void AverageShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(3d, new Expression("Average(1,2,4,5)").Evaluate());
            Assert.AreEqual(1d, new Expression("average(1)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
            Assert.AreEqual(12.5, new Expression("Average(10, 20, 5, 15)").Evaluate());

            new Expression("average()", ExpressiveOptions.IgnoreCaseForParsing).Evaluate();
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
            Assert.AreEqual(12, new Expression("ceiling(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
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
            Assert.AreEqual(4, new Expression("count(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

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
            Assert.AreEqual(12, new Expression("exp(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
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
            Assert.AreEqual(1, new Expression("in(0)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("In()", ExpressiveOptions.IgnoreCaseForParsing).Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "Length() takes only 1 argument(s)")]
        public void LengthShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(5, new Expression("Length('abcde')").Evaluate());

            new Expression("Length()", ExpressiveOptions.IgnoreCaseForParsing).Evaluate();
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
            Assert.AreEqual(1d, new Expression("mean(1)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
            Assert.AreEqual(12.5, new Expression("Mean(10, 20, 5, 15)").Evaluate());

            new Expression("mean()", ExpressiveOptions.IgnoreCaseForParsing).Evaluate();
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
            Assert.AreEqual(1.0M, new Expression("median(1)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
            Assert.AreEqual(12.5M, new Expression("Median(10, 20, 5, 15)").Evaluate());

            new Expression("median()", ExpressiveOptions.IgnoreCaseForParsing).Evaluate();
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
            Assert.AreEqual(1, new Expression("mode(1)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
            Assert.AreEqual(10, new Expression("Mode(10, 20, 5, 15)").Evaluate());

            new Expression("mode()", ExpressiveOptions.IgnoreCaseForParsing).Evaluate();
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
            Assert.AreEqual(1, new Expression("sum(1)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
            Assert.AreEqual(72, new Expression("sum(1,2,4,5,10,20,30)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

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

        #region Date Functions

        [TestMethod, ExpectedException(typeof(ExpressiveException), "AddDays() takes only 2 argument(s)")]
        public void AddDaysShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(new DateTime(2016, 01, 03), new Expression("AddDays(#2016-01-01#, 2)").Evaluate());
            Assert.AreEqual(new DateTime(2016, 01, 03), new Expression("addDays(#2016-01-01#, 2)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("AddDays()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "AddHours() takes only 2 argument(s)")]
        public void AddHoursShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(new DateTime(2016, 01, 01, 02, 00, 00), new Expression("AddHours(#2016-01-01 00:00:00#, 2)").Evaluate());
            Assert.AreEqual(new DateTime(2016, 01, 01, 02, 00, 00), new Expression("addhours(#2016-01-01 00:00:00#, 2)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("AddHours()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "AddMinutes() takes only 2 argument(s)")]
        public void AddMinutesShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(new DateTime(2016, 01, 01, 00, 02, 00), new Expression("AddMinutes(#2016-01-01 00:00:00#, 2)").Evaluate());
            Assert.AreEqual(new DateTime(2016, 01, 01, 00, 02, 00), new Expression("addminutes(#2016-01-01 00:00:00#, 2)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("AddMinutes()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "AddMonths() takes only 2 argument(s)")]
        public void AddMonthsShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(new DateTime(2016, 03, 01), new Expression("AddMonths(#2016-01-01#, 2)").Evaluate());
            Assert.AreEqual(new DateTime(2016, 03, 01), new Expression("addmonths(#2016-01-01#, 2)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("AddMonths()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "AddYears() takes only 2 argument(s)")]
        public void AddYearsShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(new DateTime(2018, 01, 01), new Expression("AddYears(#2016-01-01#, 2)").Evaluate());
            Assert.AreEqual(new DateTime(2018, 01, 01), new Expression("addyears(#2016-01-01#, 2)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("AddYears()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "DayOf() takes only 1 argument(s)")]
        public void DayOfShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1, new Expression("DayOf(#2016-01-01#)").Evaluate());
            Assert.AreEqual(12, new Expression("dayof(#2016-01-12#)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("DayOf()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "DaysBetween() takes only 2 argument(s)")]
        public void DaysBetweenShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual((new DateTime(2016, 01, 12) - new DateTime(2016, 01, 01)).TotalDays, new Expression("DaysBetween(#2016-01-01#, #2016-01-12#)").Evaluate());
            Assert.AreEqual((new DateTime(2016, 12, 01) - new DateTime(2016, 01, 01)).TotalDays, new Expression("daysbetween(#2016-01-01#, #2016-12-01#)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("DaysBetween()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "HourOf() takes only 1 argument(s)")]
        public void HourOfShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(2, new Expression("HourOf(#2016-01-01 02:00:00#)").Evaluate());
            Assert.AreEqual(2, new Expression("hourof(#2016-01-12 02:00:00#)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("HourOf()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "HoursBetween() takes only 2 argument(s)")]
        public void HoursBetweenShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual((new DateTime(2016, 01, 01, 23, 00, 00) - new DateTime(2016, 01, 01)).TotalHours, new Expression("HoursBetween(#2016-01-01#, #2016-01-01 23:00:00#)").Evaluate());
            Assert.AreEqual((new DateTime(2016, 12, 01, 23, 00, 00) - new DateTime(2016, 01, 01)).TotalHours, new Expression("hoursbetween(#2016-01-01#, #2016-12-01 23:00:00#)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("HoursBetween()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "MinuteOf() takes only 1 argument(s)")]
        public void MinuteOfShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(55, new Expression("MinuteOf(#2016-01-01 02:55:00#)").Evaluate());
            Assert.AreEqual(12, new Expression("minuteof(#2016-01-12 02:12:00#)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("MinuteOf()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "MinutesBetween() takes only 2 argument(s)")]
        public void MinutesBetweenShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual((new DateTime(2016, 01, 01, 23, 12, 00) - new DateTime(2016, 01, 01)).TotalMinutes, new Expression("MinutesBetween(#2016-01-01#, #2016-01-01 23:12:00#)").Evaluate());
            Assert.AreEqual((new DateTime(2016, 12, 01, 23, 32, 00) - new DateTime(2016, 01, 01)).TotalMinutes, new Expression("minutesbetween(#2016-01-01#, #2016-12-01 23:32:00#)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("MinutesBetween()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "MonthOf() takes only 1 argument(s)")]
        public void MonthOfShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(01, new Expression("MonthOf(#2016-01-01#)").Evaluate());
            Assert.AreEqual(06, new Expression("monthof(#2016-06-12#)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("MonthOf()").Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ExpressiveException), "YearOf() takes only 1 argument(s)")]
        public void YearOfShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(2016, new Expression("YearOf(#2016-01-01#)").Evaluate());
            Assert.AreEqual(2016, new Expression("yearof(#2016-01-12#)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            new Expression("YearOf()").Evaluate();
        }

        #endregion

        #region Conversion Functions

        [TestMethod]
        public void ShouldConvertToDate()
        {
            var arguments = new Dictionary<string, object>
            {
                ["myString"] = "2018-01-10",
            };

            var expression = new Expression("Date([myString])");
            Assert.AreEqual(new DateTime(2018, 01, 10), (DateTime)expression.Evaluate(arguments));

            expression = new Expression("Date([myString], 'yyyy-dd-MM')");
            Assert.AreEqual(new DateTime(2018, 10, 01), (DateTime)expression.Evaluate(arguments));
        }

        [TestMethod]
        public void ShouldConvertToDecimal()
        {
            var arguments = new Dictionary<string, object>
            {
                ["myLong"] = 5L,
                ["myInteger"] = 4,
                ["myDouble"] = 4.4d
            };

            var expression = new Expression("Decimal([myLong])");
            Assert.AreEqual(5M, (decimal)expression.Evaluate(arguments));

            expression = new Expression("Decimal([myInteger])");
            Assert.AreEqual(4M, (decimal)expression.Evaluate(arguments));

            expression = new Expression("Decimal([myDouble])");
            Assert.AreEqual(4.4M, (decimal)expression.Evaluate(arguments));
        }

        [TestMethod]
        public void ShouldConvertToDouble()
        {
            var arguments = new Dictionary<string, object>
            {
                ["myLong"] = 5L,
                ["myDecimal"] = 4.4M,
                ["myInteger"] = 4
            };

            var expression = new Expression("Double([myLong])");
            Assert.AreEqual(5d, (double)expression.Evaluate(arguments));

            expression = new Expression("Double([myDecimal])");
            Assert.AreEqual(4.4d, (double)expression.Evaluate(arguments));

            expression = new Expression("Double([myInteger])");
            Assert.AreEqual(4d, (double)expression.Evaluate(arguments));
        }

        [TestMethod]
        public void ShouldConvertToInteger()
        {
            var arguments = new Dictionary<string, object>
            {
                ["myLong"] = 5L,
                ["myDecimal"] = 4.4M,
                ["myDouble"] = 4.4d
            };

            var expression = new Expression("Integer([myLong])");
            Assert.AreEqual(5, (int)expression.Evaluate(arguments));

            expression = new Expression("Integer([myDecimal])");
            Assert.AreEqual(4, (int)expression.Evaluate(arguments));

            expression = new Expression("Integer([myDouble])");
            Assert.AreEqual(4, (int)expression.Evaluate(arguments));
        }

        [TestMethod]
        public void ShouldConvertToLong()
        {
            var arguments = new Dictionary<string, object>
            {
                ["myInteger"] = 5,
                ["myDecimal"] = 4.4M,
                ["myDouble"] = 4.4d
            };

            var expression = new Expression("Long([myInteger])");
            Assert.AreEqual(5L, (long)expression.Evaluate(arguments));

            expression = new Expression("Long([myDecimal])");
            Assert.AreEqual(4L, (long)expression.Evaluate(arguments));

            expression = new Expression("Long([myDouble])");
            Assert.AreEqual(4L, (long)expression.Evaluate(arguments));
        }

        [TestMethod]
        public void ShouldConvertToString()
        {
            var arguments = new Dictionary<string, object>
            {
                ["myDate"] = new DateTime(2018, 01, 10, 12, 34, 56),
                ["myDecimal"] = 4.4M
            };

            ExecuteUnderCulture("en-GB", () =>
                {
                    var dateExpression = new Expression("String([myDate])");
                    Assert.AreEqual("10/01/2018 12:34:56", (string)dateExpression.Evaluate(arguments));
                });

            var expression = new Expression("String([myDate], 'yyyy-MM-dd')");
            Assert.AreEqual("2018-01-10", (string)expression.Evaluate(arguments));

            expression = new Expression("String([myDecimal])");
            Assert.AreEqual("4.4", (string)expression.Evaluate(arguments));
        }

        #endregion

        #endregion

        #region General

        //[TestMethod]
        //public void TestAsync()
        //{
        //    Expression expression = new Expression("1+3");

        //    AutoResetEvent waitHandle = new AutoResetEvent(false);

        //    object result = null;

        //    expression.EvaluateAsync((m, r) =>
        //    {
        //        Assert.IsNull(m);
        //        result = r;
        //        waitHandle.Set();
        //    });

        //    waitHandle.WaitOne();
        //    Assert.AreEqual(4, result);
        //}

        //[TestMethod]
        //public void TestAsyncSafety()
        //{
        //    Expression expression = new Expression("1+3+[abc]");

        //    AutoResetEvent waitHandle = new AutoResetEvent(false);

        //    object result = null;

        //    expression.EvaluateAsync((m, r) =>
        //    {
        //        Assert.AreEqual(m, "The variable 'abc' has not been supplied.");
        //        result = r;
        //        waitHandle.Set();
        //    });

        //    waitHandle.WaitOne();
        //    Assert.IsNull(result);
        //}

        [TestMethod, ExpectedException(typeof(ExpressiveException), "There aren't enough ')' symbols. Expected 2 but there is only 1")]
        public void ShouldIdentifyParenthesisMismatch()
        {
            Expression expression = new Expression("(a + b) * (4 - 2");

            object value = expression.Evaluate(new Dictionary<string, object> { { "a", 2 }, { "b", 3 } });
        }

        // Expressions currently no longer short circuit at this level given the new feature of allowing aggregates in operations.
        //[TestMethod]
        //public void ShouldShortCircuitBooleanExpressions()
        //{
        //    var expression = new Expression("([a] != 0) && ([b]/[a]>2)");

        //    Assert.AreEqual(false, expression.Evaluate(new Dictionary<string, object> { { "a", 0 } }));
        //}

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
            Assert.AreEqual(13.5d, new Expression("18.0/2.0/2.0*3.0").Evaluate());
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
            new Expression("([a] + [b]) * (4 - 2)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(new Dictionary<string, object> { { "A", 2 }, { "b", 3 } });
            new Expression("IF(true, true, false)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate();

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

            Assert.AreEqual(3, expression.ReferencedVariables.Count);
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

        [TestMethod]
        public void ShouldMatchStringToBooleanTrue()
        {
            Assert.IsTrue((bool)new Expression("'True' && true").Evaluate());
            Assert.IsFalse((bool)new Expression("'False' && true").Evaluate());
        }

        [TestMethod]
        public void ShouldHandleCommaDecimalSeparatorForEuropeanCultures()
        {
            //Rule('a60f99fa-21ee-4dfa-b8c1-80ffaa3a83de', 10, 20)
            var invariantExpression = new Expression("If('a60f99fa-21ee-4dfa-b8c1-80ffaa3a83de' == 0, 10, 20)", ExpressiveOptions.None);
            Assert.AreEqual(20, invariantExpression.Evaluate());
        }

        [TestMethod]
        public void ShouldHandlePartiallyMatchingCustomFunctionNames()
        {
            var expression = new Expression("CountInstances()");

            expression.RegisterFunction("CountInstances", (p, a) =>
            {
                return 1;
            });
            expression.RegisterFunction("CountSealed", (p, a) =>
            {
                return 99;
            });

            Assert.AreEqual(1, (int)expression.Evaluate());
        }

        #endregion

        [TestMethod]
        public void ShouldHandleBugTwentyTwo()
        {
            // Not working
            //[myDouble]*5
            //[myDouble]* pow(5.5,2)
            //[myDouble]+5.5

            var arguments = new Dictionary<string, object>
            {
                ["myDouble"] = 4.0,
                ["myFloat"] = 4.0f
            };

            var expression = new Expression("[myDouble]*5");
            Assert.AreEqual(20, (double)expression.Evaluate(arguments));

            expression = new Expression("[myDouble]*Pow(5.5,2)");
            Assert.AreEqual(121, (double)expression.Evaluate(arguments));

            expression = new Expression("[myDouble]+5.5");
            Assert.AreEqual(9.5M, (decimal)expression.Evaluate(arguments));

            // Not working
            //[myDouble]*5.5
            expression = new Expression("[myDouble]*5.5");
            Assert.AreEqual(22M, (decimal)expression.Evaluate(arguments));

            expression = new Expression("[myDouble]-5.5");
            Assert.AreEqual(-1.5M, (decimal)expression.Evaluate(arguments));

            expression = new Expression("[myDouble]+5.5");
            Assert.AreEqual(9.5M, (decimal)expression.Evaluate(arguments));

            expression = new Expression("[myDouble]/2.0");
            Assert.AreEqual(2.0d, (double)expression.Evaluate(arguments));

            expression = new Expression("[myFloat]/2.0");
            Assert.AreEqual(2.0f, (float)expression.Evaluate(arguments));
        }

        [TestMethod]
        public void ShouldHandleBugTwentyThree()
        {
            var arguments = new Dictionary<string, object>
            {
                ["plate.datecontrol"] = null,
            };

            Assert.AreEqual("Date Needed", new Expression("if([plate.datecontrol] = NULL, 'Date Needed', 'Date Entered')", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(arguments));
        }

        [TestMethod]
        public void ShouldHandleBugTwentyFour()
        {
            var arguments = new Dictionary<string, object>
            {
                ["DischargeStatus1_Value"] = "11",
                ["DischargeStatus2_Value"] = "00",
            };

            Assert.AreEqual(true, new Expression("[DischargeStatus1_Value] > 00 AND[DischargeStatus2_Value] = 00", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(arguments));
        }

        [TestMethod]
        public void ShouldIgnoreCurrentCulture()
        {
            ExecuteUnderCulture("de-DE", () =>
                {
                    var invariantExpression = new Expression("1 + 2.34", ExpressiveOptions.None);
                    Assert.AreEqual(3.34M, invariantExpression.Evaluate());
                });
        }

        #region Actual unit tests for the Expression class to remain.

        [TestMethod]
        public void ShouldEvaluateT()
        {
            var expression = new Expression("1 + 2.0 + 3 * 4.0");

            Assert.AreEqual(15, expression.Evaluate<int>());
        }

        [Test]
        public void ShouldEvaluateWithVariableProvider()
        {
            var context = new Context(ExpressiveOptions.IgnoreCaseForParsing);
            var expression = new Expression("1+[a]", context);
            var variableProvider = new MockProvider();

            Assert.AreEqual(3, expression.Evaluate(variableProvider));
        }

        [TestMethod]
        public void ShouldRethrowOnInvalidEvaluateT()
        {
            var expression = new Expression("1 + 2.0 + 3 * 4.0");

            try
            {
                expression.Evaluate<DateTime>();
            }
            catch (ExpressiveException e)
            {
                if (e.InnerException is InvalidCastException)
                {
                    return;
                }
            }

            Assert.Fail("Invalid handling of exceptions.");
        }

        #endregion

        private void ExecuteUnderCulture(string localeCode, Action action)
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(localeCode);

                action.Invoke();
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = currentCulture;
            }
        }

        private class MockProvider : IVariableProvider
        {
            public bool TryGetValue(string variableName, out object value)
            {
                value = variableName == "a" ? (object)2 : null;
                return variableName == "a";
            }
        }
    }
}
