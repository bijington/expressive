using System;

namespace Expressive
{
    /// <summary>
    /// Options to alter the way in which an <see cref="Expression"/> is parsed and evaluated.
    /// </summary>
    [Flags]
    public enum ExpressiveOptions
    {
        /// <summary>
        /// Specifies that no options are set.
        /// </summary>
        None = 1,
        /// <summary>
        /// Specifies case-insensitive matching.
        /// </summary>
        IgnoreCase = 2,
        /// <summary>
        /// No-cache mode. Ingores any pre-compiled expression in the cache.
        /// </summary>
        NoCache = 4,
        /// <summary>
        /// When using Round(), if a number is halfway between two others, it is rounded toward the nearest number that is away from zero.
        /// </summary>
        RoundAwayFromZero = 8,
        /// <summary>
        /// All options are used.
        /// </summary>
        All = IgnoreCase | NoCache | RoundAwayFromZero,
    }
}
