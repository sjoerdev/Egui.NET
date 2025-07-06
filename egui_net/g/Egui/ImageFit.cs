#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct ImageFit : IEquatable<ImageFit> {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static ImageFit Deserialize(Serde.IDeserializer deserializer) {
            int index = deserializer.deserialize_variant_index();
            switch (index) {
                case 0: return Original.Load(deserializer);
                case 1: return Fraction.Load(deserializer);
                case 2: return Exact.Load(deserializer);
                default: throw new Serde.DeserializationException("Unknown variant index for ImageFit: " + index);
            }
        }
        public override int GetHashCode() {
            switch (this) {
            case Original x: return x.GetHashCode();
            case Fraction x: return x.GetHashCode();
            case Exact x: return x.GetHashCode();
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }
        public override bool Equals(object? obj) => obj is ImageFit other && Equals(other);

        public bool Equals(ImageFit other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            switch (this) {
            case Original x: return x.Equals((Original)other);
            case Fraction x: return x.Equals((Fraction)other);
            case Exact x: return x.Equals((Exact)other);
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }

        /// <summary>Creates a shallow clone of the object.</summary>
        public ImageFit Clone() => (ImageFit)MemberwiseClone();

    }


} // end of namespace Egui
