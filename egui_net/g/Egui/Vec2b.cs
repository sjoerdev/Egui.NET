#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Vec2b : IEquatable<Vec2b> {
        public bool X;
        public bool Y;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_bool(X);
            serializer.serialize_bool(Y);
            serializer.decrease_container_depth();
        }

        internal static Vec2b Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Vec2b obj = new Vec2b {
            	X = deserializer.deserialize_bool(),
            	Y = deserializer.deserialize_bool() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Vec2b other && Equals(other);

        public static bool operator ==(Vec2b left, Vec2b right) => Equals(left, right);

        public static bool operator !=(Vec2b left, Vec2b right) => !Equals(left, right);

        public bool Equals(Vec2b other) {
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
