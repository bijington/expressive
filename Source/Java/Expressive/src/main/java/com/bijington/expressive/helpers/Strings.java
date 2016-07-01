package com.bijington.expressive.helpers;

/**
 * Created by shaun on 30/06/2016.
 */
public class Strings {
    public static Boolean isNullOrEmpty(String value) {
        if (value != null)
            return value.length() == 0;
        else
            return true;
    }
}
