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
    /// An input event from the backend into egui, about a specific viewport.
    /// </summary>
    public enum ViewportEvent {
            /// <summary>
            /// The user clicked the close-button on the window, or similar.
            ///
            /// If this is the root viewport, the application will exit
            /// after this frame unless you send a
            /// <c>CancelClose</c> command.
            ///
            /// If this is not the root viewport,
            /// it is up to the user to hide this viewport the next frame.
            ///
            /// This even will wake up both the child and parent viewport.
            /// </summary>
            Close = 0,
    }
    internal static class ViewportEventExtensions {

        internal static void Serialize(this ViewportEvent value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static ViewportEvent Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(ViewportEvent), index))
                throw new Serde.DeserializationException("Unknown variant index for ViewportEvent: " + index);
            ViewportEvent value = (ViewportEvent)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
