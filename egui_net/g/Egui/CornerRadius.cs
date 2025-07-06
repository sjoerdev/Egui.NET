#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct CornerRadius : IEquatable<CornerRadius> {
        public byte Nw;
        public byte Ne;
        public byte Sw;
        public byte Se;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_u8(Nw);
            serializer.serialize_u8(Ne);
            serializer.serialize_u8(Sw);
            serializer.serialize_u8(Se);
            serializer.decrease_container_depth();
        }

        internal static CornerRadius Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            CornerRadius obj = new CornerRadius {
            	Nw = deserializer.deserialize_u8(),
            	Ne = deserializer.deserialize_u8(),
            	Sw = deserializer.deserialize_u8(),
            	Se = deserializer.deserialize_u8() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is CornerRadius other && Equals(other);

        public static bool operator ==(CornerRadius left, CornerRadius right) => Equals(left, right);

        public static bool operator !=(CornerRadius left, CornerRadius right) => !Equals(left, right);

        public bool Equals(CornerRadius other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Nw.Equals(other.Nw)) return false;
            if (!Ne.Equals(other.Ne)) return false;
            if (!Sw.Equals(other.Sw)) return false;
            if (!Se.Equals(other.Se)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Nw.GetHashCode();
                value = 31 * value + Ne.GetHashCode();
                value = 31 * value + Sw.GetHashCode();
                value = 31 * value + Se.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
