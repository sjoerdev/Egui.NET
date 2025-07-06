#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct TextCursorState : IEquatable<TextCursorState> {
        public CursorRange? CursorRange;
        public CCursorRange? CcursorRange;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_option_CursorRange(CursorRange, serializer);
            TraitHelpers.serialize_option_CCursorRange(CcursorRange, serializer);
            serializer.decrease_container_depth();
        }

        internal static TextCursorState Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            TextCursorState obj = new TextCursorState {
            	CursorRange = TraitHelpers.deserialize_option_CursorRange(deserializer),
            	CcursorRange = TraitHelpers.deserialize_option_CCursorRange(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is TextCursorState other && Equals(other);

        public static bool operator ==(TextCursorState left, TextCursorState right) => Equals(left, right);

        public static bool operator !=(TextCursorState left, TextCursorState right) => !Equals(left, right);

        public bool Equals(TextCursorState other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!CursorRange.Equals(other.CursorRange)) return false;
            if (!CcursorRange.Equals(other.CcursorRange)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + CursorRange.GetHashCode();
                value = 31 * value + CcursorRange.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
