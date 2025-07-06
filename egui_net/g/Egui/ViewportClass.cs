#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum ViewportClass {
        Root = 0,
        Deferred = 1,
        Immediate = 2,
        Embedded = 3,
    }
    internal static class ViewportClassExtensions {

        internal static void Serialize(this ViewportClass value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static ViewportClass Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(ViewportClass), index))
                throw new Serde.DeserializationException("Unknown variant index for ViewportClass: " + index);
            ViewportClass value = (ViewportClass)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
