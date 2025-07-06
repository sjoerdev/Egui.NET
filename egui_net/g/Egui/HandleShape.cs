#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct HandleShape : IEquatable<HandleShape> {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static HandleShape Deserialize(Serde.IDeserializer deserializer) {
            int index = deserializer.deserialize_variant_index();
            switch (index) {
                case 0: return Circle.Load(deserializer);
                case 1: return Rect.Load(deserializer);
                default: throw new Serde.DeserializationException("Unknown variant index for HandleShape: " + index);
            }
        }
        public override int GetHashCode() {
            switch (this) {
            case Circle x: return x.GetHashCode();
            case Rect x: return x.GetHashCode();
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }
        public override bool Equals(object? obj) => obj is HandleShape other && Equals(other);

        public bool Equals(HandleShape other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            switch (this) {
            case Circle x: return x.Equals((Circle)other);
            case Rect x: return x.Equals((Rect)other);
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }

        /// <summary>Creates a shallow clone of the object.</summary>
        public HandleShape Clone() => (HandleShape)MemberwiseClone();

    }


} // end of namespace Egui
