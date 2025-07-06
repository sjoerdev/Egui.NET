#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Rot2 : IEquatable<Rot2> {
        public float S;
        public float C;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_f32(S);
            serializer.serialize_f32(C);
            serializer.decrease_container_depth();
        }

        internal static Rot2 Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Rot2 obj = new Rot2 {
            	S = deserializer.deserialize_f32(),
            	C = deserializer.deserialize_f32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Rot2 other && Equals(other);

        public static bool operator ==(Rot2 left, Rot2 right) => Equals(left, right);

        public static bool operator !=(Rot2 left, Rot2 right) => !Equals(left, right);

        public bool Equals(Rot2 other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!S.Equals(other.S)) return false;
            if (!C.Equals(other.C)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + S.GetHashCode();
                value = 31 * value + C.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
