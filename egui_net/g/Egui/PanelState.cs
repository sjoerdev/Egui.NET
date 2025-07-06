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
    /// State regarding panels.
    /// </summary>
    public partial struct PanelState : IEquatable<PanelState> {
        public Rect Rect;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Rect.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static PanelState Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            PanelState obj = new PanelState {
            	Rect = Rect.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is PanelState other && Equals(other);

        public static bool operator ==(PanelState left, PanelState right) => Equals(left, right);

        public static bool operator !=(PanelState left, PanelState right) => !Equals(left, right);

        public bool Equals(PanelState other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Rect.Equals(other.Rect)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Rect.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
