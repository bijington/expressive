package com.bijington.expressive.operators.logic;

import com.bijington.expressive.expressions.*;
import com.bijington.expressive.operators.OperatorBase;
import com.bijington.expressive.operators.OperatorPrecedence;

/**
 * Created by shaun on 11/07/2016.
 */
public class NotOperator extends OperatorBase {
    @Override
    public String[] getTags() { return new String[] { "!", "not" }; }

    @Override
    public IExpression buildExpression(String previousToken, IExpression[] expressions) {
        return new UnaryExpression(UnaryExpressionType.Not, (expressions[0] != null) ? expressions[0] : expressions[1]);
    }

    @Override
    public int getPrecedence(String previousToken) {
        return OperatorPrecedence.Not;
    }
}