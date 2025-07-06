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
    /// Use a <c>TextStyle</c> to look up the <c>FontId</c> in <c>TextStyles</c>.
    /// </summary>
    public partial struct Style : IEquatable<Style> {
        public TextStyle? OverrideTextStyle;
        public FontId? OverrideFontId;
        public Align? OverrideTextValign;
        public ImmutableDictionary<TextStyle, FontId> TextStyles;
        public TextStyle DragValueTextStyle;
        public bool? Wrap;
        public TextWrapMode? WrapMode;
        public Spacing Spacing;
        public Interaction Interaction;
        public Visuals Visuals;
        public float AnimationTime;
        public DebugOptions Debug;
        public bool ExplanationTooltips;
        public bool UrlInTooltip;
        public bool AlwaysScrollTheOnlyDirection;
        public ScrollAnimation ScrollAnimation;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_option_TextStyle(OverrideTextStyle, serializer);
            TraitHelpers.serialize_option_FontId(OverrideFontId, serializer);
            TraitHelpers.serialize_option_Align(OverrideTextValign, serializer);
            TraitHelpers.serialize_map_TextStyle_to_FontId(TextStyles, serializer);
            DragValueTextStyle.Serialize(serializer);
            TraitHelpers.serialize_option_bool(Wrap, serializer);
            TraitHelpers.serialize_option_TextWrapMode(WrapMode, serializer);
            Spacing.Serialize(serializer);
            Interaction.Serialize(serializer);
            Visuals.Serialize(serializer);
            serializer.serialize_f32(AnimationTime);
            Debug.Serialize(serializer);
            serializer.serialize_bool(ExplanationTooltips);
            serializer.serialize_bool(UrlInTooltip);
            serializer.serialize_bool(AlwaysScrollTheOnlyDirection);
            ScrollAnimation.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static Style Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Style obj = new Style {
            	OverrideTextStyle = TraitHelpers.deserialize_option_TextStyle(deserializer),
            	OverrideFontId = TraitHelpers.deserialize_option_FontId(deserializer),
            	OverrideTextValign = TraitHelpers.deserialize_option_Align(deserializer),
            	TextStyles = TraitHelpers.deserialize_map_TextStyle_to_FontId(deserializer),
            	DragValueTextStyle = TextStyle.Deserialize(deserializer),
            	Wrap = TraitHelpers.deserialize_option_bool(deserializer),
            	WrapMode = TraitHelpers.deserialize_option_TextWrapMode(deserializer),
            	Spacing = Spacing.Deserialize(deserializer),
            	Interaction = Interaction.Deserialize(deserializer),
            	Visuals = Visuals.Deserialize(deserializer),
            	AnimationTime = deserializer.deserialize_f32(),
            	Debug = DebugOptions.Deserialize(deserializer),
            	ExplanationTooltips = deserializer.deserialize_bool(),
            	UrlInTooltip = deserializer.deserialize_bool(),
            	AlwaysScrollTheOnlyDirection = deserializer.deserialize_bool(),
            	ScrollAnimation = ScrollAnimation.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Style other && Equals(other);

        public static bool operator ==(Style left, Style right) => Equals(left, right);

        public static bool operator !=(Style left, Style right) => !Equals(left, right);

        public bool Equals(Style other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!OverrideTextStyle.Equals(other.OverrideTextStyle)) return false;
            if (!OverrideFontId.Equals(other.OverrideFontId)) return false;
            if (!OverrideTextValign.Equals(other.OverrideTextValign)) return false;
            if (!TextStyles.Equals(other.TextStyles)) return false;
            if (!DragValueTextStyle.Equals(other.DragValueTextStyle)) return false;
            if (!Wrap.Equals(other.Wrap)) return false;
            if (!WrapMode.Equals(other.WrapMode)) return false;
            if (!Spacing.Equals(other.Spacing)) return false;
            if (!Interaction.Equals(other.Interaction)) return false;
            if (!Visuals.Equals(other.Visuals)) return false;
            if (!AnimationTime.Equals(other.AnimationTime)) return false;
            if (!Debug.Equals(other.Debug)) return false;
            if (!ExplanationTooltips.Equals(other.ExplanationTooltips)) return false;
            if (!UrlInTooltip.Equals(other.UrlInTooltip)) return false;
            if (!AlwaysScrollTheOnlyDirection.Equals(other.AlwaysScrollTheOnlyDirection)) return false;
            if (!ScrollAnimation.Equals(other.ScrollAnimation)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + OverrideTextStyle.GetHashCode();
                value = 31 * value + OverrideFontId.GetHashCode();
                value = 31 * value + OverrideTextValign.GetHashCode();
                value = 31 * value + TextStyles.GetHashCode();
                value = 31 * value + DragValueTextStyle.GetHashCode();
                value = 31 * value + Wrap.GetHashCode();
                value = 31 * value + WrapMode.GetHashCode();
                value = 31 * value + Spacing.GetHashCode();
                value = 31 * value + Interaction.GetHashCode();
                value = 31 * value + Visuals.GetHashCode();
                value = 31 * value + AnimationTime.GetHashCode();
                value = 31 * value + Debug.GetHashCode();
                value = 31 * value + ExplanationTooltips.GetHashCode();
                value = 31 * value + UrlInTooltip.GetHashCode();
                value = 31 * value + AlwaysScrollTheOnlyDirection.GetHashCode();
                value = 31 * value + ScrollAnimation.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
