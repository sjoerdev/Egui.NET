#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum ScrollBarVisibility {
        AlwaysHidden = 0,
        VisibleWhenNeeded = 1,
        AlwaysVisible = 2,
    }
    internal static class ScrollBarVisibilityExtensions {

        internal static void Serialize(this ScrollBarVisibility value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static ScrollBarVisibility Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(ScrollBarVisibility), index))
                throw new Serde.DeserializationException("Unknown variant index for ScrollBarVisibility: " + index);
            ScrollBarVisibility value = (ScrollBarVisibility)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
