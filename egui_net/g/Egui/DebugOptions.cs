#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct DebugOptions : IEquatable<DebugOptions> {
        public bool DebugOnHover;
        public bool DebugOnHoverWithAllModifiers;
        public bool HoverShowsNext;
        public bool ShowExpandWidth;
        public bool ShowExpandHeight;
        public bool ShowResize;
        public bool ShowInteractiveWidgets;
        public bool ShowWidgetHits;
        public bool ShowUnaligned;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_bool(DebugOnHover);
            serializer.serialize_bool(DebugOnHoverWithAllModifiers);
            serializer.serialize_bool(HoverShowsNext);
            serializer.serialize_bool(ShowExpandWidth);
            serializer.serialize_bool(ShowExpandHeight);
            serializer.serialize_bool(ShowResize);
            serializer.serialize_bool(ShowInteractiveWidgets);
            serializer.serialize_bool(ShowWidgetHits);
            serializer.serialize_bool(ShowUnaligned);
            serializer.decrease_container_depth();
        }

        internal static DebugOptions Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            DebugOptions obj = new DebugOptions {
            	DebugOnHover = deserializer.deserialize_bool(),
            	DebugOnHoverWithAllModifiers = deserializer.deserialize_bool(),
            	HoverShowsNext = deserializer.deserialize_bool(),
            	ShowExpandWidth = deserializer.deserialize_bool(),
            	ShowExpandHeight = deserializer.deserialize_bool(),
            	ShowResize = deserializer.deserialize_bool(),
            	ShowInteractiveWidgets = deserializer.deserialize_bool(),
            	ShowWidgetHits = deserializer.deserialize_bool(),
            	ShowUnaligned = deserializer.deserialize_bool() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is DebugOptions other && Equals(other);

        public static bool operator ==(DebugOptions left, DebugOptions right) => Equals(left, right);

        public static bool operator !=(DebugOptions left, DebugOptions right) => !Equals(left, right);

        public bool Equals(DebugOptions other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!DebugOnHover.Equals(other.DebugOnHover)) return false;
            if (!DebugOnHoverWithAllModifiers.Equals(other.DebugOnHoverWithAllModifiers)) return false;
            if (!HoverShowsNext.Equals(other.HoverShowsNext)) return false;
            if (!ShowExpandWidth.Equals(other.ShowExpandWidth)) return false;
            if (!ShowExpandHeight.Equals(other.ShowExpandHeight)) return false;
            if (!ShowResize.Equals(other.ShowResize)) return false;
            if (!ShowInteractiveWidgets.Equals(other.ShowInteractiveWidgets)) return false;
            if (!ShowWidgetHits.Equals(other.ShowWidgetHits)) return false;
            if (!ShowUnaligned.Equals(other.ShowUnaligned)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + DebugOnHover.GetHashCode();
                value = 31 * value + DebugOnHoverWithAllModifiers.GetHashCode();
                value = 31 * value + HoverShowsNext.GetHashCode();
                value = 31 * value + ShowExpandWidth.GetHashCode();
                value = 31 * value + ShowExpandHeight.GetHashCode();
                value = 31 * value + ShowResize.GetHashCode();
                value = 31 * value + ShowInteractiveWidgets.GetHashCode();
                value = 31 * value + ShowWidgetHits.GetHashCode();
                value = 31 * value + ShowUnaligned.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
