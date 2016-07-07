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
}
