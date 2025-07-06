#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Rangef : IEquatable<Rangef> {
        public float Min;
        public float Max;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_f32(Min);
            serializer.serialize_f32(Max);
            serializer.decrease_container_depth();
        }

        internal static Rangef Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Rangef obj = new Rangef {
            	Min = deserializer.deserialize_f32(),
            	Max = deserializer.deserialize_f32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Rangef other && Equals(other);

        public static bool operator ==(Rangef left, Rangef right) => Equals(left, right);

        public static bool operator !=(Rangef left, Rangef right) => !Equals(left, right);

        public bool Equals(Rangef other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Min.Equals(other.Min)) return false;
            if (!Max.Equals(other.Max)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Min.GetHashCode();
                value = 31 * value + Max.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
