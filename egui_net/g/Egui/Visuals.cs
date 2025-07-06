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
    /// Controls the visual style (colors etc) of egui.
    ///
    /// You can change the visuals of a <c>Ui</c> with <c>VisualsMut</c>
    /// and of everything with <c>SetVisualsOf</c>.
    ///
    /// If you want to change fonts, use <c>SetFonts</c> instead.
    /// </summary>
    public partial struct Visuals : IEquatable<Visuals> {
        /// <summary>
        /// If true, the visuals are overall dark with light text.
        /// If false, the visuals are overall light with dark text.
        ///
        /// NOTE: setting this does very little by itself,
        /// this is more to provide a convenient summary of the rest of the settings.
        /// </summary>
        public bool DarkMode;
        /// <summary>
        /// Override default text color for all text.
        ///
        /// This is great for setting the color of text for any widget.
        ///
        /// If <c>TextColor</c> is <c>None</c> (default), then the text color will be the same as the
        /// foreground stroke color (<c>FgStroke</c>)
        /// and will depend on whether or not the widget is being interacted with.
        ///
        /// In the future we may instead modulate
        /// the <c>TextColor</c> based on whether or not it is interacted with
        /// so that <c>Visuals.textColor</c> is always used,
        /// but its alpha may be different based on whether or not
        /// it is disabled, non-interactive, hovered etc.
        /// </summary>
        public Color32? OverrideTextColor;
        /// <summary>
        /// Visual styles of widgets
        /// </summary>
        public Widgets Widgets;
        public Selection Selection;
        /// <summary>
        /// The color used for <c>Hyperlink</c>,
        /// </summary>
        public Color32 HyperlinkColor;
        /// <summary>
        /// Something just barely different from the background color.
        /// Used for <c>Striped</c>.
        /// </summary>
        public Color32 FaintBgColor;
        /// <summary>
        /// Very dark or light color (for corresponding theme).
        /// Used as the background of text edits, scroll bars and others things
        /// that needs to look different from other interactive stuff.
        /// </summary>
        public Color32 ExtremeBgColor;
        /// <summary>
        /// Background color behind code-styled monospaced labels.
        /// </summary>
        public Color32 CodeBgColor;
        /// <summary>
        /// A good color for warning text (e.g. orange).
        /// </summary>
        public Color32 WarnFgColor;
        /// <summary>
        /// A good color for error text (e.g. red).
        /// </summary>
        public Color32 ErrorFgColor;
        public CornerRadius WindowCornerRadius;
        public Shadow WindowShadow;
        public Color32 WindowFill;
        public Stroke WindowStroke;
        /// <summary>
        /// Highlight the topmost window.
        /// </summary>
        public bool WindowHighlightTopmost;
        public CornerRadius MenuCornerRadius;
        /// <summary>
        /// Panel background color
        /// </summary>
        public Color32 PanelFill;
        public Shadow PopupShadow;
        public float ResizeCornerSize;
        /// <summary>
        /// How the text cursor acts.
        /// </summary>
        public TextCursorStyle TextCursor;
        /// <summary>
        /// Allow child widgets to be just on the border and still have a stroke with some thickness
        /// </summary>
        public float ClipRectMargin;
        /// <summary>
        /// Show a background behind buttons.
        /// </summary>
        public bool ButtonFrame;
        /// <summary>
        /// Show a background behind collapsing headers.
        /// </summary>
        public bool CollapsingHeaderFrame;
        /// <summary>
        /// Draw a vertical lien left of indented region, in e.g. <c>CollapsingHeader</c>.
        /// </summary>
        public bool IndentHasLeftVline;
        /// <summary>
        /// Whether or not Grids and Tables should be striped by default
        /// (have alternating rows differently colored).
        /// </summary>
        public bool Striped;
        /// <summary>
        /// Show trailing color behind the circle of a <c>Slider</c>. Default is OFF.
        ///
        /// Enabling this will affect ALL sliders, and can be enabled/disabled per slider with <c>TrailingFill</c>.
        /// </summary>
        public bool SliderTrailingFill;
        /// <summary>
        /// Shape of the handle for sliders and similar widgets.
        ///
        /// Changing this will affect ALL sliders, and can be enabled/disabled per slider with <c>HandleShape</c>.
        /// </summary>
        public HandleShape HandleShape;
        /// <summary>
        /// Should the cursor change when the user hovers over an interactive/clickable item?
        ///
        /// This is consistent with a lot of browser-based applications (vscode, github
        /// all turn your cursor into <c>PointingHand</c> when a button is
        /// hovered) but it is inconsistent with native UI toolkits.
        /// </summary>
        public CursorIcon? InteractCursor;
        /// <summary>
        /// Show a spinner when loading an image.
        /// </summary>
        public bool ImageLoadingSpinners;
        /// <summary>
        /// How to display numeric color values.
        /// </summary>
        public NumericColorSpace NumericColorSpace;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_bool(DarkMode);
            TraitHelpers.serialize_option_Color32(OverrideTextColor, serializer);
            Widgets.Serialize(serializer);
            Selection.Serialize(serializer);
            HyperlinkColor.Serialize(serializer);
            FaintBgColor.Serialize(serializer);
            ExtremeBgColor.Serialize(serializer);
            CodeBgColor.Serialize(serializer);
            WarnFgColor.Serialize(serializer);
            ErrorFgColor.Serialize(serializer);
            WindowCornerRadius.Serialize(serializer);
            WindowShadow.Serialize(serializer);
            WindowFill.Serialize(serializer);
            WindowStroke.Serialize(serializer);
            serializer.serialize_bool(WindowHighlightTopmost);
            MenuCornerRadius.Serialize(serializer);
            PanelFill.Serialize(serializer);
            PopupShadow.Serialize(serializer);
            serializer.serialize_f32(ResizeCornerSize);
            TextCursor.Serialize(serializer);
            serializer.serialize_f32(ClipRectMargin);
            serializer.serialize_bool(ButtonFrame);
            serializer.serialize_bool(CollapsingHeaderFrame);
            serializer.serialize_bool(IndentHasLeftVline);
            serializer.serialize_bool(Striped);
            serializer.serialize_bool(SliderTrailingFill);
            HandleShape.Serialize(serializer);
            TraitHelpers.serialize_option_CursorIcon(InteractCursor, serializer);
            serializer.serialize_bool(ImageLoadingSpinners);
            NumericColorSpace.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static Visuals Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Visuals obj = new Visuals {
            	DarkMode = deserializer.deserialize_bool(),
            	OverrideTextColor = TraitHelpers.deserialize_option_Color32(deserializer),
            	Widgets = Widgets.Deserialize(deserializer),
            	Selection = Selection.Deserialize(deserializer),
            	HyperlinkColor = Color32.Deserialize(deserializer),
            	FaintBgColor = Color32.Deserialize(deserializer),
            	ExtremeBgColor = Color32.Deserialize(deserializer),
            	CodeBgColor = Color32.Deserialize(deserializer),
            	WarnFgColor = Color32.Deserialize(deserializer),
            	ErrorFgColor = Color32.Deserialize(deserializer),
            	WindowCornerRadius = CornerRadius.Deserialize(deserializer),
            	WindowShadow = Shadow.Deserialize(deserializer),
            	WindowFill = Color32.Deserialize(deserializer),
            	WindowStroke = Stroke.Deserialize(deserializer),
            	WindowHighlightTopmost = deserializer.deserialize_bool(),
            	MenuCornerRadius = CornerRadius.Deserialize(deserializer),
            	PanelFill = Color32.Deserialize(deserializer),
            	PopupShadow = Shadow.Deserialize(deserializer),
            	ResizeCornerSize = deserializer.deserialize_f32(),
            	TextCursor = TextCursorStyle.Deserialize(deserializer),
            	ClipRectMargin = deserializer.deserialize_f32(),
            	ButtonFrame = deserializer.deserialize_bool(),
            	CollapsingHeaderFrame = deserializer.deserialize_bool(),
            	IndentHasLeftVline = deserializer.deserialize_bool(),
            	Striped = deserializer.deserialize_bool(),
            	SliderTrailingFill = deserializer.deserialize_bool(),
            	HandleShape = HandleShape.Deserialize(deserializer),
            	InteractCursor = TraitHelpers.deserialize_option_CursorIcon(deserializer),
            	ImageLoadingSpinners = deserializer.deserialize_bool(),
            	NumericColorSpace = NumericColorSpaceExtensions.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Visuals other && Equals(other);

        public static bool operator ==(Visuals left, Visuals right) => Equals(left, right);

        public static bool operator !=(Visuals left, Visuals right) => !Equals(left, right);

        public bool Equals(Visuals other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!DarkMode.Equals(other.DarkMode)) return false;
            if (!OverrideTextColor.Equals(other.OverrideTextColor)) return false;
            if (!Widgets.Equals(other.Widgets)) return false;
            if (!Selection.Equals(other.Selection)) return false;
            if (!HyperlinkColor.Equals(other.HyperlinkColor)) return false;
            if (!FaintBgColor.Equals(other.FaintBgColor)) return false;
            if (!ExtremeBgColor.Equals(other.ExtremeBgColor)) return false;
            if (!CodeBgColor.Equals(other.CodeBgColor)) return false;
            if (!WarnFgColor.Equals(other.WarnFgColor)) return false;
            if (!ErrorFgColor.Equals(other.ErrorFgColor)) return false;
            if (!WindowCornerRadius.Equals(other.WindowCornerRadius)) return false;
            if (!WindowShadow.Equals(other.WindowShadow)) return false;
            if (!WindowFill.Equals(other.WindowFill)) return false;
            if (!WindowStroke.Equals(other.WindowStroke)) return false;
            if (!WindowHighlightTopmost.Equals(other.WindowHighlightTopmost)) return false;
            if (!MenuCornerRadius.Equals(other.MenuCornerRadius)) return false;
            if (!PanelFill.Equals(other.PanelFill)) return false;
            if (!PopupShadow.Equals(other.PopupShadow)) return false;
            if (!ResizeCornerSize.Equals(other.ResizeCornerSize)) return false;
            if (!TextCursor.Equals(other.TextCursor)) return false;
            if (!ClipRectMargin.Equals(other.ClipRectMargin)) return false;
            if (!ButtonFrame.Equals(other.ButtonFrame)) return false;
            if (!CollapsingHeaderFrame.Equals(other.CollapsingHeaderFrame)) return false;
            if (!IndentHasLeftVline.Equals(other.IndentHasLeftVline)) return false;
            if (!Striped.Equals(other.Striped)) return false;
            if (!SliderTrailingFill.Equals(other.SliderTrailingFill)) return false;
            if (!HandleShape.Equals(other.HandleShape)) return false;
            if (!InteractCursor.Equals(other.InteractCursor)) return false;
            if (!ImageLoadingSpinners.Equals(other.ImageLoadingSpinners)) return false;
            if (!NumericColorSpace.Equals(other.NumericColorSpace)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + DarkMode.GetHashCode();
                value = 31 * value + OverrideTextColor.GetHashCode();
                value = 31 * value + Widgets.GetHashCode();
                value = 31 * value + Selection.GetHashCode();
                value = 31 * value + HyperlinkColor.GetHashCode();
                value = 31 * value + FaintBgColor.GetHashCode();
                value = 31 * value + ExtremeBgColor.GetHashCode();
                value = 31 * value + CodeBgColor.GetHashCode();
                value = 31 * value + WarnFgColor.GetHashCode();
                value = 31 * value + ErrorFgColor.GetHashCode();
                value = 31 * value + WindowCornerRadius.GetHashCode();
                value = 31 * value + WindowShadow.GetHashCode();
                value = 31 * value + WindowFill.GetHashCode();
                value = 31 * value + WindowStroke.GetHashCode();
                value = 31 * value + WindowHighlightTopmost.GetHashCode();
                value = 31 * value + MenuCornerRadius.GetHashCode();
                value = 31 * value + PanelFill.GetHashCode();
                value = 31 * value + PopupShadow.GetHashCode();
                value = 31 * value + ResizeCornerSize.GetHashCode();
                value = 31 * value + TextCursor.GetHashCode();
                value = 31 * value + ClipRectMargin.GetHashCode();
                value = 31 * value + ButtonFrame.GetHashCode();
                value = 31 * value + CollapsingHeaderFrame.GetHashCode();
                value = 31 * value + IndentHasLeftVline.GetHashCode();
                value = 31 * value + Striped.GetHashCode();
                value = 31 * value + SliderTrailingFill.GetHashCode();
                value = 31 * value + HandleShape.GetHashCode();
                value = 31 * value + InteractCursor.GetHashCode();
                value = 31 * value + ImageLoadingSpinners.GetHashCode();
                value = 31 * value + NumericColorSpace.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
