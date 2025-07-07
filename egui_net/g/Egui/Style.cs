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
    /// Specifies the look and feel of egui.
    ///
    /// You can change the visuals of a <c>Ui</c> with <c>StyleMut</c>
    /// and of everything with <c>SetStyleOf</c>.
    /// To choose between dark and light style, use <c>SetTheme</c>.
    ///
    /// If you want to change fonts, use <c>SetFonts</c> instead.
    /// </summary>
    public partial struct Style : IEquatable<Style> {
        /// <summary>
        /// If set this will change the default <c>TextStyle</c> for all widgets.
        ///
        /// On most widgets you can also set an explicit text style,
        /// which will take precedence over this.
        /// </summary>
        public TextStyle? OverrideTextStyle;
        /// <summary>
        /// If set this will change the font family and size for all widgets.
        ///
        /// On most widgets you can also set an explicit text style,
        /// which will take precedence over this.
        /// </summary>
        public FontId? OverrideFontId;
        /// <summary>
        /// How to vertically align text.
        ///
        /// Set to <c>None</c> to use align that depends on the current layout.
        /// </summary>
        public Align? OverrideTextValign;
        /// <summary>
        /// The <c>FontFamily</c> and size you want to use for a specific <c>TextStyle</c>.
        ///
        /// The most convenient way to look something up in this is to use <c>Resolve</c>.
        ///
        /// If you would like to overwrite app <c>TextStyles</c>
        /// </summary>
        public ImmutableDictionary<TextStyle, FontId> TextStyles;
        /// <summary>
        /// The style to use for <c>DragValue</c> text.
        /// </summary>
        public TextStyle DragValueTextStyle;
        /// <summary>
        /// If set, labels, buttons, etc. will use this to determine whether to wrap the text at the
        /// right edge of the <c>Ui</c> they are in. By default, this is <c>None</c>.
        ///
        /// **Note**: this API is deprecated, use <c>WrapMode</c> instead.
        ///
        /// * <c>None</c>: use <c>WrapMode</c> instead
        /// * <c>Some(true)</c>: wrap mode defaults to <c>Wrap</c>
        /// * <c>Some(false)</c>: wrap mode defaults to <c>Extend</c>
        /// </summary>
        public bool? Wrap;
        /// <summary>
        /// If set, labels, buttons, etc. will use this to determine whether to wrap or truncate the
        /// text at the right edge of the <c>Ui</c> they are in, or to extend it. By default, this is
        /// <c>None</c>.
        ///
        /// * <c>None</c>: follow layout (with may wrap)
        /// * <c>Some(mode)</c>: use the specified mode as default
        /// </summary>
        public TextWrapMode? WrapMode;
        /// <summary>
        /// Sizes and distances between widgets
        /// </summary>
        public Spacing Spacing;
        /// <summary>
        /// How and when interaction happens.
        /// </summary>
        public Interaction Interaction;
        /// <summary>
        /// Colors etc.
        /// </summary>
        public Visuals Visuals;
        /// <summary>
        /// How many seconds a typical animation should last.
        /// </summary>
        public float AnimationTime;
        /// <summary>
        /// Options to help debug why egui behaves strangely.
        ///
        /// Only available in debug builds.
        /// </summary>
        public DebugOptions Debug;
        /// <summary>
        /// Show tooltips explaining <c>DragValue</c>:s etc when hovered.
        ///
        /// This only affects a few egui widgets.
        /// </summary>
        public bool ExplanationTooltips;
        /// <summary>
        /// Show the URL of hyperlinks in a tooltip when hovered.
        /// </summary>
        public bool UrlInTooltip;
        /// <summary>
        /// If true and scrolling is enabled for only one direction, allow horizontal scrolling without pressing shift
        /// </summary>
        public bool AlwaysScrollTheOnlyDirection;
        /// <summary>
        /// The animation that should be used when scrolling a <c>ScrollArea</c> using e.g. <c>ScrollToRect</c>.
        /// </summary>
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
