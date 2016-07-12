package com.bijington.expressive.operators.additive;

import com.bijington.expressive.expressions.*;
import com.bijington.expressive.helpers.Strings;
import com.bijington.expressive.operators.OperatorBase;
import com.bijington.expressive.operators.OperatorPrecedence;

import java.util.*;

/**
 * Created by shaun on 10/07/2016.
 */
public class SubtractOperator extends OperatorBase {

    @Override
    public String[] getTags() {return new String[] { "-", "\u2212" }; }

    @Override
    public IExpression buildExpression(String previousToken, IExpression[] expressions) {
        if (isUnary(previousToken)) {
            return new UnaryExpression(UnaryExpressionType.Minus, (expressions[0] != null) ? expressions[0] : expressions[1]);
        }

        return new BinaryExpression(BinaryExpressionType.Subtract, expressions[0], expressions[1]);
    }

    @Override
    public Boolean canGetCaptiveTokens(String previousToken, String token, Queue<String> remainingTokens) {
        Queue<String> remainingTokensCopy = new LinkedList<>(remainingTokens);

        return this.getCaptiveTokens(previousToken, token, remainingTokensCopy).length > 0;
    }

    @Override
    public String[] getCaptiveTokens(String previousToken, String token, Queue<String> remainingTokens) {
        String[] result;

        if (isUnary(previousToken)) {
            if (!remainingTokens.isEmpty()) {
                result = new String[] { token, remainingTokens.remove() };
            }
            else {
                result = new String[] { token };
            }
        }
        else {
            result = new String[] { token };
        }

        return result;
    }

    @Override
    public String[] getInnerCaptiveTokens(String[] allCaptiveTokens) {
        return Arrays.copyOfRange(allCaptiveTokens, 1, allCaptiveTokens.length);
    }

    @Override
    public int getPrecedence(String previousToken) {
        if (isUnary(previousToken)) {
            return OperatorPrecedence.UnaryMinus;
        }
        return OperatorPrecedence.Subtract;
    }

    private Boolean isUnary(String previousToken) {
        return Strings.isNullOrEmpty(previousToken) ||
                previousToken.equals("(") ||
                Strings.isArithmeticOperator(previousToken);
    }
}
