#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum UserAttentionType {
        Critical = 0,
        Informational = 1,
        Reset = 2,
    }
    internal static class UserAttentionTypeExtensions {

        internal static void Serialize(this UserAttentionType value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static UserAttentionType Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(UserAttentionType), index))
                throw new Serde.DeserializationException("Unknown variant index for UserAttentionType: " + index);
            UserAttentionType value = (UserAttentionType)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
