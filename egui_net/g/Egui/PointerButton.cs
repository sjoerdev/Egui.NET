#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum PointerButton {
        Primary = 0,
        Secondary = 1,
        Middle = 2,
        Extra1 = 3,
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
