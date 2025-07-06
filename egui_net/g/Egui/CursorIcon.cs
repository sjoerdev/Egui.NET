#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    /// <summary>
    /// A mouse cursor icon.
    ///
    /// egui emits a <c>CursorIcon</c> in <c>PlatformOutput</c> each frame as a request to the integration.
    ///
    /// Loosely based on <https://developer.mozilla.org/en-US/docs/Web/CSS/cursor>.
    /// </summary>
    public enum CursorIcon {
            /// <summary>
            /// Normal cursor icon, whatever that is.
            /// </summary>
            Default = 0,
            /// <summary>
            /// Show no cursor
            /// </summary>
            None = 1,
            /// <summary>
            /// A context menu is available
            /// </summary>
            ContextMenu = 2,
            /// <summary>
            /// Question mark
            /// </summary>
            Help = 3,
            /// <summary>
            /// Pointing hand, used for e.g. web links
            /// </summary>
            PointingHand = 4,
            /// <summary>
            /// Shows that processing is being done, but that the program is still interactive.
            /// </summary>
            Progress = 5,
            /// <summary>
            /// Not yet ready, try later.
            /// </summary>
            Wait = 6,
            /// <summary>
            /// Hover a cell in a table
            /// </summary>
            Cell = 7,
            /// <summary>
            /// For precision work
            /// </summary>
            Crosshair = 8,
            /// <summary>
            /// Text caret, e.g. "Click here to edit text"
            /// </summary>
            Text = 9,
            /// <summary>
            /// Vertical text caret, e.g. "Click here to edit vertical text"
            /// </summary>
            VerticalText = 10,
            /// <summary>
            /// Indicated an alias, e.g. a shortcut
            /// </summary>
            Alias = 11,
            /// <summary>
            /// Indicate that a copy will be made
            /// </summary>
            Copy = 12,
            /// <summary>
            /// Omnidirectional move icon (e.g. arrows in all cardinal directions)
            /// </summary>
            Move = 13,
            /// <summary>
            /// Can't drop here
            /// </summary>
            NoDrop = 14,
            /// <summary>
            /// Forbidden
            /// </summary>
            NotAllowed = 15,
            /// <summary>
            /// The thing you are hovering can be grabbed
            /// </summary>
            Grab = 16,
            /// <summary>
            /// You are grabbing the thing you are hovering
            /// </summary>
            Grabbing = 17,
            /// <summary>
            /// Something can be scrolled in any direction (panned).
            /// </summary>
            AllScroll = 18,
            /// <summary>
            /// Horizontal resize <c></c> to make something wider or more narrow (left to/from right)
            /// </summary>
            ResizeHorizontal = 19,
            /// <summary>
            /// Diagonal resize <c>/</c> (right-up to/from left-down)
            /// </summary>
            ResizeNeSw = 20,
            /// <summary>
            /// Diagonal resize <c>\</c> (left-up to/from right-down)
            /// </summary>
            ResizeNwSe = 21,
            /// <summary>
            /// Vertical resize <c>|</c> (up-down or down-up)
            /// </summary>
            ResizeVertical = 22,
            /// <summary>
            /// Resize something rightwards (e.g. when dragging the right-most edge of something)
            /// </summary>
            ResizeEast = 23,
            /// <summary>
            /// Resize something down and right (e.g. when dragging the bottom-right corner of something)
            /// </summary>
            ResizeSouthEast = 24,
            /// <summary>
            /// Resize something downwards (e.g. when dragging the bottom edge of something)
            /// </summary>
            ResizeSouth = 25,
            /// <summary>
            /// Resize something down and left (e.g. when dragging the bottom-left corner of something)
            /// </summary>
            ResizeSouthWest = 26,
            /// <summary>
            /// Resize something leftwards (e.g. when dragging the left edge of something)
            /// </summary>
            ResizeWest = 27,
            /// <summary>
            /// Resize something up and left (e.g. when dragging the top-left corner of something)
            /// </summary>
            ResizeNorthWest = 28,
            /// <summary>
            /// Resize something up (e.g. when dragging the top edge of something)
            /// </summary>
            ResizeNorth = 29,
            /// <summary>
            /// Resize something up and right (e.g. when dragging the top-right corner of something)
            /// </summary>
            ResizeNorthEast = 30,
            /// <summary>
            /// Resize a column
            /// </summary>
            ResizeColumn = 31,
            /// <summary>
            /// Resize a row
            /// </summary>
            ResizeRow = 32,
            /// <summary>
            /// Enhance!
            /// </summary>
            ZoomIn = 33,
            /// <summary>
            /// Let's get a better overview
            /// </summary>
            ZoomOut = 34,
    }
    internal static class CursorIconExtensions {

        internal static void Serialize(this CursorIcon value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static CursorIcon Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(CursorIcon), index))
                throw new Serde.DeserializationException("Unknown variant index for CursorIcon: " + index);
            CursorIcon value = (CursorIcon)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
