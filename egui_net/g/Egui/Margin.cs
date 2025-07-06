#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Margin : IEquatable<Margin> {
        public sbyte Left;
        public sbyte Right;
        public sbyte Top;
        public sbyte Bottom;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_i8(Left);
            serializer.serialize_i8(Right);
            serializer.serialize_i8(Top);
            serializer.serialize_i8(Bottom);
            serializer.decrease_container_depth();
        }

        internal static Margin Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Margin obj = new Margin {
            	Left = deserializer.deserialize_i8(),
            	Right = deserializer.deserialize_i8(),
            	Top = deserializer.deserialize_i8(),
            	Bottom = deserializer.deserialize_i8() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Margin other && Equals(other);

        public static bool operator ==(Margin left, Margin right) => Equals(left, right);

        public static bool operator !=(Margin left, Margin right) => !Equals(left, right);

        public bool Equals(Margin other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Left.Equals(other.Left)) return false;
            if (!Right.Equals(other.Right)) return false;
            if (!Top.Equals(other.Top)) return false;
            if (!Bottom.Equals(other.Bottom)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Left.GetHashCode();
                value = 31 * value + Right.GetHashCode();
                value = 31 * value + Top.GetHashCode();
                value = 31 * value + Bottom.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
