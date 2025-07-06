#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Spacing : IEquatable<Spacing> {
        public Vec2 ItemSpacing;
        public Margin WindowMargin;
        public Vec2 ButtonPadding;
        public Margin MenuMargin;
        public float Indent;
        public Vec2 InteractSize;
        public float SliderWidth;
        public float SliderRailHeight;
        public float ComboWidth;
        public float TextEditWidth;
        public float IconWidth;
        public float IconWidthInner;
        public float IconSpacing;
        public Vec2 DefaultAreaSize;
        public float TooltipWidth;
        public float MenuWidth;
        public float MenuSpacing;
        public bool IndentEndsWithHorizontalLine;
        public float ComboHeight;
        public ScrollStyle Scroll;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            ItemSpacing.Serialize(serializer);
            WindowMargin.Serialize(serializer);
            ButtonPadding.Serialize(serializer);
            MenuMargin.Serialize(serializer);
            serializer.serialize_f32(Indent);
            InteractSize.Serialize(serializer);
            serializer.serialize_f32(SliderWidth);
            serializer.serialize_f32(SliderRailHeight);
            serializer.serialize_f32(ComboWidth);
            serializer.serialize_f32(TextEditWidth);
            serializer.serialize_f32(IconWidth);
            serializer.serialize_f32(IconWidthInner);
            serializer.serialize_f32(IconSpacing);
            DefaultAreaSize.Serialize(serializer);
            serializer.serialize_f32(TooltipWidth);
            serializer.serialize_f32(MenuWidth);
            serializer.serialize_f32(MenuSpacing);
            serializer.serialize_bool(IndentEndsWithHorizontalLine);
            serializer.serialize_f32(ComboHeight);
            Scroll.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static Spacing Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Spacing obj = new Spacing {
            	ItemSpacing = Vec2.Deserialize(deserializer),
            	WindowMargin = Margin.Deserialize(deserializer),
            	ButtonPadding = Vec2.Deserialize(deserializer),
            	MenuMargin = Margin.Deserialize(deserializer),
            	Indent = deserializer.deserialize_f32(),
            	InteractSize = Vec2.Deserialize(deserializer),
            	SliderWidth = deserializer.deserialize_f32(),
            	SliderRailHeight = deserializer.deserialize_f32(),
            	ComboWidth = deserializer.deserialize_f32(),
            	TextEditWidth = deserializer.deserialize_f32(),
            	IconWidth = deserializer.deserialize_f32(),
            	IconWidthInner = deserializer.deserialize_f32(),
            	IconSpacing = deserializer.deserialize_f32(),
            	DefaultAreaSize = Vec2.Deserialize(deserializer),
            	TooltipWidth = deserializer.deserialize_f32(),
            	MenuWidth = deserializer.deserialize_f32(),
            	MenuSpacing = deserializer.deserialize_f32(),
            	IndentEndsWithHorizontalLine = deserializer.deserialize_bool(),
            	ComboHeight = deserializer.deserialize_f32(),
            	Scroll = ScrollStyle.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Spacing other && Equals(other);

        public static bool operator ==(Spacing left, Spacing right) => Equals(left, right);

        public static bool operator !=(Spacing left, Spacing right) => !Equals(left, right);

        public bool Equals(Spacing other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!ItemSpacing.Equals(other.ItemSpacing)) return false;
            if (!WindowMargin.Equals(other.WindowMargin)) return false;
            if (!ButtonPadding.Equals(other.ButtonPadding)) return false;
            if (!MenuMargin.Equals(other.MenuMargin)) return false;
            if (!Indent.Equals(other.Indent)) return false;
            if (!InteractSize.Equals(other.InteractSize)) return false;
            if (!SliderWidth.Equals(other.SliderWidth)) return false;
            if (!SliderRailHeight.Equals(other.SliderRailHeight)) return false;
            if (!ComboWidth.Equals(other.ComboWidth)) return false;
            if (!TextEditWidth.Equals(other.TextEditWidth)) return false;
            if (!IconWidth.Equals(other.IconWidth)) return false;
            if (!IconWidthInner.Equals(other.IconWidthInner)) return false;
            if (!IconSpacing.Equals(other.IconSpacing)) return false;
            if (!DefaultAreaSize.Equals(other.DefaultAreaSize)) return false;
            if (!TooltipWidth.Equals(other.TooltipWidth)) return false;
            if (!MenuWidth.Equals(other.MenuWidth)) return false;
            if (!MenuSpacing.Equals(other.MenuSpacing)) return false;
            if (!IndentEndsWithHorizontalLine.Equals(other.IndentEndsWithHorizontalLine)) return false;
            if (!ComboHeight.Equals(other.ComboHeight)) return false;
            if (!Scroll.Equals(other.Scroll)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + ItemSpacing.GetHashCode();
                value = 31 * value + WindowMargin.GetHashCode();
                value = 31 * value + ButtonPadding.GetHashCode();
                value = 31 * value + MenuMargin.GetHashCode();
                value = 31 * value + Indent.GetHashCode();
                value = 31 * value + InteractSize.GetHashCode();
                value = 31 * value + SliderWidth.GetHashCode();
                value = 31 * value + SliderRailHeight.GetHashCode();
                value = 31 * value + ComboWidth.GetHashCode();
                value = 31 * value + TextEditWidth.GetHashCode();
                value = 31 * value + IconWidth.GetHashCode();
                value = 31 * value + IconWidthInner.GetHashCode();
                value = 31 * value + IconSpacing.GetHashCode();
                value = 31 * value + DefaultAreaSize.GetHashCode();
                value = 31 * value + TooltipWidth.GetHashCode();
                value = 31 * value + MenuWidth.GetHashCode();
                value = 31 * value + MenuSpacing.GetHashCode();
                value = 31 * value + IndentEndsWithHorizontalLine.GetHashCode();
                value = 31 * value + ComboHeight.GetHashCode();
                value = 31 * value + Scroll.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
