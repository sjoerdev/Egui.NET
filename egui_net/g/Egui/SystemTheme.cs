#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum SystemTheme {
        SystemDefault = 0,
        Light = 1,
        Dark = 2,
    }
    internal static class SystemThemeExtensions {

        internal static void Serialize(this SystemTheme value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static SystemTheme Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(SystemTheme), index))
                throw new Serde.DeserializationException("Unknown variant index for SystemTheme: " + index);
            SystemTheme value = (SystemTheme)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
