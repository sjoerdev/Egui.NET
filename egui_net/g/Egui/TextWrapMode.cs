#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public enum TextWrapMode {
            Extend = 0,
            Wrap = 1,
            Truncate = 2,
    }
    internal static class TextWrapModeExtensions {

        internal static void Serialize(this TextWrapMode value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static TextWrapMode Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(TextWrapMode), index))
                throw new Serde.DeserializationException("Unknown variant index for TextWrapMode: " + index);
            TextWrapMode value = (TextWrapMode)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
