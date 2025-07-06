#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Color32 : IEquatable<Color32> {
        public ReadOnlyMemory<byte> Value;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_array4_u8_array(Value, serializer);
            serializer.decrease_container_depth();
        }

        internal static Color32 Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Color32 obj = new Color32 {
            	Value = TraitHelpers.deserialize_array4_u8_array(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Color32 other && Equals(other);

        public static bool operator ==(Color32 left, Color32 right) => Equals(left, right);

        public static bool operator !=(Color32 left, Color32 right) => !Equals(left, right);

        public bool Equals(Color32 other) {
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
