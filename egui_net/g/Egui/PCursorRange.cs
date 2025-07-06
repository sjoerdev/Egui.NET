#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct PCursorRange : IEquatable<PCursorRange> {
        public PCursor Primary;
        public PCursor Secondary;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Primary.Serialize(serializer);
            Secondary.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static PCursorRange Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            PCursorRange obj = new PCursorRange {
            	Primary = PCursor.Deserialize(deserializer),
            	Secondary = PCursor.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is PCursorRange other && Equals(other);

        public static bool operator ==(PCursorRange left, PCursorRange right) => Equals(left, right);

        public static bool operator !=(PCursorRange left, PCursorRange right) => !Equals(left, right);

        public bool Equals(PCursorRange other) {
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
