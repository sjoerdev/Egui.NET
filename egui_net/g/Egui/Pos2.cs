#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Pos2 : IEquatable<Pos2> {
        public float X;
        public float Y;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_f32(X);
            serializer.serialize_f32(Y);
            serializer.decrease_container_depth();
        }

        internal static Pos2 Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Pos2 obj = new Pos2 {
            	X = deserializer.deserialize_f32(),
            	Y = deserializer.deserialize_f32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Pos2 other && Equals(other);

        public static bool operator ==(Pos2 left, Pos2 right) => Equals(left, right);

        public static bool operator !=(Pos2 left, Pos2 right) => !Equals(left, right);

        public bool Equals(Pos2 other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!X.Equals(other.X)) return false;
            if (!Y.Equals(other.Y)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + X.GetHashCode();
                value = 31 * value + Y.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
