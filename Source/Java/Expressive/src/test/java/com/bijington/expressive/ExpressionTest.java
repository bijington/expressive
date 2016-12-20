package com.bijington.expressive;

import com.bijington.expressive.exceptions.ExpressiveException;
import com.bijington.expressive.exceptions.ParameterCountMismatchException;
import com.bijington.expressive.exceptions.UnrecognisedTokenException;
import com.bijington.expressive.helpers.Convert;
import com.sun.org.apache.xalan.internal.extensions.ExpressionContext;
import junit.framework.TestCase;
import org.junit.Assert;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by shaun on 30/06/2016.
 */
public class ExpressionTest extends TestCase {
    @org.junit.Test
    public void testSimpleIntegerAddition() throws Exception {
        Expression expression = new Expression("1+3");

        Assert.assertEquals(4, expression.evaluate());
    }

    @org.junit.Test
    public void testSimpleDoubleAddition() throws Exception {
        Expression expression = new Expression("1.3+3.5");

        Assert.assertEquals(4.8, expression.evaluate());
    }

    @org.junit.Test
    public void testShouldAddDoubleAndFloat() throws Exception {
        Expression expression = new Expression("1.8 + [var1]");

        Map<String, Object> variables = new HashMap<>();
        variables.put("var1", new Float(9.2));

        Assert.assertEquals(1.8 + new Float(9.2), expression.evaluate(variables));
    }

    @org.junit.Test
    public void testShouldConcatenateStrings() throws Exception {
        Expression expression = new Expression("'1.8' + 'suffix'");

        Assert.assertEquals("1.8suffix", expression.evaluate());
    }

    @org.junit.Test
    public void testSimpleBodmas() throws Exception {
        Expression expression = new Expression("1-3*2");

        Assert.assertEquals(-5, expression.evaluate());
    }

    @org.junit.Test
    public void testSimpleIntegerSubtraction() throws Exception {
        Expression expression = new Expression("3-1");

        Assert.assertEquals(2, expression.evaluate());
    }

    @org.junit.Test
    public void testSimpleDoubleSubtraction() throws Exception {
        Expression expression = new Expression("3.5-1.2");

        Assert.assertEquals(2.3, expression.evaluate());
    }

    @org.junit.Test
    public void testShouldHandleUnarySubtraction() throws Exception {
        Expression expression = new Expression("1.8--0.2");

        Assert.assertEquals(2.0, expression.evaluate());
    }

    @org.junit.Test
    public void testLogicOperators() throws Exception {
        Assert.assertEquals(true, new Expression("1 && 1").evaluate());
        Assert.assertEquals(false, new Expression("false and true").evaluate());
        Assert.assertEquals(false, new Expression("not true").evaluate());
        Assert.assertEquals(true, new Expression("!false").evaluate());
        Assert.assertEquals(true, new Expression("false || 1").evaluate());
        Assert.assertEquals(false, new Expression("false || !(true && true)").evaluate());
    }

