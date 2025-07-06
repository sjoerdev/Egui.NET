#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Shadow : IEquatable<Shadow> {
        public ImmutableList<sbyte> Offset;
        public byte Blur;
        public byte Spread;
        public Color32 Color;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_array2_i8_array(Offset, serializer);
            serializer.serialize_u8(Blur);
            serializer.serialize_u8(Spread);
            Color.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static Shadow Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Shadow obj = new Shadow {
            	Offset = TraitHelpers.deserialize_array2_i8_array(deserializer),
            	Blur = deserializer.deserialize_u8(),
            	Spread = deserializer.deserialize_u8(),
            	Color = Color32.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Shadow other && Equals(other);

        public static bool operator ==(Shadow left, Shadow right) => Equals(left, right);

        public static bool operator !=(Shadow left, Shadow right) => !Equals(left, right);

        public bool Equals(Shadow other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Offset.Equals(other.Offset)) return false;
            if (!Blur.Equals(other.Blur)) return false;
            if (!Spread.Equals(other.Spread)) return false;
            if (!Color.Equals(other.Color)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Offset.GetHashCode();
                value = 31 * value + Blur.GetHashCode();
                value = 31 * value + Spread.GetHashCode();
                value = 31 * value + Color.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
