#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct FontId : IEquatable<FontId> {
        public float Size;
        public FontFamily Family;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_f32(Size);
            Family.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static FontId Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            FontId obj = new FontId {
            	Size = deserializer.deserialize_f32(),
            	Family = FontFamily.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is FontId other && Equals(other);

        public static bool operator ==(FontId left, FontId right) => Equals(left, right);

        public static bool operator !=(FontId left, FontId right) => !Equals(left, right);

        public bool Equals(FontId other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Size.Equals(other.Size)) return false;
            if (!Family.Equals(other.Family)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Size.GetHashCode();
                value = 31 * value + Family.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
