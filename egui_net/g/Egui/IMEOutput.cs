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
    /// Information about text being edited.
    ///
    /// Useful for IME.
    /// </summary>
    public partial struct IMEOutput : IEquatable<IMEOutput> {
        /// <summary>
        /// Where the <c>TextEdit</c> is located on screen.
        /// </summary>
        public Rect Rect;
        /// <summary>
        /// Where the primary cursor is.
        ///
        /// This is a very thin rectangle.
        /// </summary>
        public Rect CursorRect;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Rect.Serialize(serializer);
            CursorRect.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static IMEOutput Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            IMEOutput obj = new IMEOutput {
            	Rect = Rect.Deserialize(deserializer),
            	CursorRect = Rect.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is IMEOutput other && Equals(other);

        public static bool operator ==(IMEOutput left, IMEOutput right) => Equals(left, right);

        public static bool operator !=(IMEOutput left, IMEOutput right) => !Equals(left, right);

        public bool Equals(IMEOutput other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Rect.Equals(other.Rect)) return false;
            if (!CursorRect.Equals(other.CursorRect)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Rect.GetHashCode();
                value = 31 * value + CursorRect.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
