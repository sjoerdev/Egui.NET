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
    /// The unit associated with the numeric value of a mouse wheel event
    /// </summary>
    public enum MouseWheelUnit {
            /// <summary>
            /// Number of ui points (logical pixels)
            /// </summary>
            Point = 0,
            /// <summary>
            /// Number of lines
            /// </summary>
            Line = 1,
            /// <summary>
            /// Number of pages
            /// </summary>
            Page = 2,
    }
    internal static class MouseWheelUnitExtensions {

        internal static void Serialize(this MouseWheelUnit value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static MouseWheelUnit Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(MouseWheelUnit), index))
                throw new Serde.DeserializationException("Unknown variant index for MouseWheelUnit: " + index);
            MouseWheelUnit value = (MouseWheelUnit)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
