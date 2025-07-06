#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct OutputCommand : IEquatable<OutputCommand> {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static OutputCommand Deserialize(Serde.IDeserializer deserializer) {
            int index = deserializer.deserialize_variant_index();
            switch (index) {
                case 0: return CopyText.Load(deserializer);
                case 1: return CopyImage.Load(deserializer);
                case 2: return OpenUrl.Load(deserializer);
                default: throw new Serde.DeserializationException("Unknown variant index for OutputCommand: " + index);
            }
        }
        public override int GetHashCode() {
            switch (this) {
            case CopyText x: return x.GetHashCode();
            case CopyImage x: return x.GetHashCode();
            case OpenUrl x: return x.GetHashCode();
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }
        public override bool Equals(object? obj) => obj is OutputCommand other && Equals(other);

        public bool Equals(OutputCommand other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            switch (this) {
            case CopyText x: return x.Equals((CopyText)other);
            case CopyImage x: return x.Equals((CopyImage)other);
            case OpenUrl x: return x.Equals((OpenUrl)other);
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }

        /// <summary>Creates a shallow clone of the object.</summary>
        public OutputCommand Clone() => (OutputCommand)MemberwiseClone();

    }


} // end of namespace Egui
