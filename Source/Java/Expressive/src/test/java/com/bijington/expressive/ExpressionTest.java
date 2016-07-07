package test.com.bijington.expressive;

import static org.junit.Assert.*;

/**
 * Created by shaun on 30/06/2016.
 */
@org.junit.TestCase
public class ExpressionTest extends TestCase {
    @org.junit.Test
    public void getReferencedVariables() throws Exception {

    }

    @org.junit.Test
    public void evaluate() throws Exception {
        Expression expression = new Expression("true == false");

        expression.evaluate();
    }

}