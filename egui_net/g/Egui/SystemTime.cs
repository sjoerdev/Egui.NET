#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct SystemTime : IEquatable<SystemTime> {
        public ulong SecsSinceEpoch;
        public uint NanosSinceEpoch;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_u64(SecsSinceEpoch);
            serializer.serialize_u32(NanosSinceEpoch);
            serializer.decrease_container_depth();
        }

        internal static SystemTime Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            SystemTime obj = new SystemTime {
            	SecsSinceEpoch = deserializer.deserialize_u64(),
            	NanosSinceEpoch = deserializer.deserialize_u32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is SystemTime other && Equals(other);

        public static bool operator ==(SystemTime left, SystemTime right) => Equals(left, right);

        public static bool operator !=(SystemTime left, SystemTime right) => !Equals(left, right);

        public bool Equals(SystemTime other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!SecsSinceEpoch.Equals(other.SecsSinceEpoch)) return false;
            if (!NanosSinceEpoch.Equals(other.NanosSinceEpoch)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + SecsSinceEpoch.GetHashCode();
                value = 31 * value + NanosSinceEpoch.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
