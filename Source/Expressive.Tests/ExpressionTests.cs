using Expressive.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Expressive.Tests
{
    [TestFixture]
    public class ExpressionTests
    {
        #region Operators

        #region Plus Operator

        [Test]
        public void SimpleIntegerAddition()
        {
            Expression expression = new Expression("1+3");

            object value = expression.Evaluate();

            Assert.That(value, Is.EqualTo(4));
        }

        [Test]
        public void SimpleDecimalAddition()
        {
            Expression expression = new Expression("1.3+3.5");

            object value = expression.Evaluate();

            Assert.That(value, Is.EqualTo(4.8M));
        }

        [Test]
        public void ShouldAddDoubleAndDecimal()
        {
            var expression = new Expression("1.8 + Abs([var1])");

            object value = expression.Evaluate(new Dictionary<string, object> { { "var1", 9.2 } });

            Assert.That(value, Is.EqualTo(11M));
        }

        [Test]
        public void ShouldConcatenateStrings()
        {
            var expression = new Expression("'1.8' + 'suffix'");

            object value = expression.Evaluate();

            Assert.That(value, Is.EqualTo("1.8suffix"));
        }

        [Test]
        public void ShouldHandleUnaryPlus()
        {
            var expression = new Expression("1.8++0.2");

            object value = expression.Evaluate();

            Assert.That(value, Is.EqualTo(2.0M));

            Assert.That(new Expression("+(1 * -2)").Evaluate(), Is.EqualTo(-2));
        }

        #endregion

        [Test]
        public void SimpleBodmas()
        {
            Expression expression = new Expression("1-3*2");

            object value = expression.Evaluate();

            Assert.That(value, Is.EqualTo(-5));
        }

        [Test]
        public void ScientificNotation()
        {
            var expression = new Expression("1.23e2");

            Assert.That(expression.Evaluate(), Is.EqualTo(123));
        }

        [Test]
        public void ScientificNotationWithLargeNumber()
        {
            var expression = new Expression("1.23e22");

            Assert.That(expression.Evaluate(), Is.EqualTo(12300000000000000000000M));
        }

        #region Subtract Operator

        [Test]
        public void SimpleIntegerSubtraction()
        {
            Expression expression = new Expression("3-1");

            object value = expression.Evaluate();

            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void SimpleDecimalSubtraction()
        {
            Expression expression = new Expression("3.5-1.2");

            object value = expression.Evaluate();

            Assert.That(value, Is.EqualTo(2.3M));
        }

        [Test]
        public void ShouldHandleUnarySubtraction()
        {
            var expression = new Expression("1.8--0.2");

            object value = expression.Evaluate();

            Assert.That(value, Is.EqualTo(2.0M));

            Assert.That(new Expression("-(1 * -2)").Evaluate(), Is.EqualTo(2));
        }

        #endregion

        [Test]
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

        [Test]
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

        [Test]
        public void NullCoalescingTests()
        {
            Assert.AreEqual(1, new Expression("null ?? 1").Evaluate());
            Assert.AreEqual(null, new Expression("null ?? null").Evaluate());

            Assert.AreEqual(54, new Expression("[empty] ?? 54").Evaluate(new Dictionary<string, object> { ["empty"] = null }));
        }

        #endregion

        [Test]
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

        [Test]
        public void AbsShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1, new Expression("Abs(-1)").Evaluate());
            Assert.That(
                () => new Expression("abs(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(),
                 Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void AcosShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Acos(1)").Evaluate());
            Assert.That(() => new Expression("Acos(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void AsinShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Asin(0)").Evaluate());
            Assert.That(
                () => new Expression("asin(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(),
                Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void AtanShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Atan(0)").Evaluate());
            Assert.That(() => new Expression("Atan(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void AverageShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(3d, new Expression("Average(1,2,4,5)").Evaluate());
            Assert.AreEqual(1d, new Expression("average(1)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
            Assert.AreEqual(12.5, new Expression("Average(10, 20, 5, 15)").Evaluate());

            Assert.That(
                () => new Expression("average()", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(),
                 Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void AverageShouldHandleIEnumerable()
        {
            Assert.AreEqual(3d, new Expression("Average(1,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 5 } }));
        }

        [Test]
        public void CeilingShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(2M, new Expression("Ceiling(1.5)").Evaluate());
            Assert.That(
                () => new Expression("ceiling(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(),
                 Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void CosShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1d, new Expression("Cos(0)").Evaluate());
            Assert.That(() => new Expression("Cos(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void CountShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(1, new Expression("Count(0)").Evaluate());
            Assert.AreEqual(4, new Expression("count(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            Assert.That(() => new Expression("Count()").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void CountShouldHandleIEnumerable()
        {
            Assert.AreEqual(5, new Expression("Count([array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 0, 1, 2, 3, 4 } }));
            Assert.AreEqual(5, new Expression("Count([array])").Evaluate(new Dictionary<string, object> { ["array"] = new List<int> { 0, 1, 2, 3, 4 } }));
        }

        [Test]
        public void ExpShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1d, new Expression("Exp(0)").Evaluate());
            Assert.That(
                () => new Expression("exp(1,2,4,5)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(),
                 Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void FloorShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1d, new Expression("Floor(1.5)").Evaluate());
            Assert.That(() => new Expression("Floor(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void IEEERemainderShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(-1d, new Expression("IEEERemainder(3, 2)").Evaluate());

            Assert.That(() => new Expression("IEEERemainder(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void IfShouldHandleOnlyThreeArguments()
        {
            Assert.AreEqual("Low risk", new Expression("If(1 > 8, 'High risk', 'Low risk')").Evaluate());
            Assert.AreEqual("Low risk", new Expression("If(1 > 8, 1 / 0, 'Low risk')").Evaluate());

            Assert.That(() => new Expression("If(1 > 9, 2, 4, 5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void InShouldHandleAtLeastTwoArguments()
        {
            Assert.AreEqual(false, new Expression("In('abc','def','ghi','jkl')").Evaluate());

            Assert.That(
                () => new Expression("in(0)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(),
                Throws.InstanceOf<ExpressiveException>());

            Assert.That(() => new Expression("In()").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void LengthShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(5, new Expression("Length('abcde')").Evaluate());

            Assert.That(() => new Expression("Length()").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void LengthShouldHandleNotJustStrings()
        {
            Assert.AreEqual(3, new Expression("Length(123)").Evaluate());
            Assert.AreEqual(null, new Expression("Length(null)").Evaluate());
        }

        [Test]
        public void LogShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(0d, new Expression("Log(1, 10)").Evaluate());
            Assert.That(() => new Expression("Log(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void Log10ShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Log10(1)").Evaluate());
            Assert.That(() => new Expression("Log10(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void MaxShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(3, new Expression("Max(3, 2)").Evaluate());
            Assert.That(() => new Expression("Max()").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void MaxShouldHandleIEnumerable()
        {
            Assert.AreEqual(50, new Expression("Max(1,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 50 } }));
        }

        [Test]
        public void MaxShouldIgnoreNull()
        {
            Assert.AreEqual(null, new Expression("Max(null,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 50 } }));
        }

        [Test]
        public void MeanShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(3d, new Expression("Mean(1,2,4,5)").Evaluate());
            Assert.AreEqual(1d, new Expression("mean(1)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
            Assert.AreEqual(12.5, new Expression("Mean(10, 20, 5, 15)").Evaluate());

            Assert.That(
                () => new Expression("mean()", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(),
                Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void MeanShouldHandleIEnumerable()
        {
            Assert.AreEqual(3d, new Expression("Mean(1,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 5 } }));
        }

        [Test]
        public void MedianShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(3.0M, new Expression("Median(1,2,4,5)").Evaluate());
            Assert.AreEqual(1.0M, new Expression("median(1)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
            Assert.AreEqual(12.5M, new Expression("Median(10, 20, 5, 15)").Evaluate());

            Assert.That(
                () => new Expression("median()", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(),
                Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void MinShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(2, new Expression("Min(3, 2)").Evaluate());
            Assert.That(() => new Expression("Min()").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void MinShouldHandleIEnumerable()
        {
            Assert.AreEqual(1, new Expression("Min(1,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 50 } }));
        }

        [Test]
        public void MinShouldIgnoreNull()
        {
            Assert.AreEqual(null, new Expression("Min(null,2,4,5,[array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 1, 2, 4, 50 } }));
        }

        [Test]
        public void ModeShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(2, new Expression("Mode(1,2,4,5,2)").Evaluate());
            Assert.AreEqual(1, new Expression("mode(1)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
            Assert.AreEqual(10, new Expression("Mode(10, 20, 5, 15)").Evaluate());

            Assert.That(
                () => new Expression("mode()", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(),
                Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void PadLeftShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual("0000abc", new Expression("PadLeft('abc', 7, '0')").Evaluate());
            Assert.AreEqual("abcdefghi", new Expression("PadLeft('abcdefghi', 7, '0')").Evaluate());
            Assert.AreEqual(null, new Expression("PadLeft(null, 7, '0')").Evaluate());
            Assert.AreEqual(null, new Expression("PadLeft('abcdefghi', null, '0')").Evaluate());
            Assert.AreEqual("   abcd", new Expression("PadLeft('abcd', 7, null)").Evaluate());

            Assert.That(() => new Expression("PadLeft(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void PadRightShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual("abc0000", new Expression("PadRight('abc', 7, '0')").Evaluate());
            Assert.AreEqual("abcdefghi", new Expression("PadRight('abcdefghi', 7, '0')").Evaluate());
            Assert.AreEqual(null, new Expression("PadRight(null, 7, '0')").Evaluate());
            Assert.AreEqual(null, new Expression("PadRight('abcdefghi', null, '0')").Evaluate());
            Assert.AreEqual("abcd   ", new Expression("PadRight('abcd', 7, null)").Evaluate());

            Assert.That(() => new Expression("PadRight(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void PowShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(9d, new Expression("Pow(3, 2)").Evaluate());

            Assert.That(() => new Expression("Pow(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void RegexShouldHandleOnlyTwoArguments()
        {
            Expression expression = new Expression(@"Regex('text', '^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$')");
            Assert.AreEqual(false, expression.Evaluate());

            expression = new Expression(@"Regex('text', '^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$', '')");
            Assert.That(() => expression.Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void RoundShouldHandleOnlyTwoArguments()
        {
            Assert.AreEqual(3.22d, new Expression("Round(3.222222, 2)").Evaluate());

            Assert.That(() => new Expression("Round(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void SignShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(-1, new Expression("Sign(-10)").Evaluate());

            Assert.That(() => new Expression("Sign(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void SinShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Sin(0)").Evaluate());

            Assert.That(() => new Expression("Sin(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void SqrtShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(5d, new Expression("Sqrt(25)").Evaluate());

            Assert.That(() => new Expression("Sqrt(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void SumShouldHandleAtLeastOneArgument()
        {
            Assert.AreEqual(12, new Expression("Sum(1,2,4,5)").Evaluate());
            Assert.AreEqual(1, new Expression("sum(1)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());
            Assert.AreEqual(72, new Expression("sum(1,2,4,5,10,20,30)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate());

            Assert.That(() => new Expression("Sum()").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void SumShouldHandleIEnumerable()
        {
            Assert.AreEqual(10, new Expression("Sum([array])").Evaluate(new Dictionary<string, object> { ["array"] = new[] { 0, 1, 2, 3, 4 } }));
        }

        [Test]
        public void SumShouldHandleIEnumerableWithNulls()
        {
            var arguments = new Dictionary<string, object>
            {
                ["Value"] = new List<decimal?> { 1.0m, null, 2.5m },
                ["Value1"] = 3m
            };
            var expression = new Expressive.Expression("Sum([Value], [Value1])");

            var result = expression.Evaluate(arguments);

            Assert.That(result, Is.InstanceOf(typeof(decimal?)));
            Assert.AreEqual(6.5m, result);
        }

        [Test]
        public void SumShouldUseZeroInsteadOfNull()
        {
            var arguments = new Dictionary<string, object>
            {
                ["Value1"] = 1.1m,
                ["Value2"] = null
            };
            var expression = new Expressive.Expression("Sum([Value1], [Value2])");

            var result = expression.Evaluate(arguments);

            Assert.That(result, Is.InstanceOf(typeof(decimal?)));
            Assert.AreEqual(1.1m, result);
        }

        [Test]
        public void TanShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(0d, new Expression("Tan(0)").Evaluate());

            Assert.That(() => new Expression("Tan(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void TruncateShouldHandleOnlyOneArgument()
        {
            Assert.AreEqual(1d, new Expression("Truncate(1.7)").Evaluate());

            Assert.That(() => new Expression("Truncate(1,2,4,5)").Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void CustomFunctionsWithLambda()
        {
            var exp = new Expression("myfunc('abc')");
            exp.RegisterFunction("myfunc", (p, a) =>
            {
                return 1;
            });

            Assert.AreEqual(1, exp.Evaluate());
        }

        #region Conversion Functions

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        //[Test]
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

        //[Test]
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

        [Test]
        public void ShouldIdentifyParenthesisMismatch()
        {
            Expression expression = new Expression("(a + b) * (4 - 2");

            Assert.That(
                () => expression.Evaluate(new Dictionary<string, object> { { "a", 2 }, { "b", 3 } }),
                Throws.InstanceOf<ExpressiveException>());
        }

        // Expressions currently no longer short circuit at this level given the new feature of allowing aggregates in operations.
        //[Test]
        //public void ShouldShortCircuitBooleanExpressions()
        //{
        //    var expression = new Expression("([a] != 0) && ([b]/[a]>2)");

        //    Assert.AreEqual(false, expression.Evaluate(new Dictionary<string, object> { { "a", 0 } }));
        //}

        [Test]
        public void ShouldCompareDates()
        {
            Assert.AreEqual(true, new Expression("#1/1/2009#==#1/1/2009#").Evaluate());
            Assert.AreEqual(false, new Expression("#2/1/2009#==#1/1/2009#").Evaluate());
        }

        [Test]
        public void ShouldEvaluateSubExpressions()
        {
            var volume = new Expression("[surface] * [h]");
            var surface = new Expression("[l] * [K]");

            Assert.AreEqual(6, volume.Evaluate(new Dictionary<string, object> { { "surface", surface }, { "h", 3 }, { "l", 1 }, { "K", 2 } }));
        }

        [Test]
        public void ShouldParseValues()
        {
            Assert.AreEqual(123456, new Expression("123456").Evaluate());
            Assert.AreEqual(new DateTime(2001, 01, 01), new Expression("#01/01/2001#").Evaluate());
            Assert.AreEqual(123.456M, new Expression("123.456").Evaluate());
            Assert.AreEqual(true, new Expression("true").Evaluate());
            Assert.AreEqual("true", new Expression("'true'").Evaluate());
            Assert.AreEqual("qwerty", new Expression("'qwerty'").Evaluate());
        }

        [Test]
        public void ShouldEscapeCharacters()
        {
            Assert.AreEqual("'hello'", new Expression(@"'\'hello\''").Evaluate());
            Assert.AreEqual(" ' hel lo ' ", new Expression(@"' \' hel lo \' '").Evaluate());
            System.Diagnostics.Debug.WriteLine("hel\nlo");
            System.Diagnostics.Debug.WriteLine(new Expression(@"'hel\nlo'").Evaluate());
            Assert.AreEqual("hel\nlo", new Expression(@"'hel\nlo'").Evaluate());
        }

        [Test]
        public void ShouldHandleOperatorsPriority()
        {
            Assert.AreEqual(8, new Expression("2+2+2+2").Evaluate());
            Assert.AreEqual(16, new Expression("2*2*2*2").Evaluate());
            Assert.AreEqual(6, new Expression("2*2+2").Evaluate());
            Assert.AreEqual(6, new Expression("2+2*2").Evaluate());

            Assert.AreEqual(9d, new Expression("1 + 2 + 3 * 4 / 2").Evaluate());
            Assert.AreEqual(13.5d, new Expression("18.0/2.0/2.0*3.0").Evaluate());
        }

        [Test]
        public void ShouldNotLosePrecision()
        {
            Assert.AreEqual(0.5, new Expression("3/6").Evaluate());
        }

        [Test]
        public void ShouldFailOnUnrecognisedToken()
        {
            Assert.That(
                () => new Expression("1 + blarsh + 4").Evaluate(),
                Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void ShouldHandleCaseSensitivity()
        {
            new Expression("([a] + [b]) * (4 - 2)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(new Dictionary<string, object> { { "A", 2 }, { "b", 3 } });
            new Expression("IF(true, true, false)", ExpressiveOptions.IgnoreCaseForParsing).Evaluate();

            Assert.That(() => new Expression("IF(true, true, false)").Evaluate(), Throws.InstanceOf<ExpressiveException>());

            Expression expression = new Expression("([a] + [b]) * (4 - 2)");

            Assert.That(
                () => expression.Evaluate(new Dictionary<string, object> { { "A", 2 }, { "b", 3 } }),
                Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
        public void ShouldReturnCorrectVariables()
        {
            var expression = new Expression("([a] + [b] * [c]) + ([a] * [b])");

            Assert.AreEqual(3, expression.ReferencedVariables.Count);
        }

        [Test]
        public void CheckNullValuesAreHandledCorrectly()
        {
            Assert.IsNull(new Expression("2 + [a]").Evaluate(new Dictionary<string, object> { ["a"] = null }));
            Assert.IsNull(new Expression("2 * [a]").Evaluate(new Dictionary<string, object> { ["a"] = null }));
            Assert.IsNull(new Expression("2 / [a]").Evaluate(new Dictionary<string, object> { ["a"] = null }));
            Assert.IsNull(new Expression("2 - [a]").Evaluate(new Dictionary<string, object> { ["a"] = null }));
            Assert.IsNull(new Expression("2 % [a]").Evaluate(new Dictionary<string, object> { ["a"] = null }));
        }

        [Test]
        public void CheckNaNIsHandledCorrectly()
        {
            Assert.AreEqual(double.NaN, new Expression("2 + [a]").Evaluate(new Dictionary<string, object> { ["a"] = double.NaN }));
            Assert.AreEqual(double.NaN, new Expression("2 * [a]").Evaluate(new Dictionary<string, object> { ["a"] = double.NaN }));
            Assert.AreEqual(double.NaN, new Expression("2 / [a]").Evaluate(new Dictionary<string, object> { ["a"] = double.NaN }));
            Assert.AreEqual(double.NaN, new Expression("2 - [a]").Evaluate(new Dictionary<string, object> { ["a"] = double.NaN }));
            Assert.AreEqual(double.NaN, new Expression("2 % [a]").Evaluate(new Dictionary<string, object> { ["a"] = double.NaN }));
        }

        [Test]
        public void CheckComplicatedDepth()
        {
            // This was a previous bug (Issue #6) so this is in place to make sure it does not re-occur.
            var expression = new Expression("((1 + 2) * 3) + (([d] / [e]) * [f]) - ([a] * [b])");

            object value = expression.Evaluate(new Dictionary<string, object> { ["a"] = 1, ["b"] = 2, ["c"] = 3, ["d"] = 6, ["e"] = 2, ["f"] = 6 });

            Assert.AreEqual(25d, value);
        }

        [TestCase("<= #today#")]
        [TestCase("#today# <=")]
        public void ShouldThrowExceptionIfEitherSideIsMissing(string expression)
        {
            Assert.That(() => new Expression(expression).Evaluate(), Throws.InstanceOf<ExpressiveException>());
        }

        [Test]
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

        [Test]
        public void ShouldMatchStringToBooleanTrue()
        {
            Assert.IsTrue((bool)new Expression("'True' && true").Evaluate());
            Assert.IsFalse((bool)new Expression("'False' && true").Evaluate());
        }

        [Test]
        public void ShouldHandleCommaDecimalSeparatorForEuropeanCultures()
        {
            //Rule('a60f99fa-21ee-4dfa-b8c1-80ffaa3a83de', 10, 20)
            var invariantExpression = new Expression("If('a60f99fa-21ee-4dfa-b8c1-80ffaa3a83de' == 0, 10, 20)", ExpressiveOptions.None);
            Assert.AreEqual(20, invariantExpression.Evaluate());
        }

        [Test]
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

        [Test]
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

        [Test]
        public void ShouldHandleBugTwentyThree()
        {
            var arguments = new Dictionary<string, object>
            {
                ["plate.datecontrol"] = null,
            };

            Assert.AreEqual("Date Needed", new Expression("if([plate.datecontrol] = NULL, 'Date Needed', 'Date Entered')", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(arguments));
        }

        [Test]
        public void ShouldHandleBugTwentyFour()
        {
            var arguments = new Dictionary<string, object>
            {
                ["DischargeStatus1_Value"] = "11",
                ["DischargeStatus2_Value"] = "00",
            };

            Assert.AreEqual(true, new Expression("[DischargeStatus1_Value] > 00 AND[DischargeStatus2_Value] = 00", ExpressiveOptions.IgnoreCaseForParsing).Evaluate(arguments));
        }

        [Test]
        public void ShouldIgnoreCurrentCulture()
        {
            ExecuteUnderCulture("de-DE", () =>
                {
                    var invariantExpression = new Expression("1 + 2.34", ExpressiveOptions.None);
                    Assert.AreEqual(3.34M, invariantExpression.Evaluate());
                });
        }

        #region Actual unit tests for the Expression class to remain.

        [Test]
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

        [TestCaseSource(nameof(DictionaryEnumerationSource))]
        public static void ShouldNotEnumerateDictionaryWithAppropriateComparer(DictionaryEnumerationTestData sourceData)
        {
#pragma warning disable CA1062 // Validate arguments of public methods - this really isn't going to be null
            var context = new Context(sourceData.Options);
#pragma warning restore CA1062 // Validate arguments of public methods
            var expression = new Expression("1+2", context);
            var dictionary = new MockDictionary(sourceData.Comparer);

            NUnit.Framework.Assert.That(
                () => expression.Evaluate(dictionary),
                sourceData.ExpectedEnumeration ? (Constraint)Throws.InstanceOf<ExpressiveException>() : Throws.Nothing);
        }

        private static IEnumerable<DictionaryEnumerationTestData> DictionaryEnumerationSource
        {
            get
            {
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.IgnoreCaseForParsing, EqualityComparer<string>.Default, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.IgnoreCaseForParsing, StringComparer.CurrentCulture, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.IgnoreCaseForParsing, StringComparer.CurrentCultureIgnoreCase, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.IgnoreCaseForParsing, StringComparer.InvariantCulture, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.IgnoreCaseForParsing, StringComparer.InvariantCultureIgnoreCase, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.IgnoreCaseForParsing, StringComparer.Ordinal, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.IgnoreCaseForParsing, StringComparer.OrdinalIgnoreCase, false);

                yield return new DictionaryEnumerationTestData(ExpressiveOptions.None, EqualityComparer<string>.Default, false);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.None, StringComparer.CurrentCulture, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.None, StringComparer.CurrentCultureIgnoreCase, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.None, StringComparer.InvariantCulture, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.None, StringComparer.InvariantCultureIgnoreCase, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.None, StringComparer.Ordinal, true);
                yield return new DictionaryEnumerationTestData(ExpressiveOptions.None, StringComparer.OrdinalIgnoreCase, true);
            }
        }

        [Test]
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

            Assert.Fail();
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

        private class MockDictionary : Dictionary<string, object>, IDictionary<string, object>
        {
            public MockDictionary(IEqualityComparer<string> comparer) : base(comparer)
            {

            }

            public new int Count => throw new InvalidOperationException();
        }
    }
}