    @org.junit.Test
    public void testRelationalOperators() throws Exception {
        Assert.assertEquals(true, new Expression("1 == 1").evaluate());
        Assert.assertEquals(false, new Expression("1 != 1").evaluate());
        Assert.assertEquals(true, new Expression("1 <> 2").evaluate());
        Assert.assertEquals(true, new Expression("7 >= 2").evaluate());
        Assert.assertEquals(false, new Expression("1 >= 2").evaluate());
        Assert.assertEquals(true, new Expression("7 > 2").evaluate());
        Assert.assertEquals(false, new Expression("2 > 2").evaluate());
        Assert.assertEquals(false, new Expression("7 <= 2").evaluate());
        Assert.assertEquals(true, new Expression("1 <= 2").evaluate());
        Assert.assertEquals(false, new Expression("7 < 2").evaluate());
        Assert.assertEquals(true, new Expression("1 < 2").evaluate());

        Map<String, Object> variables = new HashMap<>();
        variables.put("number1", null);

        Map<String, Object> variablesWithValue = new HashMap<>();
        variablesWithValue.put("number1", 2);

        // Null safety
        Assert.assertEquals(true, new Expression("[number1] == null").evaluate(variables));
        Assert.assertEquals(false, new Expression("[number1] == null").evaluate(variablesWithValue));
        Assert.assertEquals(false, new Expression("[number1] != null").evaluate(variables));
        Assert.assertEquals(true, new Expression("[number1] != null").evaluate(variablesWithValue));
        Assert.assertEquals(true, new Expression("[number1] <> 2").evaluate(variables));
        Assert.assertEquals(null, new Expression("[number1] >= 2").evaluate(variables));
        Assert.assertEquals(null, new Expression("[number1] > 2").evaluate(variables));
        Assert.assertEquals(null, new Expression("[number1] <= 2").evaluate(variables));
        Assert.assertEquals(null, new Expression("[number1] < 2").evaluate(variables));

        Assert.assertEquals(true, new Expression("null == [number1]").evaluate(variables));
        Assert.assertEquals(false, new Expression("null == [number1]").evaluate(variablesWithValue));
        Assert.assertEquals(false, new Expression("null != [number1]").evaluate(variables));
        Assert.assertEquals(true, new Expression("null != [number1]").evaluate(variablesWithValue));
        Assert.assertEquals(true, new Expression("2 <> [number1]").evaluate(variables));
        Assert.assertEquals(null, new Expression("2 >= [number1]").evaluate(variables));
        Assert.assertEquals(null, new Expression("2 > [number1]").evaluate(variables));
        Assert.assertEquals(null, new Expression("2 <= [number1]").evaluate(variables));
        Assert.assertEquals(null, new Expression("2 < [number1]").evaluate(variables));
    }

    @org.junit.Test
    public void testNullCoalescing() throws Exception {
        Assert.assertEquals(1, new Expression("null ?? 1").evaluate());
        Assert.assertEquals(null, new Expression("null ?? null").evaluate());

        Map<String, Object> variables = new HashMap<>();
        variables.put("empty", null);
        Assert.assertEquals(54, new Expression("[empty] ?? 54").evaluate(variables));
    }

    @org.junit.Test
    public void testAbsShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(1, new Expression("Abs(-1)").evaluate());

