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
    /// Different layer categories
    /// </summary>
    public enum Order {
            /// <summary>
            /// Painted behind all floating windows
            /// </summary>
            Background = 0,
            /// <summary>
            /// Normal moveable windows that you reorder by click
            /// </summary>
            Middle = 1,
            /// <summary>
            /// Popups, menus etc that should always be painted on top of windows
            /// Foreground objects can also have tooltips
            /// </summary>
            Foreground = 2,
            /// <summary>
            /// Things floating on top of everything else, like tooltips.
            /// You cannot interact with these.
            /// </summary>
            Tooltip = 3,
            /// <summary>
            /// Debug layer, always painted last / on top
            /// </summary>
            Debug = 4,
    }
    internal static class OrderExtensions {

        internal static void Serialize(this Order value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static Order Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(Order), index))
                throw new Serde.DeserializationException("Unknown variant index for Order: " + index);
            Order value = (Order)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
