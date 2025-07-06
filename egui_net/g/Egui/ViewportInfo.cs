#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct ViewportInfo : IEquatable<ViewportInfo> {
        public ViewportId? Parent;
        public string? Title;
        public ImmutableList<ViewportEvent> Events;
        public float? NativePixelsPerPoint;
        public Vec2? MonitorSize;
        public Rect? InnerRect;
        public Rect? OuterRect;
        public bool? Minimized;
        public bool? Maximized;
        public bool? Fullscreen;
        public bool? Focused;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_option_ViewportId(Parent, serializer);
            TraitHelpers.serialize_option_str(Title, serializer);
            TraitHelpers.serialize_vector_ViewportEvent(Events, serializer);
            TraitHelpers.serialize_option_f32(NativePixelsPerPoint, serializer);
            TraitHelpers.serialize_option_Vec2(MonitorSize, serializer);
            TraitHelpers.serialize_option_Rect(InnerRect, serializer);
            TraitHelpers.serialize_option_Rect(OuterRect, serializer);
            TraitHelpers.serialize_option_bool(Minimized, serializer);
            TraitHelpers.serialize_option_bool(Maximized, serializer);
            TraitHelpers.serialize_option_bool(Fullscreen, serializer);
            TraitHelpers.serialize_option_bool(Focused, serializer);
            serializer.decrease_container_depth();
        }

        internal static ViewportInfo Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            ViewportInfo obj = new ViewportInfo {
            	Parent = TraitHelpers.deserialize_option_ViewportId(deserializer),
            	Title = TraitHelpers.deserialize_option_str(deserializer),
            	Events = TraitHelpers.deserialize_vector_ViewportEvent(deserializer),
            	NativePixelsPerPoint = TraitHelpers.deserialize_option_f32(deserializer),
            	MonitorSize = TraitHelpers.deserialize_option_Vec2(deserializer),
            	InnerRect = TraitHelpers.deserialize_option_Rect(deserializer),
            	OuterRect = TraitHelpers.deserialize_option_Rect(deserializer),
            	Minimized = TraitHelpers.deserialize_option_bool(deserializer),
            	Maximized = TraitHelpers.deserialize_option_bool(deserializer),
            	Fullscreen = TraitHelpers.deserialize_option_bool(deserializer),
            	Focused = TraitHelpers.deserialize_option_bool(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is ViewportInfo other && Equals(other);

        public static bool operator ==(ViewportInfo left, ViewportInfo right) => Equals(left, right);

        public static bool operator !=(ViewportInfo left, ViewportInfo right) => !Equals(left, right);

        public bool Equals(ViewportInfo other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Parent.Equals(other.Parent)) return false;
            if (!Title.Equals(other.Title)) return false;
            if (!Events.Equals(other.Events)) return false;
            if (!NativePixelsPerPoint.Equals(other.NativePixelsPerPoint)) return false;
            if (!MonitorSize.Equals(other.MonitorSize)) return false;
            if (!InnerRect.Equals(other.InnerRect)) return false;
            if (!OuterRect.Equals(other.OuterRect)) return false;
            if (!Minimized.Equals(other.Minimized)) return false;
            if (!Maximized.Equals(other.Maximized)) return false;
            if (!Fullscreen.Equals(other.Fullscreen)) return false;
            if (!Focused.Equals(other.Focused)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Parent.GetHashCode();
                value = 31 * value + Title.GetHashCode();
                value = 31 * value + Events.GetHashCode();
                value = 31 * value + NativePixelsPerPoint.GetHashCode();
                value = 31 * value + MonitorSize.GetHashCode();
                value = 31 * value + InnerRect.GetHashCode();
                value = 31 * value + OuterRect.GetHashCode();
                value = 31 * value + Minimized.GetHashCode();
                value = 31 * value + Maximized.GetHashCode();
                value = 31 * value + Fullscreen.GetHashCode();
                value = 31 * value + Focused.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
