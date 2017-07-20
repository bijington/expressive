package com.bijington.expressive.operators.logic;

import com.bijington.expressive.expressions.BinaryExpression;
import com.bijington.expressive.expressions.BinaryExpressionType;
import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.operators.OperatorBase;
import com.bijington.expressive.operators.OperatorPrecedence;

/**
 * Created by shaun on 11/07/2016.
 */
public class OrOperator extends OperatorBase {
    @Override
    public String[] getTags() { return new String[] { "||", "or" }; }

    @Override
    public IExpression buildExpression(String previousToken, IExpression[] expressions) {
        return new BinaryExpression(BinaryExpressionType.Or, expressions[0], expressions[1]);
    }

    @Override
    public int getPrecedence(String previousToken) {
        return OperatorPrecedence.Or;
    }
}
