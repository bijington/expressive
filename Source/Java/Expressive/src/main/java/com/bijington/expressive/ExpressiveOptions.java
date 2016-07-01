package com.bijington.expressive;

import java.util.EnumSet;

public enum ExpressiveOptions {
    NONE, IGNORE_CASE, NO_CACHE, ROUND_AWAY_FROM_ZERO;

    public static final EnumSet<ExpressiveOptions> ALL_OPTS = EnumSet.allOf(ExpressiveOptions.class);
}

///**
// * Created by shaun on 27/06/2016.
// */
//public class ExpressiveOptions {
//    /// <summary>
//    /// Specifies that no options are set.
//    /// </summary>
//    public static final int None = 1;  // 0001
//    /// <summary>
//    /// Specifies case-insensitive matching.
//    /// </summary>
//    public static final int IgnoreCase   = 2;  // 0010
//    /// <summary>
//    /// No-cache mode. Ignores any pre-compiled expression in the cache.
//    /// </summary>
//    public static final int NoCache = 4;  // 0100
//    /// <summary>
//    /// When using Round(), if a number is halfway between two others, it is rounded toward the nearest number that is away from zero.
//    /// </summary>
//    public static final int RoundAwayFromZero = 8;  // 1000
//    /// <summary>
//    /// All options are used.
//    /// </summary>
//    public static final int All  = 15; // 1111
//}
