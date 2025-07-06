#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct ImageOptions : IEquatable<ImageOptions> {
        /// <summary>
        /// Select UV range. Default is (0,0) in top-left, (1,1) bottom right.
        /// </summary>
        public Rect Uv;
        /// <summary>
        /// A solid color to put behind the image. Useful for transparent images.
        /// </summary>
        public Color32 BgFill;
        /// <summary>
        /// Multiply image color with this. Default is WHITE (no tint).
        /// </summary>
        public Color32 Tint;
        /// <summary>
        /// Rotate the image about an origin by some angle
        ///
        /// Positive angle is clockwise.
        /// Origin is a vector in normalized UV space ((0,0) in top-left, (1,1) bottom right).
        ///
        /// To rotate about the center you can pass <c>Splat(0.5)</c> as the origin.
        ///
        /// Due to limitations in the current implementation,
        /// this will turn off rounding of the image.
        /// </summary>
        public (Rot2, Vec2)? Rotation;
        /// <summary>
        /// Round the corners of the image.
        ///
        /// The default is no rounding (<c>Zero</c>).
        ///
        /// Due to limitations in the current implementation,
        /// this will turn off any rotation of the image.
        /// </summary>
        public CornerRadius CornerRadius;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Uv.Serialize(serializer);
            BgFill.Serialize(serializer);
            Tint.Serialize(serializer);
            TraitHelpers.serialize_option_tuple2_Rot2_Vec2(Rotation, serializer);
            CornerRadius.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static ImageOptions Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            ImageOptions obj = new ImageOptions {
            	Uv = Rect.Deserialize(deserializer),
            	BgFill = Color32.Deserialize(deserializer),
            	Tint = Color32.Deserialize(deserializer),
            	Rotation = TraitHelpers.deserialize_option_tuple2_Rot2_Vec2(deserializer),
            	CornerRadius = CornerRadius.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is ImageOptions other && Equals(other);

        public static bool operator ==(ImageOptions left, ImageOptions right) => Equals(left, right);

        public static bool operator !=(ImageOptions left, ImageOptions right) => !Equals(left, right);

        public bool Equals(ImageOptions other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Uv.Equals(other.Uv)) return false;
            if (!BgFill.Equals(other.BgFill)) return false;
            if (!Tint.Equals(other.Tint)) return false;
            if (!Rotation.Equals(other.Rotation)) return false;
            if (!CornerRadius.Equals(other.CornerRadius)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Uv.GetHashCode();
                value = 31 * value + BgFill.GetHashCode();
                value = 31 * value + Tint.GetHashCode();
                value = 31 * value + Rotation.GetHashCode();
                value = 31 * value + CornerRadius.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
