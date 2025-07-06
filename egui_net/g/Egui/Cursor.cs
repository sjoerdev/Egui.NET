#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Cursor : IEquatable<Cursor> {
        public CCursor Ccursor;
        public RCursor Rcursor;
        public PCursor Pcursor;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Ccursor.Serialize(serializer);
            Rcursor.Serialize(serializer);
            Pcursor.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static Cursor Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Cursor obj = new Cursor {
            	Ccursor = CCursor.Deserialize(deserializer),
            	Rcursor = RCursor.Deserialize(deserializer),
            	Pcursor = PCursor.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Cursor other && Equals(other);

        public static bool operator ==(Cursor left, Cursor right) => Equals(left, right);

        public static bool operator !=(Cursor left, Cursor right) => !Equals(left, right);

        public bool Equals(Cursor other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Ccursor.Equals(other.Ccursor)) return false;
            if (!Rcursor.Equals(other.Rcursor)) return false;
            if (!Pcursor.Equals(other.Pcursor)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Ccursor.GetHashCode();
                value = 31 * value + Rcursor.GetHashCode();
                value = 31 * value + Pcursor.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
