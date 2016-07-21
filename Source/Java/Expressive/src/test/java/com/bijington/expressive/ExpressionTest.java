package com.bijington.expressive;

import com.bijington.expressive.helpers.Convert;
import junit.framework.TestCase;
import org.junit.Assert;

/**
 * Created by shaun on 30/06/2016.
 */
public class ExpressionTest extends TestCase {
    @org.junit.Test
    public void getReferencedVariables() throws Exception {

    }

    @org.junit.Test
    public void testEvaluate() throws Exception {
        Expression expression = new Expression("true");

        Object result = expression.evaluate();
        Assert.assertTrue(Convert.as(Boolean.class, result));
    }

    @org.junit.Test
    public void testNullCoalescing() throws Exception {
        Assert.assertEquals(1, new Expression("null ?? 1").evaluate());
        Assert.assertEquals(null, new Expression("null ?? null").evaluate());

        //Assert.AreEqual(54, new Expression("[empty] ?? 54").Evaluate(new Dictionary<string, object> { ["empty"] = null }));
    }
}