package com.bijington.expressive.helpers;

/**
 * Created by shaun on 30/06/2016.
 */
public class Strings {
    public static Boolean isArithmeticOperator(String source)
    {
        return source.equals("+") ||
                source.equals("-") || source.equals("\u2212") ||
                source.equals("/") || source.equals("\u00f7") ||
                source.equals("*") || source.equals("\u00d7") ||
                source.equals("+") ||
                source.equals("+") ||
                source.equals("+");
    }

    public static Boolean isNullOrEmpty(String value) {
        if (value != null)
            return value.length() == 0;
        else
            return true;
    }

    public static String padLeft(String value, int totalWidth, Character character) {
        StringBuilder builder = new StringBuilder();

        for (int toPrepend = totalWidth-value.length(); toPrepend > 0; toPrepend--) {
            builder.append(character);
        }

        builder.append(value);
        return builder.toString();
    }

    public static String padRight(String value, int totalWidth, Character character) {
        StringBuilder builder = new StringBuilder();

        for (int toPrepend = totalWidth-value.length(); toPrepend > 0; toPrepend--) {
            builder.append(character);
        }

        builder.append(value);
        return builder.toString();
    }

    public static Number parseNumber(String value) {
        Number number = null;

        try {
            number = Integer.parseInt(value);
        } catch(NumberFormatException e) {
            try {
                number = Long.parseLong(value);
            } catch(NumberFormatException e1) {
                try {
                    number = Double.parseDouble(value);
                } catch(NumberFormatException e2) {
                    try {
                        number = Float.parseFloat(value);
                    } catch(NumberFormatException e3) {

                    }
                }
            }
        }

        return number;
    }
}
