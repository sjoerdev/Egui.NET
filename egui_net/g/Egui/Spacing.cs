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
    /// Controls the sizes and distances between widgets.
    /// </summary>
    public partial struct Spacing : IEquatable<Spacing> {
        /// <summary>
        /// Horizontal and vertical spacing between widgets.
        ///
        /// To add extra space between widgets, use <c>AddSpace</c>.
        ///
        /// <c>ItemSpacing</c> is inserted _after_ adding a widget, so to increase the spacing between
        /// widgets <c>A</c> and <c>B</c> you need to change <c>ItemSpacing</c> before adding <c>A</c>.
        /// </summary>
        public Vec2 ItemSpacing;
        /// <summary>
        /// Horizontal and vertical margins within a window frame.
        /// </summary>
        public Margin WindowMargin;
        /// <summary>
        /// Button size is text size plus this on each side
        /// </summary>
        public Vec2 ButtonPadding;
        /// <summary>
        /// Horizontal and vertical margins within a menu frame.
        /// </summary>
        public Margin MenuMargin;
        /// <summary>
        /// Indent collapsing regions etc by this much.
        /// </summary>
        public float Indent;
        /// <summary>
        /// Minimum size of a <c>DragValue</c>, color picker button, and other small widgets.
        /// <c>InteractSize.y</c> is the default height of button, slider, etc.
        /// Anything clickable should be (at least) this size.
        /// </summary>
        public Vec2 InteractSize;
        /// <summary>
        /// Default width of a <c>Slider</c>.
        /// </summary>
        public float SliderWidth;
        /// <summary>
        /// Default rail height of a <c>Slider</c>.
        /// </summary>
        public float SliderRailHeight;
        /// <summary>
        /// Default (minimum) width of a <c>ComboBox</c>.
        /// </summary>
        public float ComboWidth;
        /// <summary>
        /// Default width of a <c>TextEdit</c>.
        /// </summary>
        public float TextEditWidth;
        /// <summary>
        /// Checkboxes, radio button and collapsing headers have an icon at the start.
        /// This is the width/height of the outer part of this icon (e.g. the BOX of the checkbox).
        /// </summary>
        public float IconWidth;
        /// <summary>
        /// Checkboxes, radio button and collapsing headers have an icon at the start.
        /// This is the width/height of the inner part of this icon (e.g. the check of the checkbox).
        /// </summary>
        public float IconWidthInner;
        /// <summary>
        /// Checkboxes, radio button and collapsing headers have an icon at the start.
        /// This is the spacing between the icon and the text
        /// </summary>
        public float IconSpacing;
        /// <summary>
        /// The size used for the <c>MaxRect</c> the first frame.
        ///
        /// Text will wrap at this width, and images that expand to fill the available space
        /// will expand to this size.
        ///
        /// If the contents are smaller than this size, the area will shrink to fit the contents.
        /// If the contents overflow, the area will grow.
        /// </summary>
        public Vec2 DefaultAreaSize;
        /// <summary>
        /// Width of a tooltip (<c>OnHoverUi</c>, <c>OnHoverText</c> etc).
        /// </summary>
        public float TooltipWidth;
        /// <summary>
        /// The default wrapping width of a menu.
        ///
        /// Items longer than this will wrap to a new line.
        /// </summary>
        public float MenuWidth;
        /// <summary>
        /// Horizontal distance between a menu and a submenu.
        /// </summary>
        public float MenuSpacing;
        /// <summary>
        /// End indented regions with a horizontal line
        /// </summary>
        public bool IndentEndsWithHorizontalLine;
        /// <summary>
        /// Height of a combo-box before showing scroll bars.
        /// </summary>
        public float ComboHeight;
        /// <summary>
        /// Controls the spacing of a <c>ScrollArea</c>.
        /// </summary>
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
