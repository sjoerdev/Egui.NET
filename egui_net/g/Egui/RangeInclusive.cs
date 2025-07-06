#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct RangeInclusive : IEquatable<RangeInclusive> {
        public ulong Start;
        public ulong End;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_u64(Start);
            serializer.serialize_u64(End);
            serializer.decrease_container_depth();
        }

        internal static RangeInclusive Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            RangeInclusive obj = new RangeInclusive {
            	Start = deserializer.deserialize_u64(),
            	End = deserializer.deserialize_u64() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is RangeInclusive other && Equals(other);

        public static bool operator ==(RangeInclusive left, RangeInclusive right) => Equals(left, right);

        public static bool operator !=(RangeInclusive left, RangeInclusive right) => !Equals(left, right);

        public bool Equals(RangeInclusive other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Start.Equals(other.Start)) return false;
            if (!End.Equals(other.End)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Start.GetHashCode();
                value = 31 * value + End.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
