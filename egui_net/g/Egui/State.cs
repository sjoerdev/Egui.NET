#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct State : IEquatable<State> {
        public Vec2 Offset;
        public ImmutableList<ScrollingToTarget?> OffsetTarget;
        public Vec2b ShowScroll;
        public Vec2b ContentIsTooLarge;
        public Vec2b ScrollBarInteraction;
        public ImmutableList<float?> ScrollStartOffsetFromTopLeft;
        public Vec2b ScrollStuckToEnd;
        public Rect? InteractRect;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Offset.Serialize(serializer);
            TraitHelpers.serialize_array2_option_ScrollingToTarget_array(OffsetTarget, serializer);
            ShowScroll.Serialize(serializer);
            ContentIsTooLarge.Serialize(serializer);
            ScrollBarInteraction.Serialize(serializer);
            TraitHelpers.serialize_array2_option_f32_array(ScrollStartOffsetFromTopLeft, serializer);
            ScrollStuckToEnd.Serialize(serializer);
            TraitHelpers.serialize_option_Rect(InteractRect, serializer);
            serializer.decrease_container_depth();
        }

        internal static State Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            State obj = new State {
            	Offset = Vec2.Deserialize(deserializer),
            	OffsetTarget = TraitHelpers.deserialize_array2_option_ScrollingToTarget_array(deserializer),
            	ShowScroll = Vec2b.Deserialize(deserializer),
            	ContentIsTooLarge = Vec2b.Deserialize(deserializer),
            	ScrollBarInteraction = Vec2b.Deserialize(deserializer),
            	ScrollStartOffsetFromTopLeft = TraitHelpers.deserialize_array2_option_f32_array(deserializer),
            	ScrollStuckToEnd = Vec2b.Deserialize(deserializer),
            	InteractRect = TraitHelpers.deserialize_option_Rect(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is State other && Equals(other);

        public static bool operator ==(State left, State right) => Equals(left, right);

        public static bool operator !=(State left, State right) => !Equals(left, right);

        public bool Equals(State other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Offset.Equals(other.Offset)) return false;
            if (!OffsetTarget.Equals(other.OffsetTarget)) return false;
            if (!ShowScroll.Equals(other.ShowScroll)) return false;
            if (!ContentIsTooLarge.Equals(other.ContentIsTooLarge)) return false;
            if (!ScrollBarInteraction.Equals(other.ScrollBarInteraction)) return false;
            if (!ScrollStartOffsetFromTopLeft.Equals(other.ScrollStartOffsetFromTopLeft)) return false;
            if (!ScrollStuckToEnd.Equals(other.ScrollStuckToEnd)) return false;
            if (!InteractRect.Equals(other.InteractRect)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Offset.GetHashCode();
                value = 31 * value + OffsetTarget.GetHashCode();
                value = 31 * value + ShowScroll.GetHashCode();
                value = 31 * value + ContentIsTooLarge.GetHashCode();
                value = 31 * value + ScrollBarInteraction.GetHashCode();
                value = 31 * value + ScrollStartOffsetFromTopLeft.GetHashCode();
                value = 31 * value + ScrollStuckToEnd.GetHashCode();
                value = 31 * value + InteractRect.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
