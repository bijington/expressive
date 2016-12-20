package com.bijington.expressive.helpers;

/**
 * Created by shaun on 06/07/2016.
 */
public class Convert {
    public static <T> T as(Class<T> desiredClass, Object o){
        if(desiredClass.isInstance(o)){
            return desiredClass.cast(o);
        }
        return null;
    }

    public static Boolean toBoolean(Object value) {
        if (value == null) return false;

        // Boolean check
        if (value.getClass().equals(Boolean.class)) {
            return Boolean.class.cast(value);
        }
        // String check
        if (value.getClass().equals(String.class)) {
            return Boolean.valueOf(String.class.cast(value));
        }
        // Integer check
        if (value.getClass().equals(Integer.class)) {
            return Integer.class.cast(value) != 0;
        }

        return false;
    }

    public static Double toDouble(Object value) {
        if (value == null) return 0.0;

        // Boolean check
        if (value.getClass().equals(Boolean.class)) {
            return Boolean.class.cast(value) ? 1.0 : 0.0;
        }
        // String check
        if (value.getClass().equals(String.class)) {
            return Double.valueOf(String.class.cast(value));
        }

        return (Double)Numbers.add(0.0, value);
    }

    public static Integer toInteger(Object value) {
        if (value == null) return 0;

        // Boolean check
        if (value.getClass().equals(Boolean.class)) {
            return Boolean.class.cast(value) ? 1 : 0;
        }
        // String check
        if (value.getClass().equals(String.class)) {
            return Integer.valueOf(String.class.cast(value));
        }
        // Integer check
        if (value.getClass().equals(Integer.class)) {
            return Integer.class.cast(value);
        }

        return 0;
    }
}
