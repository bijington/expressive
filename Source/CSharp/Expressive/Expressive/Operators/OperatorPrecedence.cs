namespace Expressive.Operators
{
    enum OperatorPrecedence
    {
        Minimum = 0,
        Or = 1,
        And = 2,
        Equal = 3,
        NotEqual = 4,
        LessThan = 5,
        GreaterThan = 6,
        LessThanOrEqual = 7,
        GreaterThanOrEqual = 8,
        Not = 9,
        BitwiseOr = 10,
        BitwiseXOr = 11,
        BitwiseAnd = 12,
        LeftShift = 13,
        RightShift = 14,
        Add = 15,
        Subtract = 16,
        Multiply = 17,
        Modulus = 18,
        Divide = 19,
        NullCoalescing = 20,
        Conditional = 20,
        //BitwiseNot = 20, // TODO: Is this needed?
        UnaryPlus = 21,
        UnaryMinus = 22,
        ParenthesisOpen = 23,
        ParenthesisClose = 24,
    }
}
