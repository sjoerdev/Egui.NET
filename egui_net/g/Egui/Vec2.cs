#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Vec2 : IEquatable<Vec2> {
        public float X;
        public float Y;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_f32(X);
            serializer.serialize_f32(Y);
            serializer.decrease_container_depth();
        }

        internal static Vec2 Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Vec2 obj = new Vec2 {
            	X = deserializer.deserialize_f32(),
            	Y = deserializer.deserialize_f32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Vec2 other && Equals(other);

        public static bool operator ==(Vec2 left, Vec2 right) => Equals(left, right);

        public static bool operator !=(Vec2 left, Vec2 right) => !Equals(left, right);

        public bool Equals(Vec2 other) {
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
