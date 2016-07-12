package com.bijington.expressive.helpers;

/**
 * Created by shaun on 10/07/2016.
 */
public class Numbers {
    public static Object add(Object lhs, Object rhs) {
        if (lhs == null || rhs == null) {
            return null;
        }

        if (lhs.getClass().equals(Integer.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Integer.class.cast(lhs) + Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Integer.class.cast(lhs) + Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Integer.class.cast(lhs) + Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Integer.class.cast(lhs) + Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Long.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Long.class.cast(lhs) + Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Long.class.cast(lhs) + Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Long.class.cast(lhs) + Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Long.class.cast(lhs) + Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Double.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Double.class.cast(lhs) + Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Double.class.cast(lhs) + Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Double.class.cast(lhs) + Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Double.class.cast(lhs) + Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Float.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Float.class.cast(lhs) + Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Float.class.cast(lhs) + Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Float.class.cast(lhs) + Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Float.class.cast(lhs) + Float.class.cast(rhs);
            }
        }

        return  null;
    }

    public static Object divide(Object lhs, Object rhs) {
        if (lhs == null || rhs == null) {
            return null;
        }

        if (lhs.getClass().equals(Integer.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Integer.class.cast(lhs) / Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Integer.class.cast(lhs) / Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Integer.class.cast(lhs) / Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Integer.class.cast(lhs) / Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Long.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Long.class.cast(lhs) / Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Long.class.cast(lhs) / Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Long.class.cast(lhs) / Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Long.class.cast(lhs) / Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Double.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Double.class.cast(lhs) / Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Double.class.cast(lhs) / Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Double.class.cast(lhs) / Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Double.class.cast(lhs) / Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Float.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Float.class.cast(lhs) / Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Float.class.cast(lhs) / Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Float.class.cast(lhs) / Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Float.class.cast(lhs) / Float.class.cast(rhs);
            }
        }

        return  null;
    }

    public static Object modulus(Object lhs, Object rhs) {
        if (lhs == null || rhs == null) {
            return null;
        }

        if (lhs.getClass().equals(Integer.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Integer.class.cast(lhs) % Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Integer.class.cast(lhs) % Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Integer.class.cast(lhs) % Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Integer.class.cast(lhs) % Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Long.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Long.class.cast(lhs) % Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Long.class.cast(lhs) % Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Long.class.cast(lhs) % Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Long.class.cast(lhs) % Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Double.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Double.class.cast(lhs) * Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Double.class.cast(lhs) * Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Double.class.cast(lhs) * Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Double.class.cast(lhs) * Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Float.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Float.class.cast(lhs) * Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Float.class.cast(lhs) * Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Float.class.cast(lhs) * Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Float.class.cast(lhs) * Float.class.cast(rhs);
            }
        }

        return  null;
    }

    public static Object multiply(Object lhs, Object rhs) {
        if (lhs == null || rhs == null) {
            return null;
        }

        if (lhs.getClass().equals(Integer.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Integer.class.cast(lhs) * Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Integer.class.cast(lhs) * Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Integer.class.cast(lhs) * Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Integer.class.cast(lhs) * Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Long.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Long.class.cast(lhs) * Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Long.class.cast(lhs) * Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Long.class.cast(lhs) * Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Long.class.cast(lhs) * Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Double.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Double.class.cast(lhs) * Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Double.class.cast(lhs) * Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Double.class.cast(lhs) * Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Double.class.cast(lhs) * Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Float.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Float.class.cast(lhs) * Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Float.class.cast(lhs) * Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Float.class.cast(lhs) * Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Float.class.cast(lhs) * Float.class.cast(rhs);
            }
        }

        return  null;
    }

    public static Object subtract(Object lhs, Object rhs) {
        if (lhs == null || rhs == null) {
            return null;
        }

        if (lhs.getClass().equals(Integer.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Integer.class.cast(lhs) - Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Integer.class.cast(lhs) - Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Integer.class.cast(lhs) - Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Integer.class.cast(lhs) - Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Long.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Long.class.cast(lhs) - Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Long.class.cast(lhs) - Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Long.class.cast(lhs) - Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Long.class.cast(lhs) - Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Double.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Double.class.cast(lhs) - Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Double.class.cast(lhs) - Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Double.class.cast(lhs) - Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Double.class.cast(lhs) - Float.class.cast(rhs);
            }
        }
        else if (lhs.getClass().equals(Float.class)) {
            if (rhs.getClass().equals(Integer.class)) {
                return Float.class.cast(lhs) - Integer.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Long.class)) {
                return Float.class.cast(lhs) - Long.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Double.class)) {
                return Float.class.cast(lhs) - Double.class.cast(rhs);
            }
            else if (rhs.getClass().equals(Float.class)) {
                return Float.class.cast(lhs) - Float.class.cast(rhs);
            }
        }

        return  null;
    }
}
