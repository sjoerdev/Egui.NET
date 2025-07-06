#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum ResizeDirection {
            North = 0,
            South = 1,
            East = 2,
            West = 3,
            NorthEast = 4,
            SouthEast = 5,
            NorthWest = 6,
            SouthWest = 7,
    }
    internal static class ResizeDirectionExtensions {

        internal static void Serialize(this ResizeDirection value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static ResizeDirection Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(ResizeDirection), index))
                throw new Serde.DeserializationException("Unknown variant index for ResizeDirection: " + index);
            ResizeDirection value = (ResizeDirection)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
