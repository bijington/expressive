using System;

namespace Expressive.Playground.TextMarking
{
    [Flags]
    public enum TextMarkerTypes
    {
        /// <summary>
        /// Use no marker
        /// </summary>
        None = 0x0000,
        /// <summary>
        /// Use squiggly underline marker
        /// </summary>
        SquigglyUnderline = 0x001,
        /// <summary>
        /// Normal underline.
        /// </summary>
        NormalUnderline = 0x002,
        /// <summary>
        /// Dotted underline.
        /// </summary>
        DottedUnderline = 0x004,

        /// <summary>
        /// Horizontal line in the scroll bar.
        /// </summary>
        LineInScrollBar = 0x0100,
        /// <summary>
        /// Small triangle in the scroll bar, pointing to the right.
        /// </summary>
        ScrollBarRightTriangle = 0x0400,
        /// <summary>
        /// Small triangle in the scroll bar, pointing to the left.
        /// </summary>
        ScrollBarLeftTriangle = 0x0800,
        /// <summary>
        /// Small circle in the scroll bar.
        /// </summary>
        CircleInScrollBar = 0x1000
    }
}