        try {
            Assert.assertEquals(12, new Expression("abs(1,2,4,5)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Abs() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testAcosShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(0d, new Expression("Acos(1)").evaluate());

        try {
            Assert.assertEquals(12, new Expression("Acos(1,2,4,5)").evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Acos() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testAsinShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(1.5707963267948966, new Expression("Asin(1)").evaluate());

        try {
            Assert.assertEquals(12, new Expression("Asin(1,2,4,5)").evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Asin() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testAtanShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(0d, new Expression("Atan(0)").evaluate());

        try {
            Assert.assertEquals(12, new Expression("Atan(1,2,4,5)").evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Atan() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testAverageShouldHandleAtLeastOneArgument() throws Exception {
        Assert.assertEquals(3d, new Expression("Average(1,2,4,5)").evaluate());
        Assert.assertEquals(1d, new Expression("average(1)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        Assert.assertEquals(12.5, new Expression("Average(10, 20, 5, 15)").evaluate());

        try {
            new Expression("average()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate();
        }
        catch (ExpressiveException ute) {
            Assert.assertEquals("Mean() expects at least 1 argument(s)", ute.getMessage());
        }
    }

    @org.junit.Test
    public void testAverageShouldHandleAggregates() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("array", new int[] { 1, 2, 4, 5 });

        Assert.assertEquals(3d, new Expression("Average(1,2,4,5,[array])").evaluate(variables));
    }

    @org.junit.Test
    public void testCeilingShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(2d, new Expression("Ceiling(1.5)").evaluate());
        try {
            Assert.assertEquals(12, new Expression("ceiling(1,2,4,5)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Ceiling() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testCosShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(1d, new Expression("Cos(0)").evaluate());

        try {
            Assert.assertEquals(12, new Expression("Cos(1,2,4,5)").evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Cos() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testCountShouldHandleAtLeastOneArgument() throws Exception {
        Assert.assertEquals(1, new Expression("Count(0)").evaluate());
        Assert.assertEquals(4, new Expression("count(1,2,4,5)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        Assert.assertEquals(12.5, new Expression("Average(10, 20, 5, 15)").evaluate());

        try {
            new Expression("count()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate();
        }
        catch (ExpressiveException ute) {
            Assert.assertEquals("Count() expects at least 1 argument(s)", ute.getMessage());
        }
    }

    @org.junit.Test
    public void testACountShouldHandleAggregates() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("array", new int[] { 0, 1, 2, 3, 4 });

        Assert.assertEquals(5, new Expression("Count([array])").evaluate(variables));
    }

    @org.junit.Test
    public void testExpShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(1d, new Expression("Exp(0)").evaluate());
        try {
            Assert.assertEquals(12, new Expression("exp(1,2,4,5)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Exp() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testFloorShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(1d, new Expression("Floor(1.5)").evaluate());
        try {
            Assert.assertEquals(12, new Expression("floor(1,2,4,5)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Floor() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testIEEERemainderShouldHandleOnlyTwoArguments() throws Exception {
        Assert.assertEquals(-1d, new Expression("IEEERemainder(3, 2)").evaluate());
        try {
            Assert.assertEquals(12, new Expression("IeEeRemainder(1,2,4,5)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("IEEERemainder() takes only 2 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testIfShouldHandleOnlyThreeArguments() throws Exception {
        Assert.assertEquals("Low risk", new Expression("If(1 > 8, 'High risk', 'Low risk')").evaluate());
        Assert.assertEquals("Low risk", new Expression("If(1 > 8, 1 / 0, 'Low risk')").evaluate());

        try {
            new Expression("If(1 > 9, 2, 4, 5)").evaluate();
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("If() takes only 3 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testInShouldHandleAtLeastTwoArguments() throws Exception {
        Assert.assertEquals(false, new Expression("In('abc','def','ghi','jkl')").evaluate());
        Assert.assertEquals(true, new Expression("in(0, 0, 1, 2)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());

        try {
            new Expression("In()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate();
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("In() expects at least 2 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testLengthShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(5, new Expression("Length('abced')").evaluate());
        try {
            Assert.assertEquals(12, new Expression("length(1,2,4,5)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Length() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testLengthShouldHandleNotJustStrings() throws Exception {
        Assert.assertEquals(3, new Expression("Length(123)").evaluate());
        Assert.assertEquals(null, new Expression("Length(null)").evaluate());
    }

    @org.junit.Test
    public void testLog10ShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(0d, new Expression("Log10(1)").evaluate());
        try {
            Assert.assertEquals(12, new Expression("log10(1,2,4,5)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Log10() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testLogShouldHandleOnlyTwoArguments() throws Exception {
        Assert.assertEquals(0d, new Expression("Log(1, 10)").evaluate());

        try {
            Assert.assertEquals(12, new Expression("Log(1,2,3,4,5)").evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Log() takes only 2 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testMaxShouldHandleAtLeastOneArgument() throws Exception {
        Assert.assertEquals(3, new Expression("Max(3,2)").evaluate());

        try {
            new Expression("max()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate();
        }
        catch (ExpressiveException ute) {
            Assert.assertEquals("Max() expects at least 1 argument(s)", ute.getMessage());
        }
    }

    @org.junit.Test
    public void testMaxShouldHandleAggregates() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("array", new int[] { 1, 2, 4, 50 });

        Assert.assertEquals(50, new Expression("Max(1,2,4,5,[array])").evaluate(variables));
    }

    @org.junit.Test
    public void testMeanShouldHandleAtLeastOneArgument() throws Exception {
        Assert.assertEquals(3d, new Expression("Mean(1,2,4,5)").evaluate());
        Assert.assertEquals(1d, new Expression("mean(1)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        Assert.assertEquals(12.5, new Expression("Mean(10, 20, 5, 15)").evaluate());

        try {
            new Expression("mean()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate();
        }
        catch (ExpressiveException ute) {
            Assert.assertEquals("Mean() expects at least 1 argument(s)", ute.getMessage());
        }
    }

    @org.junit.Test
    public void testMeanShouldHandleAggregates() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("array", new int[] { 1, 2, 4, 5 });

        Assert.assertEquals(3d, new Expression("Mean(1,2,4,5,[array])").evaluate(variables));
    }

    @org.junit.Test
    public void testMedianShouldHandleAtLeastOneArgument() throws Exception {
        Assert.assertEquals(3d, new Expression("Median(1,2,4,5)").evaluate());
        Assert.assertEquals(1d, new Expression("median(1)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        Assert.assertEquals(12.5, new Expression("Median(10, 20, 5, 15)").evaluate());

        try {
            new Expression("median()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate();
        }
        catch (ExpressiveException ute) {
            Assert.assertEquals("Median() expects at least 1 argument(s)", ute.getMessage());
        }
    }

    @org.junit.Test
    public void testMinShouldHandleAtLeastOneArgument() throws Exception {
        Assert.assertEquals(2, new Expression("Min(3,2)").evaluate());

        try {
            new Expression("min()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate();
        }
        catch (ExpressiveException ute) {
            Assert.assertEquals("Min() expects at least 1 argument(s)", ute.getMessage());
        }
    }

    @org.junit.Test
    public void testMinShouldHandleAggregates() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("array", new int[] { 1, 2, 4, 50 });

        Assert.assertEquals(1, new Expression("Min(1,2,4,5,[array])").evaluate(variables));
    }

    @org.junit.Test
    public void testModeShouldHandleAtLeastOneArgument() throws Exception {
        Assert.assertEquals(2d, new Expression("Mode(1,2,4,5,2)").evaluate());
        Assert.assertEquals(1d, new Expression("mode(1)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        Assert.assertEquals(10d, new Expression("Mode(10, 20, 5, 15)").evaluate());

        try {
            new Expression("mode()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate();
        }
        catch (ExpressiveException ute) {
            Assert.assertEquals("Mode() expects at least 1 argument(s)", ute.getMessage());
        }
    }

    @org.junit.Test
    public void testPadLeftShouldHandleOnlyThreeArguments() throws Exception {
        Assert.assertEquals("0000abc", new Expression("PadLeft('abc', 7, '0')").evaluate());
        Assert.assertEquals("abcdefghi", new Expression("PadLeft('abcdefghi', 7, '0')").evaluate());
        Assert.assertEquals(null, new Expression("PadLeft(null, 7, '0')").evaluate());
        Assert.assertEquals(null, new Expression("PadLeft('abcdefghi', null, '0')").evaluate());
        Assert.assertEquals("   abcd", new Expression("PadLeft('abcd', 7, null)").evaluate());

        try {
            new Expression("PadLeft(1,2,4,5)").evaluate();
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("PadLeft() takes only 3 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testPowShouldHandleOnlyTwoArguments() throws Exception {
        Assert.assertEquals(9d, new Expression("Pow(3,2)").evaluate());

        try {
            new Expression("Pow(1,2,4,5)").evaluate();
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Pow() takes only 2 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testRegexShouldHandleOnlyTwoArguments() throws Exception {
        Assert.assertEquals(false, new Expression("Regex('text', '^\\s*(?:\\+?(\\d{1,3}))?([-. (]*(\\d{3})[-. )]*)?((\\d{3})[-. ]*(\\d{2,4})(?:[-.x ]*(\\d+))?)\\s*$')").evaluate());
        Assert.assertEquals(true, new Expression("Regex('someone@mail.com', '^\\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,}\\b*$')").evaluate());

        try {
            new Expression("Regex('text', '^\\s*(?:\\+?(\\d{1,3}))?([-. (]*(\\d{3})[-. )]*)?((\\d{3})[-. ]*(\\d{2,4})(?:[-.x ]*(\\d+))?)\\s*$', '')").evaluate();
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Regex() takes only 2 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testRoundShouldHandleOnlyTwoArguments() throws Exception {
        Assert.assertEquals(3.22d, new Expression("Round(3.222222,2)").evaluate());
        Assert.assertEquals(4.26d, new Expression("Round(4.257,2)").evaluate());

        try {
            new Expression("Round(1,2,4,5)").evaluate();
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Round() takes only 2 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testSignShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(-1, new Expression("Sign(-10)").evaluate());
        Assert.assertEquals(1, new Expression("Sign(2)").evaluate());
        Assert.assertEquals(0, new Expression("Sign(0)").evaluate());

        try {
            new Expression("Sign(1,2,4,5)").evaluate();
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Sign() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testSinShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(0d, new Expression("Sin(0)").evaluate());

        try {
            new Expression("Sin(1,2,4,5)").evaluate();
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Sin() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testSqrtShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(5d, new Expression("Sqrt(25)").evaluate());

        try {
            new Expression("Sqrt(1,2,4,5)").evaluate();
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Sqrt() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testSumShouldHandleAtLeastOneArgument() throws Exception {
        Assert.assertEquals(12, new Expression("Sum(1,2,4,5)").evaluate());
        Assert.assertEquals(1, new Expression("sum(1)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        Assert.assertEquals(72, new Expression("sum(1,2,4,5,10,20,30)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());

        try {
            new Expression("sum()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate();
        }
        catch (ExpressiveException ute) {
            Assert.assertEquals("Sum() expects at least 1 argument(s)", ute.getMessage());
        }
    }

    @org.junit.Test
    public void testSumShouldHandleAggregates() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("array", new int[] { 1, 2, 4, 50 });

        Assert.assertEquals(69, new Expression("Sum(1,2,4,5,[array])").evaluate(variables));
    }

    @org.junit.Test
    public void testSumShouldHandleAggregatesWithNulls() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("array", new Double[] { 1.0, null, 2.5 });
        variables.put("array1", new Double[] { 3d });

        Assert.assertEquals(6.5, new Expression("Sum([array], [array1])").evaluate(variables));
    }

    @org.junit.Test
    public void testTanShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(0d, new Expression("Tan(0)").evaluate());
        try {
            Assert.assertEquals(12, new Expression("tan(1,2,4,5)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Tan() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testTruncateShouldHandleOnlyOneArgument() throws Exception {
        Assert.assertEquals(1d, new Expression("Truncate(1.7)").evaluate());
        try {
            Assert.assertEquals(12, new Expression("truncate(1,2,4,5)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("Truncate() takes only 1 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testAddDaysShouldHandleOnlyTwoArguments() throws Exception {
        String sourceDate = "2016-01-03";
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
        Date expectedDate = format.parse(sourceDate);

        Assert.assertEquals(expectedDate, new Expression("AddDays(#2016-01-01#, 2)").evaluate());
        Assert.assertEquals(expectedDate, new Expression("adddays(#2016-01-01#, 2)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());

        try {
            Assert.assertEquals(12, new Expression("adddays()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("AddDays() takes only 2 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testAddHoursShouldHandleOnlyTwoArguments() throws Exception {
        String sourceDate = "2016-01-01 02:00:00";
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
        Date expectedDate = format.parse(sourceDate);

        Assert.assertEquals(expectedDate, new Expression("AddHours(#2016-01-01 00:00:00#, 2)").evaluate());
        Assert.assertEquals(expectedDate, new Expression("addhours(#2016-01-01 00:00:00#, 2)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());

        try {
            Assert.assertEquals(12, new Expression("addhours()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("AddHours() takes only 2 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testAddMinutesShouldHandleOnlyTwoArguments() throws Exception {
        String sourceDate = "2016-01-01 00:02:00";
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
        Date expectedDate = format.parse(sourceDate);

        Assert.assertEquals(expectedDate, new Expression("AddMinutes(#2016-01-01 00:00:00#, 2)").evaluate());
        Assert.assertEquals(expectedDate, new Expression("addminutes(#2016-01-01 00:00:00#, 2)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());

        try {
            Assert.assertEquals(12, new Expression("addminutes()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("AddMinutes() takes only 2 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testAddMonthsShouldHandleOnlyTwoArguments() throws Exception {
        String sourceDate = "2016-03-01";
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
        Date expectedDate = format.parse(sourceDate);

        Assert.assertEquals(expectedDate, new Expression("AddMonths(#2016-01-01#, 2)").evaluate());
        Assert.assertEquals(expectedDate, new Expression("addmonths(#2016-01-01#, 2)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());

        try {
            Assert.assertEquals(12, new Expression("addmonths()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("AddMonths() takes only 2 argument(s)", pcme.getMessage());
        }
    }

    @org.junit.Test
    public void testAddYearsShouldHandleOnlyTwoArguments() throws Exception {
        String sourceDate = "2018-01-01";
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
        Date expectedDate = format.parse(sourceDate);

        Assert.assertEquals(expectedDate, new Expression("AddYears(#2016-01-01#, 2)").evaluate());
        Assert.assertEquals(expectedDate, new Expression("addyears(#2016-01-01#, 2)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());

        try {
            Assert.assertEquals(12, new Expression("addyears()", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate());
        }
        catch (ExpressiveException pcme) {
            Assert.assertEquals("AddYears() takes only 2 argument(s)", pcme.getMessage());
        }
    }

    /*#region Date Functions

    [TestMethod, ExpectedException(typeof(ExpressiveException), "DayOf() takes only 1 argument(s)")]
    public void DayOfShouldHandleOnlyOneArgument()
    {
        Assert.AreEqual(1, new Expression("DayOf(#2016-01-01#)").Evaluate());
        Assert.AreEqual(12, new Expression("dayof(#2016-01-12#)", ExpressiveOptions.IgnoreCase).Evaluate());

        new Expression("DayOf()").Evaluate();
    }

    [TestMethod, ExpectedException(typeof(ExpressiveException), "DaysBetween() takes only 2 argument(s)")]
    public void DaysBetweenShouldHandleOnlyTwoArguments()
    {
        Assert.AreEqual((new DateTime(2016, 01, 12) - new DateTime(2016, 01, 01)).TotalDays, new Expression("DaysBetween(#2016-01-01#, #2016-01-12#)").Evaluate());
        Assert.AreEqual((new DateTime(2016, 12, 01) - new DateTime(2016, 01, 01)).TotalDays, new Expression("daysbetween(#2016-01-01#, #2016-12-01#)", ExpressiveOptions.IgnoreCase).Evaluate());

        new Expression("DaysBetween()").Evaluate();
    }

    [TestMethod, ExpectedException(typeof(ExpressiveException), "HourOf() takes only 1 argument(s)")]
    public void HourOfShouldHandleOnlyOneArgument()
    {
        Assert.AreEqual(2, new Expression("HourOf(#2016-01-01 02:00:00#)").Evaluate());
        Assert.AreEqual(2, new Expression("hourof(#2016-01-12 02:00:00#)", ExpressiveOptions.IgnoreCase).Evaluate());

        new Expression("HourOf()").Evaluate();
    }

    [TestMethod, ExpectedException(typeof(ExpressiveException), "HoursBetween() takes only 2 argument(s)")]
    public void HoursBetweenShouldHandleOnlyTwoArguments()
    {
        Assert.AreEqual((new DateTime(2016, 01, 01, 23, 00, 00) - new DateTime(2016, 01, 01)).TotalHours, new Expression("HoursBetween(#2016-01-01#, #2016-01-01 23:00:00#)").Evaluate());
        Assert.AreEqual((new DateTime(2016, 12, 01, 23, 00, 00) - new DateTime(2016, 01, 01)).TotalHours, new Expression("hoursbetween(#2016-01-01#, #2016-12-01 23:00:00#)", ExpressiveOptions.IgnoreCase).Evaluate());

        new Expression("HoursBetween()").Evaluate();
    }

    [TestMethod, ExpectedException(typeof(ExpressiveException), "MinuteOf() takes only 1 argument(s)")]
    public void MinuteOfShouldHandleOnlyOneArgument()
    {
        Assert.AreEqual(55, new Expression("MinuteOf(#2016-01-01 02:55:00#)").Evaluate());
        Assert.AreEqual(12, new Expression("minuteof(#2016-01-12 02:12:00#)", ExpressiveOptions.IgnoreCase).Evaluate());

        new Expression("MinuteOf()").Evaluate();
    }

    [TestMethod, ExpectedException(typeof(ExpressiveException), "MinutesBetween() takes only 2 argument(s)")]
    public void MinutesBetweenShouldHandleOnlyTwoArguments()
    {
        Assert.AreEqual((new DateTime(2016, 01, 01, 23, 12, 00) - new DateTime(2016, 01, 01)).TotalMinutes, new Expression("MinutesBetween(#2016-01-01#, #2016-01-01 23:12:00#)").Evaluate());
        Assert.AreEqual((new DateTime(2016, 12, 01, 23, 32, 00) - new DateTime(2016, 01, 01)).TotalMinutes, new Expression("minutesbetween(#2016-01-01#, #2016-12-01 23:32:00#)", ExpressiveOptions.IgnoreCase).Evaluate());

        new Expression("MinutesBetween()").Evaluate();
    }

    [TestMethod, ExpectedException(typeof(ExpressiveException), "MonthOf() takes only 1 argument(s)")]
    public void MonthOfShouldHandleOnlyOneArgument()
    {
        Assert.AreEqual(01, new Expression("MonthOf(#2016-01-01#)").Evaluate());
        Assert.AreEqual(06, new Expression("monthof(#2016-06-12#)", ExpressiveOptions.IgnoreCase).Evaluate());

        new Expression("MonthOf()").Evaluate();
    }

    [TestMethod, ExpectedException(typeof(ExpressiveException), "YearOf() takes only 1 argument(s)")]
    public void YearOfShouldHandleOnlyOneArgument()
    {
        Assert.AreEqual(2016, new Expression("YearOf(#2016-01-01#)").Evaluate());
        Assert.AreEqual(2016, new Expression("yearof(#2016-01-12#)", ExpressiveOptions.IgnoreCase).Evaluate());

        new Expression("YearOf()").Evaluate();
    }

    #endregion*/


//    [TestMethod]
//    public void CustomFunctionsWithLambda()
//    {
//        var exp = new Expression("myfunc('abc')");
//        exp.RegisterFunction("myfunc", (p, a) =>
//        {
//            return 1;
//        });
//
//        Assert.AreEqual(1, exp.Evaluate());
//    }
//
//    [TestMethod]
//    public void TestAsync()
//    {
//        Expression expression = new Expression("1+3");
//
//        AutoResetEvent waitHandle = new AutoResetEvent(false);
//
//        object result = null;
//
//        expression.EvaluateAsync((m, r) =>
//        {
//            Assert.IsNull(m);
//            result = r;
//            waitHandle.Set();
//        });
//
//        waitHandle.WaitOne();
//        Assert.AreEqual(4, result);
//    }
//
//    [TestMethod]
//    public void TestAsyncSafety()
//    {
//        Expression expression = new Expression("1+3+[abc]");
//
//        AutoResetEvent waitHandle = new AutoResetEvent(false);
//
//        object result = null;
//
//        expression.EvaluateAsync((m, r) =>
//        {
//            Assert.AreEqual(m, "The variable 'abc' has not been supplied.");
//            result = r;
//            waitHandle.Set();
//        });
//
//        waitHandle.WaitOne();
//        Assert.IsNull(result);
//    }

    @org.junit.Test
    public void testShouldIdentifyParenthesisMismatch() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("a", 2);
        variables.put("b", 3);

        try {
            new Expression("([a] + [b]) * (4 - 2").evaluate(variables);
        }
        catch (Exception ex) {
            Assert.assertEquals("There aren't enough ')' symbols. Expected 2 but there is only 1", ex.getMessage());
        }
    }

    @org.junit.Test
    public void testShouldShortCircuitBooleanExpressions() throws Exception {
        Expression expression = new Expression("([a] != 0) && ([b]/[a]>2)");

        Map<String, Object> variables = new HashMap<>();
        variables.put("a", 0);

        Assert.assertEquals(false, expression.evaluate(variables));
    }

    @org.junit.Test
    public void testShouldCompareDates() throws Exception {
        Assert.assertEquals(true, new Expression("#01/01/2009#==#1/1/2009#").evaluate());
        Assert.assertEquals(false, new Expression("#2/1/2009#==#1/1/2009#").evaluate());
    }

    @org.junit.Test
    public void testShouldEvaluateSubExpressions() throws Exception {
        Expression volume = new Expression("[surface] * [h]");
        Expression surface = new Expression("[l] * [K]");

        Map<String, Object> variables = new HashMap<>();
        variables.put("surface", surface);
        variables.put("h", 3);
        variables.put("l", 1);
        variables.put("K", 2);

        Assert.assertEquals(6, volume.evaluate(variables));
    }

    @org.junit.Test
    public void testShouldParseValues() throws Exception {
        Assert.assertEquals(123456, new Expression("123456").evaluate());
        //Assert.assertEquals(new Date(2001, 01, 01), new Expression("#01/01/2001#").evaluate());
        Assert.assertEquals(123.456, new Expression("123.456").evaluate());
        Assert.assertEquals(true, new Expression("true").evaluate());
        Assert.assertEquals("true", new Expression("'true'").evaluate());
        Assert.assertEquals("qwerty", new Expression("'qwerty'").evaluate());
    }

    @org.junit.Test
    public void testShouldEscapeCharacters() throws Exception {
        Assert.assertEquals("'hello'", new Expression("'\\'hello\\''").evaluate());
        Assert.assertEquals(" ' hel lo ' ", new Expression("' \\' hel lo \\' '").evaluate());
        Assert.assertEquals("hel\nlo", new Expression("'hel\\nlo'").evaluate());
    }

    @org.junit.Test
    public void testShouldHandleOperatorsPriority() throws Exception {
        Assert.assertEquals(8, new Expression("2+2+2+2").evaluate());
        Assert.assertEquals(16, new Expression("2*2*2*2").evaluate());
        Assert.assertEquals(6, new Expression("2*2+2").evaluate());
        Assert.assertEquals(6, new Expression("2+2*2").evaluate());

        Assert.assertEquals(9d, new Expression("1 + 2 + 3 * 4 / 2").evaluate());
        Assert.assertEquals(13.5, new Expression("18.0/2.0/2.0*3.0").evaluate());
    }

    @org.junit.Test
    public void testShouldNotLosePrecision() throws Exception {
        Assert.assertEquals(0.5, new Expression("3/6").evaluate());
    }

    @org.junit.Test
    public void testShouldFailOnUnrecognisedToken() throws Exception {
        try {
            new Expression("1 + blarsh + 4").evaluate();
        }
        catch (ExpressiveException ute) {
            Assert.assertEquals("Unrecognised token 'blarsh'", ute.getMessage());
        }
    }

    @org.junit.Test
    public void testShouldHandleCaseSensitivity() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("A", 2);
        variables.put("b", 3);

        new Expression("([a] + [b]) * (4 - 2)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate(variables);
        new Expression("IF(true, true, false)", EnumSet.of(ExpressiveOptions.IGNORE_CASE)).evaluate();

        try {
            new Expression("IF(true, true, false)").evaluate();
        }
        catch (ExpressiveException ute) {
            Assert.assertEquals("Unrecognised token 'IF'", ute.getMessage());
        }

        try {
            new Expression("([a] + [b]) * (4 - 2)").evaluate(variables);
        }
        catch (Exception ex) {
            Assert.assertEquals("The variable 'a' has not been supplied.", ex.getMessage());
        }
    }

    @org.junit.Test
    public void testShouldReturnCorrectVariables() throws Exception {
        Expression expression = new Expression("([a] + [b] * [c]) + ([a] * [b])");

        Assert.assertEquals(3, expression.getReferencedVariables().length);
    }

    @org.junit.Test
    public void testCheckNullValuesAreHandledCorrectly() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("a", null);

        Assert.assertNull(new Expression("2 + [a]").evaluate(variables));
        Assert.assertNull(new Expression("2 * [a]").evaluate(variables));
        Assert.assertNull(new Expression("2 / [a]").evaluate(variables));
        Assert.assertNull(new Expression("2 - [a]").evaluate(variables));
        Assert.assertNull(new Expression("2 % [a]").evaluate(variables));
    }

    @org.junit.Test
    public void testCheckNaNIsHandledCorrectly() throws Exception {
        Map<String, Object> variables = new HashMap<>();
        variables.put("a", Double.NaN);

        Assert.assertEquals(Double.NaN, new Expression("2 + [a]").evaluate(variables));
        Assert.assertEquals(Double.NaN, new Expression("2 * [a]").evaluate(variables));
        Assert.assertEquals(Double.NaN, new Expression("2 / [a]").evaluate(variables));
        Assert.assertEquals(Double.NaN, new Expression("2 - [a]").evaluate(variables));
        Assert.assertEquals(Double.NaN, new Expression("2 % [a]").evaluate(variables));
    }

    @org.junit.Test
    public void testCheckComplicatedDepth() throws Exception {
        // This was a previous bug (Issue #6) so this is in place to make sure it does not re-occur.
        Expression expression = new Expression("((1 + 2) * 3) + (([d] / [e]) * [f]) - ([a] * [b])");

        Map<String, Object> variables = new HashMap<>();
        variables.put("a", 1);
        variables.put("b", 2);
        variables.put("c", 3);
        variables.put("d", 6);
        variables.put("e", 2);
        variables.put("f", 6);
        Object value = expression.evaluate(variables);

        Assert.assertEquals(25d, value);
    }
}