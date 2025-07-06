#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Visuals : IEquatable<Visuals> {
        public bool DarkMode;
        public Color32? OverrideTextColor;
        public Widgets Widgets;
        public Selection Selection;
        public Color32 HyperlinkColor;
        public Color32 FaintBgColor;
        public Color32 ExtremeBgColor;
        public Color32 CodeBgColor;
        public Color32 WarnFgColor;
        public Color32 ErrorFgColor;
        public CornerRadius WindowCornerRadius;
        public Shadow WindowShadow;
        public Color32 WindowFill;
        public Stroke WindowStroke;
        public bool WindowHighlightTopmost;
        public CornerRadius MenuCornerRadius;
        public Color32 PanelFill;
        public Shadow PopupShadow;
        public float ResizeCornerSize;
        public TextCursorStyle TextCursor;
        public float ClipRectMargin;
        public bool ButtonFrame;
        public bool CollapsingHeaderFrame;
        public bool IndentHasLeftVline;
        public bool Striped;
        public bool SliderTrailingFill;
        public HandleShape HandleShape;
        public CursorIcon? InteractCursor;
        public bool ImageLoadingSpinners;
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
