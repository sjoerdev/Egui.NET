#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum SliderOrientation {
        Horizontal = 0,
        Vertical = 1,
    }
    internal static class SliderOrientationExtensions {

        internal static void Serialize(this SliderOrientation value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static SliderOrientation Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(SliderOrientation), index))
                throw new Serde.DeserializationException("Unknown variant index for SliderOrientation: " + index);
            SliderOrientation value = (SliderOrientation)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
