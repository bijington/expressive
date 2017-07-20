package com.bijington.expressive.exceptions;

/**
 * Created by shaun on 11/11/2016.
 */
public class ExpressiveException extends Exception {
    /*public ExpressiveException(String message) : base(message)
    {

    }

    public ExpressiveException(Exception innerException) : base(innerException.Message, innerException)
    {

    }*/
    public ExpressiveException(String message) {
        super(message);
    }

    public ExpressiveException(Exception ex) {
        super(ex.getMessage(), ex);
    }
}
