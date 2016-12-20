package com.bijington.expressive.helpers;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.concurrent.TimeUnit;

/**
 * Created by shaun on 09/12/2016.
 */
public class Dates {
    public static Date getDate(String dateString, ArrayList<String> dateFormats) {
        Date date;

        for (Integer i = 0; i < dateFormats.size(); i++) {
            DateFormat dateFormat = new SimpleDateFormat(dateFormats.get(i));
            dateFormat.setLenient(false);
            try {
                date = dateFormat.parse(dateString);
                return date;
            } catch (ParseException pe) {

            }
        }

        return null;
    }

    public static Date add(Object dateObject, Object unitsObject, int unitType) {
        if (dateObject == null || unitsObject == null) return null;

        Date date = Date.class.cast(dateObject);
        Integer units = Integer.class.cast(unitsObject);

        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.add(unitType, units);
        return cal.getTime();
    }

    public static Integer componentOf(Object dateObject, int componentType) {
        if (dateObject == null) return null;

        Date date = Date.class.cast(dateObject);
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);

        return cal.get(componentType);
    }

    public static Map<TimeUnit,Long> computeDiff(Date date1, Date date2) {
        long diffInMillies = date2.getTime() - date1.getTime();
        List<TimeUnit> units = new ArrayList<TimeUnit>(EnumSet.allOf(TimeUnit.class));
        Collections.reverse(units);
        Map<TimeUnit,Long> result = new LinkedHashMap<TimeUnit,Long>();
        long milliesRest = diffInMillies;
        for ( TimeUnit unit : units ) {
            long diff = unit.convert(milliesRest, TimeUnit.MILLISECONDS);
            long diffInMilliesForUnit = unit.toMillis(diff);
            milliesRest = milliesRest - diffInMilliesForUnit;
            result.put(unit,diff);
        }
        return result;
    }
}
