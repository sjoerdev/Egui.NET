#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct KeyboardShortcut : IEquatable<KeyboardShortcut> {
        public Modifiers Modifiers;
        public Key LogicalKey;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Modifiers.Serialize(serializer);
            LogicalKey.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static KeyboardShortcut Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            KeyboardShortcut obj = new KeyboardShortcut {
            	Modifiers = Modifiers.Deserialize(deserializer),
            	LogicalKey = KeyExtensions.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is KeyboardShortcut other && Equals(other);

        public static bool operator ==(KeyboardShortcut left, KeyboardShortcut right) => Equals(left, right);

        public static bool operator !=(KeyboardShortcut left, KeyboardShortcut right) => !Equals(left, right);

        public bool Equals(KeyboardShortcut other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Modifiers.Equals(other.Modifiers)) return false;
            if (!LogicalKey.Equals(other.LogicalKey)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Modifiers.GetHashCode();
                value = 31 * value + LogicalKey.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
