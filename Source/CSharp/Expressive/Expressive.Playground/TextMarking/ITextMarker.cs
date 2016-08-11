using System;
using System.Windows;
using System.Windows.Media;

namespace Expressive.Playground.TextMarking
{
    /// <summary>
	/// Represents a text marker.
	/// </summary>
	public interface ITextMarker
    {
        /// <summary>
        /// Gets the start offset of the marked text region.
        /// </summary>
        int StartOffset { get; }

        /// <summary>
        /// Gets the end offset of the marked text region.
        /// </summary>
        int EndOffset { get; }

        /// <summary>
        /// Gets the length of the marked region.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Deletes the text marker.
        /// </summary>
        void Delete();

        /// <summary>
        /// Gets whether the text marker was deleted.
        /// </summary>
        bool IsDeleted { get; }

        /// <summary>
        /// Event that occurs when the text marker is deleted.
        /// </summary>
        event EventHandler Deleted;

        /// <summary>
        /// Gets/Sets the background color.
        /// </summary>
        Color? BackgroundColor { get; set; }

        /// <summary>
        /// Gets/Sets the foreground color.
        /// </summary>
        Color? ForegroundColor { get; set; }

        /// <summary>
        /// Gets/Sets the font weight.
        /// </summary>
        FontWeight? FontWeight { get; set; }

        /// <summary>
        /// Gets/Sets the font style.
        /// </summary>
        FontStyle? FontStyle { get; set; }

        /// <summary>
        /// Gets/Sets the type of the marker. Use TextMarkerType.None for normal markers.
        /// </summary>
        TextMarkerTypes MarkerTypes { get; set; }

        /// <summary>
        /// Gets/Sets the color of the marker.
        /// </summary>
        Color MarkerColor { get; set; }

        /// <summary>
        /// Gets/Sets an object with additional data for this text marker.
        /// </summary>
        object Tag { get; set; }

        /// <summary>
        /// Gets/Sets an object that will be displayed as tooltip in the text editor.
        /// </summary>
        /// <remarks>Not supported in this sample!</remarks>
        object ToolTip { get; set; }
    }
}
