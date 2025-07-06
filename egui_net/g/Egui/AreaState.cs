#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct AreaState : IEquatable<AreaState> {
        public Pos2? PivotPos;
        public Align2 Pivot;
        public bool Interactable;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_option_Pos2(PivotPos, serializer);
            Pivot.Serialize(serializer);
            serializer.serialize_bool(Interactable);
            serializer.decrease_container_depth();
        }

        internal static AreaState Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            AreaState obj = new AreaState {
            	PivotPos = TraitHelpers.deserialize_option_Pos2(deserializer),
            	Pivot = Align2.Deserialize(deserializer),
            	Interactable = deserializer.deserialize_bool() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is AreaState other && Equals(other);

        public static bool operator ==(AreaState left, AreaState right) => Equals(left, right);

        public static bool operator !=(AreaState left, AreaState right) => !Equals(left, right);

        public bool Equals(AreaState other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!PivotPos.Equals(other.PivotPos)) return false;
            if (!Pivot.Equals(other.Pivot)) return false;
            if (!Interactable.Equals(other.Interactable)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + PivotPos.GetHashCode();
                value = 31 * value + Pivot.GetHashCode();
                value = 31 * value + Interactable.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
