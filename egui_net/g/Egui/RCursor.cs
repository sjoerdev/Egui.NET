#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct RCursor : IEquatable<RCursor> {
        public ulong Row;
        public ulong Column;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_u64(Row);
            serializer.serialize_u64(Column);
            serializer.decrease_container_depth();
        }

        internal static RCursor Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            RCursor obj = new RCursor {
            	Row = deserializer.deserialize_u64(),
            	Column = deserializer.deserialize_u64() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is RCursor other && Equals(other);

        public static bool operator ==(RCursor left, RCursor right) => Equals(left, right);

        public static bool operator !=(RCursor left, RCursor right) => !Equals(left, right);

        public bool Equals(RCursor other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Row.Equals(other.Row)) return false;
            if (!Column.Equals(other.Column)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Row.GetHashCode();
                value = 31 * value + Column.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
