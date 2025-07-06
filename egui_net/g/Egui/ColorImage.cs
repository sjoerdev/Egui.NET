#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct ColorImage : IEquatable<ColorImage> {
        public ImmutableList<ulong> Size;
        public ImmutableList<Color32> Pixels;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_array2_u64_array(Size, serializer);
            TraitHelpers.serialize_vector_Color32(Pixels, serializer);
            serializer.decrease_container_depth();
        }

        internal static ColorImage Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            ColorImage obj = new ColorImage {
            	Size = TraitHelpers.deserialize_array2_u64_array(deserializer),
            	Pixels = TraitHelpers.deserialize_vector_Color32(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is ColorImage other && Equals(other);

        public static bool operator ==(ColorImage left, ColorImage right) => Equals(left, right);

        public static bool operator !=(ColorImage left, ColorImage right) => !Equals(left, right);

        public bool Equals(ColorImage other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Size.Equals(other.Size)) return false;
            if (!Pixels.Equals(other.Pixels)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Size.GetHashCode();
                value = 31 * value + Pixels.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
