package com.bijington.expressive.exceptions;

/**
 * Created by shaun on 30/06/2016.
 */
public class UnrecognisedTokenException extends Exception {
    public UnrecognisedTokenException(String token) {
        super("Unrecognised token '" + token + "'");
    }
}
