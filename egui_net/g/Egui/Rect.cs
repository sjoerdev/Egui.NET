#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Rect : IEquatable<Rect> {
        public Pos2 Min;
        public Pos2 Max;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Min.Serialize(serializer);
            Max.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static Rect Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Rect obj = new Rect {
            	Min = Pos2.Deserialize(deserializer),
            	Max = Pos2.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Rect other && Equals(other);

        public static bool operator ==(Rect left, Rect right) => Equals(left, right);

        public static bool operator !=(Rect left, Rect right) => !Equals(left, right);

        public bool Equals(Rect other) {
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
