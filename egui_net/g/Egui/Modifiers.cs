#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Modifiers : IEquatable<Modifiers> {
        public bool Alt;
        public bool Ctrl;
        public bool Shift;
        public bool MacCmd;
        public bool Command;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_bool(Alt);
            serializer.serialize_bool(Ctrl);
            serializer.serialize_bool(Shift);
            serializer.serialize_bool(MacCmd);
            serializer.serialize_bool(Command);
            serializer.decrease_container_depth();
        }

        internal static Modifiers Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Modifiers obj = new Modifiers {
            	Alt = deserializer.deserialize_bool(),
            	Ctrl = deserializer.deserialize_bool(),
            	Shift = deserializer.deserialize_bool(),
            	MacCmd = deserializer.deserialize_bool(),
            	Command = deserializer.deserialize_bool() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Modifiers other && Equals(other);

        public static bool operator ==(Modifiers left, Modifiers right) => Equals(left, right);

        public static bool operator !=(Modifiers left, Modifiers right) => !Equals(left, right);

        public bool Equals(Modifiers other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Alt.Equals(other.Alt)) return false;
            if (!Ctrl.Equals(other.Ctrl)) return false;
            if (!Shift.Equals(other.Shift)) return false;
            if (!MacCmd.Equals(other.MacCmd)) return false;
            if (!Command.Equals(other.Command)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Alt.GetHashCode();
                value = 31 * value + Ctrl.GetHashCode();
                value = 31 * value + Shift.GetHashCode();
                value = 31 * value + MacCmd.GetHashCode();
                value = 31 * value + Command.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
