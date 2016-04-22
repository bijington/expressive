# Expressive expression parser

## Supported languages

C#  
Swift  
Java

## Operators

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

## Functions

| Function           | Usage                         | Description                   |
| ------------------ | ----------------------------- | :----------------------------:|
| Abs                | Abs(-1)                       | |
| Acos               | Acos(1)                       | |
| Asin               | Asin(0)                       | |
| Atan               | Atan(0)                       | |
| Avg                | Avg(1,2,3,4)                  | |
| Ceiling            | Ceiling(1.5)                  | |
| Cos                | Cos(0)                        | |
| EndsWith           | EndsWith('end')               | |
| Exp                | Exp(0)                        | |
| Floor              | Floor(1.5)                    | |
| IEEERemainder      | IEEERemainder(3, 2)           | |
| If                 | If(1 == 1, 'true', 'false')   | |
| In                 | In(1, 1, 2, 3, 4)             | |
| Log                | Log(1, 10)                    | |
| Log10              | Log10(1)                      | |
| Max                | Max(1, 2)                     | |
| Min                | Min(1, 2)                     | |
| Pow                | Pow(3, 2)                     | |
| Random             | Random()                      | |
| Round              | Round(4.5234, 2)              | |
| Sign               | Sign(-10)                     | |
| Sin                | Sin(0)                        | |
| Sqrt               | Sqrt(4)                       | |
| StartsWith         | StartsWith('start')           | |
| Sum                | Sum(0, 2, 3, 4)               | |
| Tan                | Tan(0)                        | |
| Truncate           | Truncate(1.7)                 | |


| Name                | Usage                   |
| ------------------- | ----------------------- |
| Variable            | [parametername]         |
| String              | "abc", 'abc'            |
| DateTime            | #2015/12/25#            |
| Integer             | 123                     |
| Boolean             | true, false             |
| Floating point      | 123.456                 |
| Scientific notation | 1.22e2                  |
| Boolean             | true, false             |


# Testing Plan

