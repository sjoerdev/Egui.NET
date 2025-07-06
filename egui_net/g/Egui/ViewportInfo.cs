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
    /// Information about the current viewport, given as input each frame.
    ///
    /// <c>None</c> means "unknown".
    ///
    /// All units are in ui "points", which can be calculated from native physical pixels
    /// using <c>PixelsPerPoint</c> = <c>ZoomFactor</c> * <c>NativePixelsPerPoint</c>;
    /// </summary>
    public partial struct ViewportInfo : IEquatable<ViewportInfo> {
        /// <summary>
        /// Parent viewport, if known.
        /// </summary>
        public ViewportId? Parent;
        /// <summary>
        /// Name of the viewport, if known.
        /// </summary>
        public string? Title;
        public ImmutableList<ViewportEvent> Events;
        /// <summary>
        /// The OS native pixels-per-point.
        ///
        /// This should always be set, if known.
        ///
        /// On web this takes browser scaling into account,
        /// and corresponds to <c>Window.devicePixelRatio</c> in JavaScript.
        /// </summary>
        public float? NativePixelsPerPoint;
        /// <summary>
        /// Current monitor size in egui points.
        /// </summary>
        public Vec2? MonitorSize;
        /// <summary>
        /// The inner rectangle of the native window, in monitor space and ui points scale.
        ///
        /// This is the content rectangle of the viewport.
        /// </summary>
        public Rect? InnerRect;
        /// <summary>
        /// The outer rectangle of the native window, in monitor space and ui points scale.
        ///
        /// This is the content rectangle plus decoration chrome.
        /// </summary>
        public Rect? OuterRect;
        /// <summary>
        /// Are we minimized?
        /// </summary>
        public bool? Minimized;
        /// <summary>
        /// Are we maximized?
        /// </summary>
        public bool? Maximized;
        /// <summary>
        /// Are we in fullscreen mode?
        /// </summary>
        public bool? Fullscreen;
        /// <summary>
        /// Is the window focused and able to receive input?
        ///
        /// This should be the same as <c>Focused</c>.
        /// </summary>
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
