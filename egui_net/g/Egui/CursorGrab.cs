#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum CursorGrab {
            None = 0,
            Confined = 1,
            Locked = 2,
    }
    internal static class CursorGrabExtensions {

        internal static void Serialize(this CursorGrab value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static CursorGrab Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(CursorGrab), index))
                throw new Serde.DeserializationException("Unknown variant index for CursorGrab: " + index);
            CursorGrab value = (CursorGrab)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
