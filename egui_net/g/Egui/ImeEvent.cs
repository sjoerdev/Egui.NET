#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct ImeEvent : IEquatable<ImeEvent> {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static ImeEvent Deserialize(Serde.IDeserializer deserializer) {
            int index = deserializer.deserialize_variant_index();
            switch (index) {
                case 0: return Enabled.Load(deserializer);
                case 1: return Preedit.Load(deserializer);
                case 2: return Commit.Load(deserializer);
                case 3: return Disabled.Load(deserializer);
                default: throw new Serde.DeserializationException("Unknown variant index for ImeEvent: " + index);
            }
        }
        public override int GetHashCode() {
            switch (this) {
            case Enabled x: return x.GetHashCode();
            case Preedit x: return x.GetHashCode();
            case Commit x: return x.GetHashCode();
            case Disabled x: return x.GetHashCode();
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }
        public override bool Equals(object? obj) => obj is ImeEvent other && Equals(other);

        public bool Equals(ImeEvent other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            switch (this) {
            case Enabled x: return x.Equals((Enabled)other);
            case Preedit x: return x.Equals((Preedit)other);
            case Commit x: return x.Equals((Commit)other);
            case Disabled x: return x.Equals((Disabled)other);
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }

        /// <summary>Creates a shallow clone of the object.</summary>
        public ImeEvent Clone() => (ImeEvent)MemberwiseClone();

    }


} // end of namespace Egui
