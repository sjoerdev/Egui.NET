#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct ImageOptions : IEquatable<ImageOptions> {
        public Rect Uv;
        public Color32 BgFill;
        public Color32 Tint;
        public (Rot2, Vec2)? Rotation;
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
