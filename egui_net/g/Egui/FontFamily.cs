#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct FontFamily : IEquatable<FontFamily> {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static FontFamily Deserialize(Serde.IDeserializer deserializer) {
            int index = deserializer.deserialize_variant_index();
            switch (index) {
                case 0: return Proportional.Load(deserializer);
                case 1: return Monospace.Load(deserializer);
                case 2: return Name.Load(deserializer);
                default: throw new Serde.DeserializationException("Unknown variant index for FontFamily: " + index);
            }
        }
        public override int GetHashCode() {
            switch (this) {
            case Proportional x: return x.GetHashCode();
            case Monospace x: return x.GetHashCode();
            case Name x: return x.GetHashCode();
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }
        public override bool Equals(object? obj) => obj is FontFamily other && Equals(other);

        public bool Equals(FontFamily other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            switch (this) {
            case Proportional x: return x.Equals((Proportional)other);
            case Monospace x: return x.Equals((Monospace)other);
            case Name x: return x.Equals((Name)other);
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }

        /// <summary>Creates a shallow clone of the object.</summary>
        public FontFamily Clone() => (FontFamily)MemberwiseClone();

    }


} // end of namespace Egui
