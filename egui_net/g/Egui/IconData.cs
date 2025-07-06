#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct IconData : IEquatable<IconData> {
        public ReadOnlyMemory<byte> Rgba;
        public uint Width;
        public uint Height;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_vector_u8(Rgba, serializer);
            serializer.serialize_u32(Width);
            serializer.serialize_u32(Height);
            serializer.decrease_container_depth();
        }

        internal static IconData Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            IconData obj = new IconData {
            	Rgba = TraitHelpers.deserialize_vector_u8(deserializer),
            	Width = deserializer.deserialize_u32(),
            	Height = deserializer.deserialize_u32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is IconData other && Equals(other);

        public static bool operator ==(IconData left, IconData right) => Equals(left, right);

        public static bool operator !=(IconData left, IconData right) => !Equals(left, right);

        public bool Equals(IconData other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Rgba.Equals(other.Rgba)) return false;
            if (!Width.Equals(other.Width)) return false;
            if (!Height.Equals(other.Height)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Rgba.GetHashCode();
                value = 31 * value + Width.GetHashCode();
                value = 31 * value + Height.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
