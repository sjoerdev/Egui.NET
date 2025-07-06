#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct ScrollStyle : IEquatable<ScrollStyle> {
        public bool Floating;
        public float BarWidth;
        public float HandleMinLength;
        public float BarInnerMargin;
        public float BarOuterMargin;
        public float FloatingWidth;
        public float FloatingAllocatedWidth;
        public bool ForegroundColor;
        public float DormantBackgroundOpacity;
        public float ActiveBackgroundOpacity;
        public float InteractBackgroundOpacity;
        public float DormantHandleOpacity;
        public float ActiveHandleOpacity;
        public float InteractHandleOpacity;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_bool(Floating);
            serializer.serialize_f32(BarWidth);
            serializer.serialize_f32(HandleMinLength);
            serializer.serialize_f32(BarInnerMargin);
            serializer.serialize_f32(BarOuterMargin);
            serializer.serialize_f32(FloatingWidth);
            serializer.serialize_f32(FloatingAllocatedWidth);
            serializer.serialize_bool(ForegroundColor);
            serializer.serialize_f32(DormantBackgroundOpacity);
            serializer.serialize_f32(ActiveBackgroundOpacity);
            serializer.serialize_f32(InteractBackgroundOpacity);
            serializer.serialize_f32(DormantHandleOpacity);
            serializer.serialize_f32(ActiveHandleOpacity);
            serializer.serialize_f32(InteractHandleOpacity);
            serializer.decrease_container_depth();
        }

        internal static ScrollStyle Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            ScrollStyle obj = new ScrollStyle {
            	Floating = deserializer.deserialize_bool(),
            	BarWidth = deserializer.deserialize_f32(),
            	HandleMinLength = deserializer.deserialize_f32(),
            	BarInnerMargin = deserializer.deserialize_f32(),
            	BarOuterMargin = deserializer.deserialize_f32(),
            	FloatingWidth = deserializer.deserialize_f32(),
            	FloatingAllocatedWidth = deserializer.deserialize_f32(),
            	ForegroundColor = deserializer.deserialize_bool(),
            	DormantBackgroundOpacity = deserializer.deserialize_f32(),
            	ActiveBackgroundOpacity = deserializer.deserialize_f32(),
            	InteractBackgroundOpacity = deserializer.deserialize_f32(),
            	DormantHandleOpacity = deserializer.deserialize_f32(),
            	ActiveHandleOpacity = deserializer.deserialize_f32(),
            	InteractHandleOpacity = deserializer.deserialize_f32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is ScrollStyle other && Equals(other);

        public static bool operator ==(ScrollStyle left, ScrollStyle right) => Equals(left, right);

        public static bool operator !=(ScrollStyle left, ScrollStyle right) => !Equals(left, right);

        public bool Equals(ScrollStyle other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Floating.Equals(other.Floating)) return false;
            if (!BarWidth.Equals(other.BarWidth)) return false;
            if (!HandleMinLength.Equals(other.HandleMinLength)) return false;
            if (!BarInnerMargin.Equals(other.BarInnerMargin)) return false;
            if (!BarOuterMargin.Equals(other.BarOuterMargin)) return false;
            if (!FloatingWidth.Equals(other.FloatingWidth)) return false;
            if (!FloatingAllocatedWidth.Equals(other.FloatingAllocatedWidth)) return false;
            if (!ForegroundColor.Equals(other.ForegroundColor)) return false;
            if (!DormantBackgroundOpacity.Equals(other.DormantBackgroundOpacity)) return false;
            if (!ActiveBackgroundOpacity.Equals(other.ActiveBackgroundOpacity)) return false;
            if (!InteractBackgroundOpacity.Equals(other.InteractBackgroundOpacity)) return false;
            if (!DormantHandleOpacity.Equals(other.DormantHandleOpacity)) return false;
            if (!ActiveHandleOpacity.Equals(other.ActiveHandleOpacity)) return false;
            if (!InteractHandleOpacity.Equals(other.InteractHandleOpacity)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Floating.GetHashCode();
                value = 31 * value + BarWidth.GetHashCode();
                value = 31 * value + HandleMinLength.GetHashCode();
                value = 31 * value + BarInnerMargin.GetHashCode();
                value = 31 * value + BarOuterMargin.GetHashCode();
                value = 31 * value + FloatingWidth.GetHashCode();
                value = 31 * value + FloatingAllocatedWidth.GetHashCode();
                value = 31 * value + ForegroundColor.GetHashCode();
                value = 31 * value + DormantBackgroundOpacity.GetHashCode();
                value = 31 * value + ActiveBackgroundOpacity.GetHashCode();
                value = 31 * value + InteractBackgroundOpacity.GetHashCode();
                value = 31 * value + DormantHandleOpacity.GetHashCode();
                value = 31 * value + ActiveHandleOpacity.GetHashCode();
                value = 31 * value + InteractHandleOpacity.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
