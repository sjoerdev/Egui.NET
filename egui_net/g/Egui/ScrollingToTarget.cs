#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct ScrollingToTarget : IEquatable<ScrollingToTarget> {
        public ImmutableList<double> AnimationTimeSpan;
        public float TargetOffset;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_array2_f64_array(AnimationTimeSpan, serializer);
            serializer.serialize_f32(TargetOffset);
            serializer.decrease_container_depth();
        }

        internal static ScrollingToTarget Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            ScrollingToTarget obj = new ScrollingToTarget {
            	AnimationTimeSpan = TraitHelpers.deserialize_array2_f64_array(deserializer),
            	TargetOffset = deserializer.deserialize_f32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is ScrollingToTarget other && Equals(other);

        public static bool operator ==(ScrollingToTarget left, ScrollingToTarget right) => Equals(left, right);

        public static bool operator !=(ScrollingToTarget left, ScrollingToTarget right) => !Equals(left, right);

        public bool Equals(ScrollingToTarget other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!AnimationTimeSpan.Equals(other.AnimationTimeSpan)) return false;
            if (!TargetOffset.Equals(other.TargetOffset)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + AnimationTimeSpan.GetHashCode();
                value = 31 * value + TargetOffset.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
