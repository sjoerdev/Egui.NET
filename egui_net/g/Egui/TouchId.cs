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
    /// Unique identification of a touch occurrence (finger or pen or …).
    /// A Touch ID is valid until the finger is lifted.
    /// A new ID is used for the next touch.
    /// </summary>
    public partial struct TouchId : IEquatable<TouchId> {
        public ulong Value;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_u64(Value);
            serializer.decrease_container_depth();
        }

        internal static TouchId Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            TouchId obj = new TouchId {
            	Value = deserializer.deserialize_u64() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is TouchId other && Equals(other);

        public static bool operator ==(TouchId left, TouchId right) => Equals(left, right);

        public static bool operator !=(TouchId left, TouchId right) => !Equals(left, right);

        public bool Equals(TouchId other) {
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
