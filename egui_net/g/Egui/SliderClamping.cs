#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum SliderClamping {
        Never = 0,
        Edits = 1,
        Always = 2,
    }
    internal static class SliderClampingExtensions {

        internal static void Serialize(this SliderClamping value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static SliderClamping Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(SliderClamping), index))
                throw new Serde.DeserializationException("Unknown variant index for SliderClamping: " + index);
            SliderClamping value = (SliderClamping)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
