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
    /// A frame around some content, including margin, colors, etc.
    ///
    /// ## Definitions
    /// The total (outer) size of a frame is
    /// <c>ContentSize+InnerMargin+2*Stroke.width+OuterMargin</c>.
    ///
    /// Everything within the stroke is filled with the fill color (if any).The four rectangles, from inside to outside, are:
    /// * <c>ContentRect</c>: the rectangle that is made available to the inner <c>Ui</c> or widget.
    /// * <c>FillRect</c>: the rectangle that is filled with the fill color (inside the stroke, if any).
    /// * <c>WidgetRect</c>: is the interactive part of the widget (what sense clicks etc).
    /// * <c>OuterRect</c>: what is allocated in the outer <c>Ui</c>, and is what is returned by <c>Rect</c>.
    ///
    /// ## Usage## Dynamic color
    /// If you want to change the color of the frame based on the response of
    /// the widget, you need to break it up into multiple steps:You can also respond to the hovering of the frame itself:Note that you cannot change the margins after calling <c>Begin</c>.
    /// </summary>
    public partial struct Frame : IEquatable<Frame> {
        /// <summary>
        /// Margin within the painted frame.
        ///
        /// Known as <c>Padding</c> in CSS.
        /// </summary>
        public Margin InnerMargin;
        /// <summary>
        /// The background fill color of the frame, within the <c>Stroke</c>.
        ///
        /// Known as <c>Background</c> in CSS.
        /// </summary>
        public Color32 Fill;
        /// <summary>
        /// The width and color of the outline around the frame.
        ///
        /// The width of the stroke is part of the total margin/padding of the frame.
        /// </summary>
        public Stroke Stroke;
        /// <summary>
        /// The rounding of the _outer_ corner of the <c>Stroke</c>
        /// (or, if there is no stroke, the outer corner of <c>Fill</c>).
        ///
        /// In other words, this is the corner radius of the _widget rect_.
        /// </summary>
        public CornerRadius CornerRadius;
        /// <summary>
        /// Margin outside the painted frame.
        ///
        /// Similar to what is called <c>Margin</c> in CSS.
        /// However, egui does NOT do "Margin Collapse" like in CSS,
        /// i.e. when placing two frames next to each other,
        /// the distance between their borders is the SUM
        /// of their other margins.
        /// In CSS the distance would be the MAX of their outer margins.
        /// Supporting margin collapse is difficult, and would
        /// requires complicating the already complicated egui layout code.
        ///
        /// Consider using <c>ItemSpacing</c>
        /// for adding space between widgets.
        /// </summary>
        public Margin OuterMargin;
        /// <summary>
        /// Optional drop-shadow behind the frame.
        /// </summary>
        public Shadow Shadow;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            InnerMargin.Serialize(serializer);
            Fill.Serialize(serializer);
            Stroke.Serialize(serializer);
            CornerRadius.Serialize(serializer);
            OuterMargin.Serialize(serializer);
            Shadow.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static Frame Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Frame obj = new Frame {
            	InnerMargin = Margin.Deserialize(deserializer),
            	Fill = Color32.Deserialize(deserializer),
            	Stroke = Stroke.Deserialize(deserializer),
            	CornerRadius = CornerRadius.Deserialize(deserializer),
            	OuterMargin = Margin.Deserialize(deserializer),
            	Shadow = Shadow.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Frame other && Equals(other);

        public static bool operator ==(Frame left, Frame right) => Equals(left, right);

        public static bool operator !=(Frame left, Frame right) => !Equals(left, right);

        public bool Equals(Frame other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!InnerMargin.Equals(other.InnerMargin)) return false;
            if (!Fill.Equals(other.Fill)) return false;
            if (!Stroke.Equals(other.Stroke)) return false;
            if (!CornerRadius.Equals(other.CornerRadius)) return false;
            if (!OuterMargin.Equals(other.OuterMargin)) return false;
            if (!Shadow.Equals(other.Shadow)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + InnerMargin.GetHashCode();
                value = 31 * value + Fill.GetHashCode();
                value = 31 * value + Stroke.GetHashCode();
                value = 31 * value + CornerRadius.GetHashCode();
                value = 31 * value + OuterMargin.GetHashCode();
                value = 31 * value + Shadow.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
