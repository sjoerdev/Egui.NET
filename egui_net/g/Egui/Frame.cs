#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Frame : IEquatable<Frame> {
        public Margin InnerMargin;
        public Color32 Fill;
        public Stroke Stroke;
        public CornerRadius CornerRadius;
        public Margin OuterMargin;
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
