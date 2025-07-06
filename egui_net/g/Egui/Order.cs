#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum Order {
        Background = 0,
        Middle = 1,
        Foreground = 2,
        Tooltip = 3,
        Debug = 4,
    }
    internal static class OrderExtensions {

        internal static void Serialize(this Order value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static Order Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(Order), index))
                throw new Serde.DeserializationException("Unknown variant index for Order: " + index);
            Order value = (Order)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
