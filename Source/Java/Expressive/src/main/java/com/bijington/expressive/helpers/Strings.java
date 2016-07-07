package com.bijington.expressive.helpers;

import java.text.NumberFormat;

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

    public static Number parseNumber(String value) {
        Number number = null;

        try {
            number = Double.parseDouble(value);
        } catch(NumberFormatException e) {
            try {
                number = Float.parseFloat(value);
            } catch(NumberFormatException e1) {
                try {
                    number = Long.parseLong(value);
                } catch(NumberFormatException e2) {
                    try {
                        number = Integer.parseInt(value);
                    } catch(NumberFormatException e3) {

                    }
                }
            }
        }

        return number;
    }
}
