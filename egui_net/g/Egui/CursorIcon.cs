#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum CursorIcon {
        Default = 0,
        None = 1,
        ContextMenu = 2,
        Help = 3,
        PointingHand = 4,
        Progress = 5,
        Wait = 6,
        Cell = 7,
        Crosshair = 8,
        Text = 9,
        VerticalText = 10,
        Alias = 11,
        Copy = 12,
        Move = 13,
        NoDrop = 14,
        NotAllowed = 15,
        Grab = 16,
        Grabbing = 17,
        AllScroll = 18,
        ResizeHorizontal = 19,
        ResizeNeSw = 20,
        ResizeNwSe = 21,
        ResizeVertical = 22,
        ResizeEast = 23,
        ResizeSouthEast = 24,
        ResizeSouth = 25,
        ResizeSouthWest = 26,
        ResizeWest = 27,
        ResizeNorthWest = 28,
        ResizeNorth = 29,
        ResizeNorthEast = 30,
        ResizeColumn = 31,
        ResizeRow = 32,
        ZoomIn = 33,
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
