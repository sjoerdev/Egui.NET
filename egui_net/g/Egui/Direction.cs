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
    /// Layout direction, one of <c>LeftToRight</c>, <c>RightToLeft</c>, <c>TopDown</c>, <c>BottomUp</c>.
    /// </summary>
    public enum Direction {
            LeftToRight = 0,
            RightToLeft = 1,
            TopDown = 2,
            BottomUp = 3,
    }
    internal static class DirectionExtensions {

        internal static void Serialize(this Direction value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static Direction Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(Direction), index))
                throw new Serde.DeserializationException("Unknown variant index for Direction: " + index);
            Direction value = (Direction)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
