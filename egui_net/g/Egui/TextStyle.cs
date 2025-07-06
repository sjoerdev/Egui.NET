#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct TextStyle : IEquatable<TextStyle> {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static TextStyle Deserialize(Serde.IDeserializer deserializer) {
            int index = deserializer.deserialize_variant_index();
            switch (index) {
                case 0: return Small.Load(deserializer);
                case 1: return Body.Load(deserializer);
                case 2: return Monospace.Load(deserializer);
                case 3: return Button.Load(deserializer);
                case 4: return Heading.Load(deserializer);
                case 5: return Name.Load(deserializer);
                default: throw new Serde.DeserializationException("Unknown variant index for TextStyle: " + index);
            }
        }
        public override int GetHashCode() {
            switch (this) {
            case Small x: return x.GetHashCode();
            case Body x: return x.GetHashCode();
            case Monospace x: return x.GetHashCode();
            case Button x: return x.GetHashCode();
            case Heading x: return x.GetHashCode();
            case Name x: return x.GetHashCode();
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }
        public override bool Equals(object? obj) => obj is TextStyle other && Equals(other);

        public bool Equals(TextStyle other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            switch (this) {
            case Small x: return x.Equals((Small)other);
            case Body x: return x.Equals((Body)other);
            case Monospace x: return x.Equals((Monospace)other);
            case Button x: return x.Equals((Button)other);
            case Heading x: return x.Equals((Heading)other);
            case Name x: return x.Equals((Name)other);
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }

        /// <summary>Creates a shallow clone of the object.</summary>
        public TextStyle Clone() => (TextStyle)MemberwiseClone();

    }


} // end of namespace Egui
