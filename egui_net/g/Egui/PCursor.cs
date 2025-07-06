#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct PCursor : IEquatable<PCursor> {
        public ulong Paragraph;
        public ulong Offset;
        public bool PreferNextRow;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_u64(Paragraph);
            serializer.serialize_u64(Offset);
            serializer.serialize_bool(PreferNextRow);
            serializer.decrease_container_depth();
        }

        internal static PCursor Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            PCursor obj = new PCursor {
            	Paragraph = deserializer.deserialize_u64(),
            	Offset = deserializer.deserialize_u64(),
            	PreferNextRow = deserializer.deserialize_bool() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is PCursor other && Equals(other);

        public static bool operator ==(PCursor left, PCursor right) => Equals(left, right);

        public static bool operator !=(PCursor left, PCursor right) => !Equals(left, right);

        public bool Equals(PCursor other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Paragraph.Equals(other.Paragraph)) return false;
            if (!Offset.Equals(other.Offset)) return false;
            if (!PreferNextRow.Equals(other.PreferNextRow)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Paragraph.GetHashCode();
                value = 31 * value + Offset.GetHashCode();
                value = 31 * value + PreferNextRow.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
