using System;
using System.Collections.Generic;

namespace Expressive.Playground.TextMarking
{
    public interface ITextMarkerService
    {
        /// <summary>
        /// Creates a new text marker. The text marker will be invisible at first,
        /// you need to set one of the Color properties to make it visible.
        /// </summary>
        ITextMarker Create(int startOffset, int length);

        /// <summary>
        /// Gets the list of text markers.
        /// </summary>
        IEnumerable<ITextMarker> TextMarkers { get; }

        /// <summary>
        /// Removes the specified text marker.
        /// </summary>
        void Remove(ITextMarker marker);

        /// <summary>
        /// Removes all text markers that match the condition.
        /// </summary>
        void RemoveAll(Predicate<ITextMarker> predicate);

        /// <summary>
        /// Finds all text markers at the specified offset.
        /// </summary>
        IEnumerable<ITextMarker> GetMarkersAtOffset(int offset);
    }
}
