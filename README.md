# Expressive

Expressive is a multi-platform expression parsing and evaluation framework.

## Design and Implementation
There are 3 main stages of the parsing/evaluation of an Expression.

1. Tokenising - this deals with walking through the characters in the expression and matching the items against the known set of `Operators`, `Functions` and `Values`.
2. Compiling - once the `Expression` has been tokenised the set of tokens is then used to build a tree of internal `IExpression` nodes.
3. Evaluation - each individual `IExpression` node is then evaluated to determine it's result and eventually the overall result of the top level `Expression`.

### Error handling

The `Expression` will terminate early in the following situations:
1. Any part of the tokenising process fails (i.e. unrecognised tokens).
2. If any of the `Function`s are supplied with an incorrect number of parameters.

### Aggregate operator
Operators can now handle aggregates (i.e. a variable passed in as an array of values will each be enumerated).
```c
var expression = new Expression("[array] + 2");
var result = expression.Evaluate(new Dictionary<string, object> { ["variable"] = new [] { 2, 3, 4, 5});
// result == 16
```
### Lazy Parameter evaluation
Each individual `parameter` will only be evaluated at the point of being used within its `IExpression` node. This will prevent parts of an expression being evaluated should the result already be determined (e.g. when the left hand side of an && (logical AND) operator being `false`). 

The supported languages are:

