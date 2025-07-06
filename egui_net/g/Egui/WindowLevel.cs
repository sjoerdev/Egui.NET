#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum WindowLevel {
        Normal = 0,
        AlwaysOnBottom = 1,
        AlwaysOnTop = 2,
    }
    internal static class WindowLevelExtensions {

        internal static void Serialize(this WindowLevel value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static WindowLevel Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(WindowLevel), index))
                throw new Serde.DeserializationException("Unknown variant index for WindowLevel: " + index);
            WindowLevel value = (WindowLevel)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
