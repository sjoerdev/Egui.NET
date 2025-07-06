#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct OutputEvent : IEquatable<OutputEvent> {

        internal void Serialize(Serde.ISerializer serializer) { throw new NotImplementedException(); }

        internal static OutputEvent Deserialize(Serde.IDeserializer deserializer) {
            int index = deserializer.deserialize_variant_index();
            switch (index) {
                case 0: return Clicked.Load(deserializer);
                case 1: return DoubleClicked.Load(deserializer);
                case 2: return TripleClicked.Load(deserializer);
                case 3: return FocusGained.Load(deserializer);
                case 4: return TextSelectionChanged.Load(deserializer);
                case 5: return ValueChanged.Load(deserializer);
                default: throw new Serde.DeserializationException("Unknown variant index for OutputEvent: " + index);
            }
        }
        public override int GetHashCode() {
            switch (this) {
            case Clicked x: return x.GetHashCode();
            case DoubleClicked x: return x.GetHashCode();
            case TripleClicked x: return x.GetHashCode();
            case FocusGained x: return x.GetHashCode();
            case TextSelectionChanged x: return x.GetHashCode();
            case ValueChanged x: return x.GetHashCode();
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }
        public override bool Equals(object? obj) => obj is OutputEvent other && Equals(other);

        public bool Equals(OutputEvent other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            switch (this) {
            case Clicked x: return x.Equals((Clicked)other);
            case DoubleClicked x: return x.Equals((DoubleClicked)other);
            case TripleClicked x: return x.Equals((TripleClicked)other);
            case FocusGained x: return x.Equals((FocusGained)other);
            case TextSelectionChanged x: return x.Equals((TextSelectionChanged)other);
            case ValueChanged x: return x.Equals((ValueChanged)other);
            default: throw new InvalidOperationException("Unknown variant type");
            }
        }

        /// <summary>Creates a shallow clone of the object.</summary>
        public OutputEvent Clone() => (OutputEvent)MemberwiseClone();

    }


} // end of namespace Egui
