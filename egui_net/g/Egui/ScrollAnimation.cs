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
    /// Scroll animation configuration, used when programmatically scrolling somewhere (e.g. with <c>ScrollToCursor</c>)
    /// The animation duration is calculated based on the distance to be scrolled via <c>PointsPerSecond</c>
    /// and can be clamped to a min / max duration via <c>Duration</c>.
    /// </summary>
    public partial struct ScrollAnimation : IEquatable<ScrollAnimation> {
        /// <summary>
        /// With what speed should we scroll? (Default: 1000.0)
        /// </summary>
        public float PointsPerSecond;
        /// <summary>
        /// The min / max scroll duration.
        /// </summary>
        public Rangef Duration;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_f32(PointsPerSecond);
            Duration.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static ScrollAnimation Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            ScrollAnimation obj = new ScrollAnimation {
            	PointsPerSecond = deserializer.deserialize_f32(),
            	Duration = Rangef.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is ScrollAnimation other && Equals(other);

        public static bool operator ==(ScrollAnimation left, ScrollAnimation right) => Equals(left, right);

        public static bool operator !=(ScrollAnimation left, ScrollAnimation right) => !Equals(left, right);

        public bool Equals(ScrollAnimation other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!PointsPerSecond.Equals(other.PointsPerSecond)) return false;
            if (!Duration.Equals(other.Duration)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + PointsPerSecond.GetHashCode();
                value = 31 * value + Duration.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
