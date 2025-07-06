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
    /// bg = background, fg = foreground.
    /// </summary>
    public partial struct WidgetVisuals : IEquatable<WidgetVisuals> {
        /// <summary>
        /// Background color of widgets that must have a background fill,
        /// such as the slider background, a checkbox background, or a radio button background.
        ///
        /// Must never be <c>Transparent</c>.
        /// </summary>
        public Color32 BgFill;
        /// <summary>
        /// Background color of widgets that can _optionally_ have a background fill, such as buttons.
        ///
        /// May be <c>Transparent</c>.
        /// </summary>
        public Color32 WeakBgFill;
        /// <summary>
        /// For surrounding rectangle of things that need it,
        /// like buttons, the box of the checkbox, etc.
        /// Should maybe be called <c>FrameStroke</c>.
        /// </summary>
        public Stroke BgStroke;
        /// <summary>
        /// Button frames etc.
        /// </summary>
        public CornerRadius CornerRadius;
        /// <summary>
        /// Stroke and text color of the interactive part of a component (button text, slider grab, check-mark, …).
        /// </summary>
        public Stroke FgStroke;
        /// <summary>
        /// Make the frame this much larger.
        /// </summary>
        public float Expansion;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            BgFill.Serialize(serializer);
            WeakBgFill.Serialize(serializer);
            BgStroke.Serialize(serializer);
            CornerRadius.Serialize(serializer);
            FgStroke.Serialize(serializer);
            serializer.serialize_f32(Expansion);
            serializer.decrease_container_depth();
        }

        internal static WidgetVisuals Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            WidgetVisuals obj = new WidgetVisuals {
            	BgFill = Color32.Deserialize(deserializer),
            	WeakBgFill = Color32.Deserialize(deserializer),
            	BgStroke = Stroke.Deserialize(deserializer),
            	CornerRadius = CornerRadius.Deserialize(deserializer),
            	FgStroke = Stroke.Deserialize(deserializer),
            	Expansion = deserializer.deserialize_f32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is WidgetVisuals other && Equals(other);

        public static bool operator ==(WidgetVisuals left, WidgetVisuals right) => Equals(left, right);

        public static bool operator !=(WidgetVisuals left, WidgetVisuals right) => !Equals(left, right);

        public bool Equals(WidgetVisuals other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!BgFill.Equals(other.BgFill)) return false;
            if (!WeakBgFill.Equals(other.WeakBgFill)) return false;
            if (!BgStroke.Equals(other.BgStroke)) return false;
            if (!CornerRadius.Equals(other.CornerRadius)) return false;
            if (!FgStroke.Equals(other.FgStroke)) return false;
            if (!Expansion.Equals(other.Expansion)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + BgFill.GetHashCode();
                value = 31 * value + WeakBgFill.GetHashCode();
                value = 31 * value + BgStroke.GetHashCode();
                value = 31 * value + CornerRadius.GetHashCode();
                value = 31 * value + FgStroke.GetHashCode();
                value = 31 * value + Expansion.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
