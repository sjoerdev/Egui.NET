#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct TouchDeviceId : IEquatable<TouchDeviceId> {
        public ulong Value;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_u64(Value);
            serializer.decrease_container_depth();
        }

        internal static TouchDeviceId Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            TouchDeviceId obj = new TouchDeviceId {
            	Value = deserializer.deserialize_u64() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is TouchDeviceId other && Equals(other);

        public static bool operator ==(TouchDeviceId left, TouchDeviceId right) => Equals(left, right);

        public static bool operator !=(TouchDeviceId left, TouchDeviceId right) => !Equals(left, right);

        public bool Equals(TouchDeviceId other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Value.Equals(other.Value)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Value.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
