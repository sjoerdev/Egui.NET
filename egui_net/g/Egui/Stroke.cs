#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Stroke : IEquatable<Stroke> {
        public float Width;
        public Color32 Color;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_f32(Width);
            Color.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static Stroke Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Stroke obj = new Stroke {
            	Width = deserializer.deserialize_f32(),
            	Color = Color32.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Stroke other && Equals(other);

        public static bool operator ==(Stroke left, Stroke right) => Equals(left, right);

        public static bool operator !=(Stroke left, Stroke right) => !Equals(left, right);

        public bool Equals(Stroke other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Width.Equals(other.Width)) return false;
            if (!Color.Equals(other.Color)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Width.GetHashCode();
                value = 31 * value + Color.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
