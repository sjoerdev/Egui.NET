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
    /// State of the modifier keys. These must be fed to egui.
    ///
    /// The best way to compare <c>Modifiers</c> is by using <c>Matches</c>.
    ///
    /// NOTE: For cross-platform uses, ALT+SHIFT is a bad combination of modifiers
    /// as on mac that is how you type special characters,
    /// so those key presses are usually not reported to egui.
    /// </summary>
    public partial struct Modifiers : IEquatable<Modifiers> {
        /// <summary>
        /// Either of the alt keys are down (option ⌥ on Mac).
        /// </summary>
        public bool Alt;
        /// <summary>
        /// Either of the control keys are down.
        /// When checking for keyboard shortcuts, consider using <c>Command</c> instead.
        /// </summary>
        public bool Ctrl;
        /// <summary>
        /// Either of the shift keys are down.
        /// </summary>
        public bool Shift;
        /// <summary>
        /// The Mac ⌘ Command key. Should always be set to <c>False</c> on other platforms.
        /// </summary>
        public bool MacCmd;
        /// <summary>
        /// On Windows and Linux, set this to the same value as <c>Ctrl</c>.
        /// On Mac, this should be set whenever one of the ⌘ Command keys are down (same as <c>MacCmd</c>).
        /// This is so that egui can, for instance, select all text by checking for <c>Command+A</c>
        /// and it will work on both Mac and Windows.
        /// </summary>
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
