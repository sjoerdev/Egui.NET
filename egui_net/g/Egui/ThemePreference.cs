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
    /// The user's theme preference.
    /// </summary>
    public enum ThemePreference {
            /// <summary>
            /// Dark mode: light text on a dark background.
            /// </summary>
            Dark = 0,
            /// <summary>
            /// Light mode: dark text on a light background.
            /// </summary>
            Light = 1,
            /// <summary>
            /// Follow the system's theme preference.
            /// </summary>
            System = 2,
    }
    internal static class ThemePreferenceExtensions {

        internal static void Serialize(this ThemePreference value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static ThemePreference Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(ThemePreference), index))
                throw new Serde.DeserializationException("Unknown variant index for ThemePreference: " + index);
            ThemePreference value = (ThemePreference)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
