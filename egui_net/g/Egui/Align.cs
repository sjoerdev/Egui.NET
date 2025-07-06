#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum Align {
        Min = 0,
        Center = 1,
        Max = 2,
    }
    internal static class AlignExtensions {

        internal static void Serialize(this Align value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static Align Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(Align), index))
                throw new Serde.DeserializationException("Unknown variant index for Align: " + index);
            Align value = (Align)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
