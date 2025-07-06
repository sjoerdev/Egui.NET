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
    /// In what phase a touch event is in.
    /// </summary>
    public enum TouchPhase {
            /// <summary>
            /// User just placed a touch point on the touch surface
            /// </summary>
            Start = 0,
            /// <summary>
            /// User moves a touch point along the surface. This event is also sent when
            /// any attributes (position, force, …) of the touch point change.
            /// </summary>
            Move = 1,
            /// <summary>
            /// User lifted the finger or pen from the surface, or slid off the edge of
            /// the surface
            /// </summary>
            End = 2,
            /// <summary>
            /// Touch operation has been disrupted by something (various reasons are possible,
            /// maybe a pop-up alert or any other kind of interruption which may not have
            /// been intended by the user)
            /// </summary>
            Cancel = 3,
    }
    internal static class TouchPhaseExtensions {

        internal static void Serialize(this TouchPhase value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static TouchPhase Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(TouchPhase), index))
                throw new Serde.DeserializationException("Unknown variant index for TouchPhase: " + index);
            TouchPhase value = (TouchPhase)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
