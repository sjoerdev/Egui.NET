#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct TextEditState : IEquatable<TextEditState> {
        public TextCursorState Cursor;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Cursor.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static TextEditState Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            TextEditState obj = new TextEditState {
            	Cursor = TextCursorState.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is TextEditState other && Equals(other);

        public static bool operator ==(TextEditState left, TextEditState right) => Equals(left, right);

        public static bool operator !=(TextEditState left, TextEditState right) => !Equals(left, right);

        public bool Equals(TextEditState other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Cursor.Equals(other.Cursor)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Cursor.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
