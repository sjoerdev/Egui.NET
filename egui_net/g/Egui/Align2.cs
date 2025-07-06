#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Align2 : IEquatable<Align2> {
        public ImmutableList<Align> Value;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_array2_Align_array(Value, serializer);
            serializer.decrease_container_depth();
        }

        internal static Align2 Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Align2 obj = new Align2 {
            	Value = TraitHelpers.deserialize_array2_Align_array(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Align2 other && Equals(other);

        public static bool operator ==(Align2 left, Align2 right) => Equals(left, right);

        public static bool operator !=(Align2 left, Align2 right) => !Equals(left, right);

        public bool Equals(Align2 other) {
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
