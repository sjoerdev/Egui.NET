#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct CCursor : IEquatable<CCursor> {
        public ulong Index;
        public bool PreferNextRow;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_u64(Index);
            serializer.serialize_bool(PreferNextRow);
            serializer.decrease_container_depth();
        }

        internal static CCursor Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            CCursor obj = new CCursor {
            	Index = deserializer.deserialize_u64(),
            	PreferNextRow = deserializer.deserialize_bool() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is CCursor other && Equals(other);

        public static bool operator ==(CCursor left, CCursor right) => Equals(left, right);

        public static bool operator !=(CCursor left, CCursor right) => !Equals(left, right);

        public bool Equals(CCursor other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Index.Equals(other.Index)) return false;
            if (!PreferNextRow.Equals(other.PreferNextRow)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Index.GetHashCode();
                value = 31 * value + PreferNextRow.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
