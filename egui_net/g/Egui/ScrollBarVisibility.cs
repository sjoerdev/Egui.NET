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
    /// Indicate whether the horizontal and vertical scroll bars must be always visible, hidden or visible when needed.
    /// </summary>
    public enum ScrollBarVisibility {
            /// <summary>
            /// Hide scroll bar even if they are needed.
            ///
            /// You can still scroll, with the scroll-wheel
            /// and by dragging the contents, but there is no
            /// visual indication of how far you have scrolled.
            /// </summary>
            AlwaysHidden = 0,
            /// <summary>
            /// Show scroll bars only when the content size exceeds the container,
            /// i.e. when there is any need to scroll.
            ///
            /// This is the default.
            /// </summary>
            VisibleWhenNeeded = 1,
            /// <summary>
            /// Always show the scroll bar, even if the contents fit in the container
            /// and there is no need to scroll.
            /// </summary>
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