- C&#35;
  - [GitHub release](https://github.com/bijington/expressive/releases/tag/v1.3.1)
  - [Nuget release](https://www.nuget.org/packages/ExpressiveParser/)
- Java
  - In beta testing
- Swift (TBA)

## Usage Examples

### C&#35;

#### Simple Evaluation
```c
var expression = new Expression("1+2");
var result = expression.Evaluate();
```

#### Custom options
```c
public enum ExpressiveOptions
{
    /// <summary>
    /// Specifies that no options are set.
    /// </summary>
    None = 1,
    /// <summary>
    /// Specifies case-insensitive matching.
    /// </summary>
    IgnoreCase = 2,
    /// <summary>
    /// No-cache mode. Ignores any pre-compiled expression in the cache.
    /// </summary>
    NoCache = 4,
    /// <summary>
    /// When using Round(), if a number is halfway between two others, it is rounded toward the nearest number that is away from zero.
    /// </summary>
    RoundAwayFromZero = 8,
    /// <summary>
    /// All options are used.
    /// </summary>
    All = IgnoreCase | NoCache | RoundAwayFromZero,
}

// Use in any combination.
var expression = new Expression("1+2", ExpressiveOptions.IgnoreCase | ExpressiveOptions.NoCache);
var result = expression.Evaluate();
```

#### Variables
```c#
var expression = new Expression("1 * [variable]");
var result = expression.Evaluate(new Dictionary<string, object> { ["variable"] = 2);
```

#### Built-in Functions
Expressive provides a standard set of functions (see Functions).
```c#
var expression = new Expression("sum(1,2,3,4)");
var result = expression.Evaluate();
```

#### Custom Functions
Custom functions allow you to register your own set of funcitonality in order to extend the current set. There are 2 methods of registering a custom function:

* `IFunction` implementation.

```c#
internal class AbsFunction : IFunction
{
    #region IFunction Members

    public IDictionary<string, object> Variables { get; set; }

    public string Name { get { return "Asin"; } }

    public object Evaluate(IExpression[] parameters)
    {
        return Math.Asin(Convert.ToDouble(parameters[0].Evaluate(Variables)));
    }

    #endregion
}

var expression = new Expression("Abs(1)");
expression.RegisterFunction(new AbsFunction());
var result = expression.Evaluate();
```

* Lambda callback.

```c#
var expression = new Expression("myfunc(1)");
expression.RegisterFunction("myfunc", (p, v) =>
{
    // p = parameters
    // v = variables
    return p[0].Evaluate(v);
});
var result = expression.Evaluate();
```

#### Retrieve referenced variables
Compiles the expression and returns an array of the variable names contained within the expression.
```c#
var expression = new Expression("[abc] + [def] * [ghi]");
var variables = expression.ReferencedVariables;
```

#### Asynchronous evaluation
Useful for when an evaluation might take a long time (i.e. a function that deals with a  long running background tasks such as loading from the database).
```c
var expression = new Expression("1+2");
expression.EvaluateAsync((r) =>
{
    var result = r;
});
```

## General functionality

### Operators

| Name                   | Operator          | Usage                   |
| ---------------------- | ----------------- |-------------------------|
| Add                    | +                 | 1 + 2                   |
| Unary plus             | +                 | +2                      |
| Subtract               | -                 | 1 - 2                   |
| Unary minus            | -                 | -2                      |
| Multiply               | *                 | 2 * 2                   |
| Divide                 | /                 | 2 / 2                   |
| Modulus                | %                 | 6 % 2                   |
| Equals                 | =, ==             | 1 == 1                  |
| Not equals             | !=, <>            | 1 <> 2                  |
| Greater than           | >                 | 2 > 1                   |
| Greater than or equal  | >=                | 2 >= 2                  |
| Less than              | <                 | 1 < 2                   |
| Less than or equal     | <=                | 2 <= 2                  |
| Boolean AND            | &&, AND           | true && false           |
| Boolean OR             | &#124;&#124;, OR  | true &#124;&#124; false |
| Bitwise AND            | &      	         | 2 & 2                   |
| Bitwise OR             | &#124;            | 2 &#124; 2              |
| Bitwise XOR            | ^                 | 2 ^ 2                   |
| Bitwise NOT            | ~                 | 2 ~ 2                   |
| Left shift             | <<                | 2 << 2                  |
| Right shift            | >>                | 2 >> 2                  |
| Null Coalesce          | ??                | null ?? 0               |

### Functions

#### Date

| Function | AddDays |
| -------- | ------- |
| **Usage** | AddDays(#2017-01-30#, 2)|
| **Description** | Returns the supplied date with the specified number of days added. |
| **Remarks** | Expects **exactly 2** parameters.|

| Function | AddHours |
| -------- | ------- |
| **Usage** | AddHours(#2017-01-30 00:00:00#, 2)|
| **Description** | Returns the supplied date with the specified number of hours added. |
| **Remarks** | Expects **exactly 2** parameters.|

| Function | AddMilliseconds |
| -------- | ------- |
| **Usage** | AddMilliseconds(#2017-01-30 00:00:00.000#, 2)|
| **Description** | Returns the supplied date with the specified number of milliseconds added. |
| **Remarks** | Expects **exactly 2** parameters.|

| Function | AddMinutes |
| -------- | ------- |
| **Usage** | AddMinutes(#2017-01-30 00:00:00#, 2)|
| **Description** | Returns the supplied date with the specified number of minutes added. |
| **Remarks** | Expects **exactly 2** parameters.|

| Function | AddMonths |
| -------- | ------- |
| **Usage** | AddMonths(#2017-01-30#, 2)|
| **Description** | Returns the supplied date with the specified number of months added. |
| **Remarks** | Expects **exactly 2** parameters.|

| Function | AddSeconds |
| -------- | ------- |
| **Usage** | AddSeconds(#2017-01-30 00:00:00#, 2)|
| **Description** | Returns the supplied date with the specified number of seconds added. |
| **Remarks** | Expects **exactly 2** parameters.|

| Function | AddYears |
| -------- | ------- |
| **Usage** | AddMonths(#2017-01-30#, 2)|
| **Description** | Returns the supplied date with the specified number of months added. |
| **Remarks** | Expects **exactly 2** parameters.|

****

| Function | DayOf |
| -------- | ------- |
| **Usage** | DayOf(#2017-01-30#)|
| **Description** | Returns the day component of the supplied date. |
| **Remarks** | Expects **exactly 1** parameter.|

| Function | HourOf |
| -------- | ------- |
| **Usage** | DayOf(#2017-01-30 00:00:00#)|
| **Description** | Returns the hour component of the supplied date. |
| **Remarks** | Expects **exactly 1** parameter.|

| Function | MillisecondOf |
| -------- | ------- |
| **Usage** | MillisecondOf(#2017-01-30 00:00:00.000#)|
| **Description** | Returns the millisecond component of the supplied date. |
| **Remarks** | Expects **exactly 1** parameter.|

| Function | MinuteOf |
| -------- | ------- |
| **Usage** | MinuteOf(#2017-01-30 00:00:00#)|
| **Description** | Returns the minute component of the supplied date. |
| **Remarks** | Expects **exactly 1** parameter.|

| Function | MonthOf |
| -------- | ------- |
| **Usage** | MonthOf(#2017-01-30 00:00:00#)|
| **Description** | Returns the month component of the supplied date. |
| **Remarks** | Expects **exactly 1** parameter.|

| Function | SecondOf |
| -------- | ------- |
| **Usage** | SecondOf(#2017-01-30 00:00:00#)|
| **Description** | Returns the second component of the supplied date. |
| **Remarks** | Expects **exactly 1** parameter.|

| Function | YearOf |
| -------- | ------- |
| **Usage** | YearOf(#2017-01-30 00:00:00#)|
| **Description** | Returns the year component of the supplied date. |
| **Remarks** | Expects **exactly 1** parameter.|

****

| Function | DaysBetween |
| -------- | ------- |
| **Usage** | DaysBetween(#2017-01-01#, #2017-01-30#)|
| **Description** | Returns the number of days between the specified 2 dates. |
| **Remarks** | Expects **exactly 2** parameters.|

| Function | HoursBetween |
| -------- | ------- |
| **Usage** | HoursBetween(#2017-01-01 00:00:00#, #2017-01-30 00:00:00#)|
| **Description** | Returns the number of hours between the specified 2 dates. |
| **Remarks** | Expects **exactly 2** parameters.|

| Function | MillisecondsBetween |
| -------- | ------- |
| **Usage** | MillisecondsBetween(#2017-01-01 00:00:00#, #2017-01-30 00:00:00#)|
| **Description** | Returns the number of milliseconds between the specified 2 dates. |
| **Remarks** | Expects **exactly 2** parameters.|

| Function | MinutesBetween |
| -------- | ------- |
| **Usage** | MinutesBetween(#2017-01-01#, #2017-01-30#)|
| **Description** | Returns the number of minutes between the specified 2 dates. |
| **Remarks** | Expects **exactly 2** parameters.|

** MonthsBetween - not yet implemented due to the inprecise duration of a month **

| Function | SecondsBetween |
| -------- | ------- |
| **Usage** | SecondsBetween(#2017-01-01#, #2017-01-30#)|
| **Description** | Returns the number of seconds between the specified 2 dates. |
| **Remarks** | Expects **exactly 2** parameters.|

** YearsBetween - not yet implemented due to the inprecise duration of a year **

#### Mathematical

| Function | Abs |
| -------- | ------- |
| **Usage** | Abs(-1)|
| **Description** | Returns the absolute value of a number. |
| **Remarks** | Expects **exactly 1** parameter.|


| Function | Acos |
| -------- | ------- |
| **Usage** | Acos(1)|
| **Description** | Returns the angle whose cosine is the specified number. |
| **Remarks** | Expects **exactly 1** parameter.|


| Function | Asin |
| -------- | ------- |
| **Usage** | Asin(0)|
| **Description** | Returns the angle whose sine is the specified number. |
| **Remarks** | Expects **exactly 1** parameter.|


| Function | Atan |
| -------- | ------- |
| **Usage** | Atan(0)|
| **Description** | Returns the angle whose tangent is the specified number. |
| **Remarks** | Expects **exactly 1** parameter.|


| Function | Average |
| -------- | ------- |
| **Usage** | Average(1,2,3,4,5,6)|
| **Description** | Returns the mean average out of the supplied numbers. |
| **Remarks** | Expects **at least 1** parameter.|


| Function | Ceiling |
| -------- | ------- |
| **Usage** | Ceiling(1.5)|
| **Description** | Returns the smallest integral value that is greater than or equal to the specified number. |
| **Remarks** | Expects **exactly 1** parameter.|


** Still to be converted**

| Function           | Usage                         | Parameters                    |
| ------------------ | ----------------------------- | ------------------------------ |
| Cos                | Cos(0)                        | Expects **exactly 1** parameter. |
| EndsWith           | EndsWith('end')               | Expects **exactly 1** parameter. |
| Exp                | Exp(0)                        | Expects **exactly 1** parameter. |
| Floor              | Floor(1.5)                    | Expects **exactly 1** parameter. |
| IEEERemainder      | IEEERemainder(3, 2)           | Expects **exactly 2** parameters. |
| If                 | If(1 == 1, 'true', 'false')   | Expects **exactly 3** parameters. |
| In                 | In(1, 1, 2, 3, 4)             | Expects **at least 2** parameters. |
| Log                | Log(1, 10)                    | Expects **exactly 2** parameters. |
| Log10              | Log10(1)                      | Expects **exactly 1** parameter. |
| Max                | Max(1, 2)                     | Expects **at least 1** parameter. |
| Mean               | Mean(1, 2)                    | Expects **exactly 2** parameters. |
| Median             | Median(1, 2)                  | Expects **exactly 2** parameters. |
| Min                | Min(1, 2)                     | Expects **at least 1** parameter. |
| Mode               | Mode(1, 2)                    | Expects **exactly 2** parameters. |
| PadLeft            | PadLeft([number], 3, '0')     | Expects **exactly 3** parameters. |
| PadRight           | PadRight([number], 3, '0')    | Expects **exactly 3** parameters. |
| Pow                | Pow(3, 2)                     | Expects **exactly 2** parameters. |
| Random             | Random()                      | Expects **no** parameters. |
| Round              | Round(4.5234, 2)              | Expects **exactly 2** parameters. |
| Sign               | Sign(-10)                     | Expects **exactly 1** parameter. |
| Sin                | Sin(0)                        | Expects **exactly 1** parameter. |
| Sqrt               | Sqrt(4)                       | Expects **exactly 1** parameter. |
| StartsWith         | StartsWith('start')           | Expects **exactly 1** parameter. |
| Sum                | Sum(0, 2, 3, 4)               | Expects **at least 1** parameter. |
| Tan                | Tan(0)                        | Expects **exactly 1** parameter. |
| Truncate           | Truncate(1.7)                 | Expects **exactly 1** parameter. |

### Values

| Name                | Usage                   |
| ------------------- | ----------------------- |
| Variable            | [parametername]         |
| String              | "abc", 'abc'            |
| DateTime            | #2015/12/25#, #Today#, #Now#            |
| Integer             | 123                     |
| Boolean             | true, false             |
| Floating point      | 123.456                 |
| null                | null                    |
