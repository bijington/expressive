package com.bijington.expressive.operators.grouping;

import com.bijington.expressive.expressions.IExpression;
import com.bijington.expressive.expressions.ParenthesisedExpression;
import com.bijington.expressive.operators.OperatorBase;
import com.bijington.expressive.operators.OperatorPrecedence;

import java.util.*;

/**
 * Created by shaun on 10/07/2016.
 */
public class ParenthesisOpenOperator extends OperatorBase {
    @Override
    public String[] getTags() {return new String[] { "(" }; }

    @Override
    public IExpression buildExpression(String previousToken, IExpression[] expressions) {
        return new ParenthesisedExpression((expressions[0] != null) ? expressions[0] : expressions[1]);
    }

    @Override
    public Boolean canGetCaptiveTokens(String previousToken, String token, Queue<String> remainingTokens) {
        Queue<String> remainingTokensCopy = new LinkedList<>(remainingTokens);

        return this.getCaptiveTokens(previousToken, token, remainingTokensCopy).length > 0;
    }

    @Override
    public String[] getCaptiveTokens(String previousToken, String token, Queue<String> remainingTokens) {
        List<String> result = new ArrayList();

        result.add(token);

        int parenCount = 1;

        while (!remainingTokens.isEmpty()) {
            String nextToken = remainingTokens.remove();

            result.add(nextToken);

            if (nextToken.equals("(")) {
                parenCount++;
            }
            else if (nextToken.equals(")")) {
                parenCount--;
            }

            if (parenCount <= 0) {
                break;
            }
        }

        return result.toArray(new String[result.size()]);
    }

    @Override
    public String[] getInnerCaptiveTokens(String[] allCaptiveTokens) {
        return Arrays.copyOfRange(allCaptiveTokens, 1, allCaptiveTokens.length - 1);
    }

    @Override
    public int getPrecedence(String previousToken) {
        return OperatorPrecedence.ParenthesisOpen;
    }
}
