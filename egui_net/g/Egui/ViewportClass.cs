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
    /// The different types of viewports supported by egui.
    /// </summary>
    public enum ViewportClass {
            /// <summary>
            /// The root viewport; i.e. the original window.
            /// </summary>
            Root = 0,
            /// <summary>
            /// A viewport run independently from the parent viewport.
            ///
            /// This is the preferred type of viewport from a performance perspective.
            ///
            /// Create these with <c>ShowViewportDeferred</c>.
            /// </summary>
            Deferred = 1,
            /// <summary>
            /// A viewport run inside the parent viewport.
            ///
            /// This is the easier type of viewport to use, but it is less performant
            /// at it requires both parent and child to repaint if any one of them needs repainting,
            /// which effectively produces double work for two viewports, and triple work for three viewports, etc.
            ///
            /// Create these with <c>ShowViewportImmediate</c>.
            /// </summary>
            Immediate = 2,
            /// <summary>
            /// The fallback, when the egui integration doesn't support viewports,
            /// or <c>EmbedViewports</c> is set to <c>True</c>.
            /// </summary>
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
