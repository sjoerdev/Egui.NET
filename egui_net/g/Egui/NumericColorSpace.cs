#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    /// <summary>
    /// How to display numeric color values.
    /// </summary>
    public enum NumericColorSpace {
            /// <summary>
            /// RGB is 0-255 in gamma space.
            ///
            /// Alpha is 0-255 in linear space.
            /// </summary>
            GammaByte = 0,
            /// <summary>
            /// 0-1 in linear space.
            /// </summary>
            Linear = 1,
    }
    internal static class NumericColorSpaceExtensions {

        internal static void Serialize(this NumericColorSpace value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static NumericColorSpace Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(NumericColorSpace), index))
                throw new Serde.DeserializationException("Unknown variant index for NumericColorSpace: " + index);
            NumericColorSpace value = (NumericColorSpace)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
