using ICSharpCode.AvalonEdit.Document;
using System;
using System.Windows;
using System.Windows.Media;

namespace Expressive.Playground.TextMarking
{
    public sealed class TextMarker : TextSegment, ITextMarker
    {
        #region Fields

        Color? _backgroundColor;
        FontStyle? _fontStyle;
        FontWeight? _fontWeight;
        Color? _foregroundColor;
        Color _markerColor;
        TextMarkerTypes _markerTypes;
        readonly TextMarkerService _service;

        #endregion

        #region Constructors

        public TextMarker(TextMarkerService service, int startOffset, int length)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            _service = service;
            StartOffset = startOffset;
            Length = length;
            _markerTypes = TextMarkerTypes.None;
        }

        #endregion

        #region ITextMarker Members

        public event EventHandler Deleted;

        public bool IsDeleted
        {
            get { return !this.IsConnectedToCollection; }
        }

        public void Delete()
        {
            _service.Remove(this);
        }

        internal void OnDeleted()
        {
            if (Deleted != null)
                Deleted(this, EventArgs.Empty);
        }

        void Redraw()
        {
            _service.Redraw(this);
        }

        public Color? BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    Redraw();
                }
            }
        }
        
        public Color? ForegroundColor
        {
            get { return _foregroundColor; }
            set
            {
                if (_foregroundColor != value)
                {
                    _foregroundColor = value;
                    Redraw();
                }
            }
        }
        
        public FontWeight? FontWeight
        {
            get { return _fontWeight; }
            set
            {
                if (_fontWeight != value)
                {
                    _fontWeight = value;
                    Redraw();
                }
            }
        }

        public FontStyle? FontStyle
        {
            get { return _fontStyle; }
            set
            {
                if (_fontStyle != value)
                {
                    _fontStyle = value;
                    Redraw();
                }
            }
        }

        public object Tag { get; set; }

        public TextMarkerTypes MarkerTypes
        {
            get { return _markerTypes; }
            set
            {
                if (_markerTypes != value)
                {
                    _markerTypes = value;
                    Redraw();
                }
            }
        }

        public Color MarkerColor
        {
            get { return _markerColor; }
            set
            {
                if (_markerColor != value)
                {
                    _markerColor = value;
                    Redraw();
                }
            }
        }

        public object ToolTip { get; set; }

        #endregion
    }
}
