#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum TouchPhase {
        Start = 0,
        Move = 1,
        End = 2,
        Cancel = 3,
    }
    internal static class TouchPhaseExtensions {

        internal static void Serialize(this TouchPhase value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static TouchPhase Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(TouchPhase), index))
                throw new Serde.DeserializationException("Unknown variant index for TouchPhase: " + index);
            TouchPhase value = (TouchPhase)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
