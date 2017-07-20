package com.bijington.expressive.operators.grouping;

import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.operators.OperatorBase;
import com.bijington.expressive.operators.OperatorPrecedence;

/**
 * Created by shaun on 10/07/2016.
 */
public class ParenthesisCloseOperator extends OperatorBase {
    @Override
    public String[] getTags() {return new String[] { ")" }; }

    @Override
    public IExpression buildExpression(String previousToken, IExpression[] expressions) {
        return (expressions[0] != null) ? expressions[0] : expressions[1];
    }

    @Override
    public int getPrecedence(String previousToken) {
        return OperatorPrecedence.ParenthesisClose;
    }
}
