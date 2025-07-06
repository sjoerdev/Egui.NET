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
    /// The different types of built-in widgets in egui
    /// </summary>
    public enum WidgetType {
            Label = 0,
            /// <summary>
            /// e.g. a hyperlink
            /// </summary>
            Link = 1,
            TextEdit = 2,
            Button = 3,
            Checkbox = 4,
            RadioButton = 5,
            /// <summary>
            /// A group of radio buttons.
            /// </summary>
            RadioGroup = 6,
            SelectableLabel = 7,
            ComboBox = 8,
            Slider = 9,
            DragValue = 10,
            ColorButton = 11,
            ImageButton = 12,
            Image = 13,
            CollapsingHeader = 14,
            ProgressIndicator = 15,
            Window = 16,
            /// <summary>
            /// If you cannot fit any of the above slots.
            ///
            /// If this is something you think should be added, file an issue.
            /// </summary>
            Other = 17,
    }
    internal static class WidgetTypeExtensions {

        internal static void Serialize(this WidgetType value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static WidgetType Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(WidgetType), index))
                throw new Serde.DeserializationException("Unknown variant index for WidgetType: " + index);
            WidgetType value = (WidgetType)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
