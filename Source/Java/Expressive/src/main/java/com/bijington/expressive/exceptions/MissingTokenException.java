package com.bijington.expressive.exceptions;

/**
 * Created by shaun on 28/06/2016.
 */
public class MissingTokenException extends Exception {

    public MissingTokenException(String message, Character missingToken) {
        super(message);
    }
}
