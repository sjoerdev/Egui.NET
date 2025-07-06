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
    /// Dark or Light theme.
    /// </summary>
    public enum Theme {
            /// <summary>
            /// Dark mode: light text on a dark background.
            /// </summary>
            Dark = 0,
            /// <summary>
            /// Light mode: dark text on a light background.
            /// </summary>
            Light = 1,
    }
    internal static class ThemeExtensions {

        internal static void Serialize(this Theme value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static Theme Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(Theme), index))
                throw new Serde.DeserializationException("Unknown variant index for Theme: " + index);
            Theme value = (Theme)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
