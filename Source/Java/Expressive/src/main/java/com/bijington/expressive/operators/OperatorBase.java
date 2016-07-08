package com.bijington.expressive.operators;

import com.bijington.expressive.expressions.IExpression;
import com.sun.xml.internal.ws.policy.privateutil.PolicyUtils;

import java.util.List;

/**
 * Created by shaun on 08/07/2016.
 */
public abstract class OperatorBase implements IOperator {
    public abstract String[] getTags();

    public abstract IExpression buildExpression(String previousToken, IExpression[] expressions);

    public Boolean canGetCaptiveTokens(String previousToken, String token, List<String> remainingTokens)
    {
        return true;
    }

    public String[] getCaptiveTokens(String previousToken, String token, List<String> remainingTokens)
    {
        return new String[] { token };
    }

    public String[] getInnerCaptiveTokens(String[] allCaptiveTokens)
    {
        return new String[0];
    }

    public abstract int getPrecedence(String previousToken);
}
