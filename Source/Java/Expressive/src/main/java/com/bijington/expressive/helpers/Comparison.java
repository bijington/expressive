package com.bijington.expressive.helpers;

import java.util.Date;

/**
 * Created by shaun on 12/07/2016.
 */
public class Comparison {

    public static int compareUsingMostPreciseType(Object lhs, Object rhs) {

        String n = lhs.getClass().getCanonicalName();
        // Int comparison
        try {
            if (lhs.getClass().equals(Integer.class) || rhs.getClass().equals(Integer.class)) {
                return Integer.class.cast(lhs).compareTo(Integer.class.cast(rhs));
            }
        }
        catch (ClassCastException cce) {
            String err = cce.getMessage();
        }

        // Long comparison
        try {
            if (lhs.getClass().equals(Long.class) || rhs.getClass().equals(Long.class)) {
                return Long.class.cast(lhs).compareTo(Long.class.cast(rhs));
            }
        }
        catch (ClassCastException cce) {

        }

        // Double comparison
        try {
            if (lhs.getClass().equals(Double.class) || rhs.getClass().equals(Double.class)) {
                return Double.class.cast(lhs).compareTo(Double.class.cast(rhs));
            }
        }
        catch (ClassCastException cce) {

        }

        // Float comparison
        try {
            if (lhs.getClass().equals(Float.class) || rhs.getClass().equals(Float.class)) {
                return Float.class.cast(lhs).compareTo(Float.class.cast(rhs));
            }
        }
        catch (ClassCastException cce) {

        }

        // Date comparison
        try {
            if (lhs.getClass().equals(Date.class) || rhs.getClass().equals(Date.class)) {
                return Date.class.cast(lhs).compareTo(Date.class.cast(rhs));
            }
        }
        catch (ClassCastException cce) {

        }

        // String comparison
        try {
            if (lhs.getClass().equals(String.class) || rhs.getClass().equals(String.class)) {
                return String.class.cast(lhs).compareTo(String.class.cast(rhs));
            }
        }
        catch (ClassCastException cce) {

        }

        return 0;
    }
}
