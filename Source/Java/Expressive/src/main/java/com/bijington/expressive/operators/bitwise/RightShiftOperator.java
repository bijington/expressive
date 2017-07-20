package com.bijington.expressive.operators.bitwise;

import com.bijington.expressive.expressions.BinaryExpression;
import com.bijington.expressive.expressions.BinaryExpressionType;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.operators.OperatorBase;
import com.bijington.expressive.operators.OperatorPrecedence;

/**
 * Created by shaun on 12/07/2016.
 */
public class RightShiftOperator extends OperatorBase {
    @Override
    public String[] getTags() { return new String[] { ">>" }; }

    @Override
    public IExpression buildExpression(String previousToken, IExpression[] expressions) {
        return new BinaryExpression(BinaryExpressionType.RightShift, expressions[0], expressions[1]);
    }

    @Override
    public int getPrecedence(String previousToken) {
        return OperatorPrecedence.RightShift;
    }
}
