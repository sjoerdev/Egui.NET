#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum MouseWheelUnit {
        Point = 0,
        Line = 1,
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
