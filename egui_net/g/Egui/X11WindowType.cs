#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum X11WindowType {
        Normal = 0,
        Desktop = 1,
        Dock = 2,
        Toolbar = 3,
        Menu = 4,
        Utility = 5,
        Splash = 6,
        Dialog = 7,
        DropdownMenu = 8,
        PopupMenu = 9,
        Tooltip = 10,
        Notification = 11,
        Combo = 12,
        Dnd = 13,
    }
    internal static class X11WindowTypeExtensions {

        internal static void Serialize(this X11WindowType value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static X11WindowType Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(X11WindowType), index))
                throw new Serde.DeserializationException("Unknown variant index for X11WindowType: " + index);
            X11WindowType value = (X11WindowType)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
