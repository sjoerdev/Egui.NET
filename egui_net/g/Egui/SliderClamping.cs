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
    /// Specifies how values in a <c>Slider</c> are clamped.
    /// </summary>
    public enum SliderClamping {
            /// <summary>
            /// Values are not clamped.
            ///
            /// This means editing the value with the keyboard,
            /// or dragging the number next to the slider will always work.
            ///
            /// The actual slider part is always clamped though.
            /// </summary>
            Never = 0,
            /// <summary>
            /// Users cannot enter new values that are outside the range.
            ///
            /// Existing values remain intact though.
            /// </summary>
            Edits = 1,
            /// <summary>
            /// Always clamp values, even existing ones.
            /// </summary>
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
