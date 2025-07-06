#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    /// <summary>
    /// A selected text range (could be a range of length zero).
    /// </summary>
    public partial struct CursorRange : IEquatable<CursorRange> {
        /// <summary>
        /// When selecting with a mouse, this is where the mouse was released.
        /// When moving with e.g. shift+arrows, this is what moves.
        /// Note that the two ends can come in any order, and also be equal (no selection).
        /// </summary>
        public Cursor Primary;
        /// <summary>
        /// When selecting with a mouse, this is where the mouse was first pressed.
        /// This part of the cursor does not move when shift is down.
        /// </summary>
        public Cursor Secondary;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Primary.Serialize(serializer);
            Secondary.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static CursorRange Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            CursorRange obj = new CursorRange {
            	Primary = Cursor.Deserialize(deserializer),
            	Secondary = Cursor.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is CursorRange other && Equals(other);

        public static bool operator ==(CursorRange left, CursorRange right) => Equals(left, right);

        public static bool operator !=(CursorRange left, CursorRange right) => !Equals(left, right);

        public bool Equals(CursorRange other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Primary.Equals(other.Primary)) return false;
            if (!Secondary.Equals(other.Secondary)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Primary.GetHashCode();
                value = 31 * value + Secondary.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
