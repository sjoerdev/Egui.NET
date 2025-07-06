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
    /// Mouse button (or similar for touch input)
    /// </summary>
    public enum PointerButton {
            /// <summary>
            /// The primary mouse button is usually the left one.
            /// </summary>
            Primary = 0,
            /// <summary>
            /// The secondary mouse button is usually the right one,
            /// and most often used for context menus or other optional things.
            /// </summary>
            Secondary = 1,
            /// <summary>
            /// The tertiary mouse button is usually the middle mouse button (e.g. clicking the scroll wheel).
            /// </summary>
            Middle = 2,
            /// <summary>
            /// The first extra mouse button on some mice. In web typically corresponds to the Browser back button.
            /// </summary>
            Extra1 = 3,
            /// <summary>
            /// The second extra mouse button on some mice. In web typically corresponds to the Browser forward button.
            /// </summary>
            Extra2 = 4,
    }
    internal static class PointerButtonExtensions {

        internal static void Serialize(this PointerButton value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static PointerButton Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(PointerButton), index))
                throw new Serde.DeserializationException("Unknown variant index for PointerButton: " + index);
            PointerButton value = (PointerButton)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
