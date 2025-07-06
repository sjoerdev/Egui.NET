#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum IMEPurpose {
        Normal = 0,
        Password = 1,
        Terminal = 2,
    }
    internal static class ImePurposeExtensions {

        internal static void Serialize(this IMEPurpose value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static IMEPurpose Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(IMEPurpose), index))
                throw new Serde.DeserializationException("Unknown variant index for IMEPurpose: " + index);
            IMEPurpose value = (IMEPurpose)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
