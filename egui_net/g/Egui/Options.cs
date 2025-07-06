#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Options : IEquatable<Options> {
        public ThemePreference ThemePreference;
        public Theme FallbackTheme;
        public float ZoomFactor;
        public TessellationOptions TessellationOptions;
        public bool RepaintOnWidgetChange;
        public ulong MaxPasses;
        public bool ScreenReader;
        public bool PreloadFontGlyphs;
        public bool WarnOnIdClash;
        public float LineScrollSpeed;
        public float ScrollZoomSpeed;
        public InputOptions InputOptions;
        public bool ReduceTextureMemory;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            ThemePreference.Serialize(serializer);
            FallbackTheme.Serialize(serializer);
            serializer.serialize_f32(ZoomFactor);
            TessellationOptions.Serialize(serializer);
            serializer.serialize_bool(RepaintOnWidgetChange);
            serializer.serialize_u64(MaxPasses);
            serializer.serialize_bool(ScreenReader);
            serializer.serialize_bool(PreloadFontGlyphs);
            serializer.serialize_bool(WarnOnIdClash);
            serializer.serialize_f32(LineScrollSpeed);
            serializer.serialize_f32(ScrollZoomSpeed);
            InputOptions.Serialize(serializer);
            serializer.serialize_bool(ReduceTextureMemory);
            serializer.decrease_container_depth();
        }

        internal static Options Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Options obj = new Options {
            	ThemePreference = ThemePreferenceExtensions.Deserialize(deserializer),
            	FallbackTheme = ThemeExtensions.Deserialize(deserializer),
            	ZoomFactor = deserializer.deserialize_f32(),
            	TessellationOptions = TessellationOptions.Deserialize(deserializer),
            	RepaintOnWidgetChange = deserializer.deserialize_bool(),
            	MaxPasses = deserializer.deserialize_u64(),
            	ScreenReader = deserializer.deserialize_bool(),
            	PreloadFontGlyphs = deserializer.deserialize_bool(),
            	WarnOnIdClash = deserializer.deserialize_bool(),
            	LineScrollSpeed = deserializer.deserialize_f32(),
            	ScrollZoomSpeed = deserializer.deserialize_f32(),
            	InputOptions = InputOptions.Deserialize(deserializer),
            	ReduceTextureMemory = deserializer.deserialize_bool() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Options other && Equals(other);

        public static bool operator ==(Options left, Options right) => Equals(left, right);

        public static bool operator !=(Options left, Options right) => !Equals(left, right);

        public bool Equals(Options other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!ThemePreference.Equals(other.ThemePreference)) return false;
            if (!FallbackTheme.Equals(other.FallbackTheme)) return false;
            if (!ZoomFactor.Equals(other.ZoomFactor)) return false;
            if (!TessellationOptions.Equals(other.TessellationOptions)) return false;
            if (!RepaintOnWidgetChange.Equals(other.RepaintOnWidgetChange)) return false;
            if (!MaxPasses.Equals(other.MaxPasses)) return false;
            if (!ScreenReader.Equals(other.ScreenReader)) return false;
            if (!PreloadFontGlyphs.Equals(other.PreloadFontGlyphs)) return false;
            if (!WarnOnIdClash.Equals(other.WarnOnIdClash)) return false;
            if (!LineScrollSpeed.Equals(other.LineScrollSpeed)) return false;
            if (!ScrollZoomSpeed.Equals(other.ScrollZoomSpeed)) return false;
            if (!InputOptions.Equals(other.InputOptions)) return false;
            if (!ReduceTextureMemory.Equals(other.ReduceTextureMemory)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + ThemePreference.GetHashCode();
                value = 31 * value + FallbackTheme.GetHashCode();
                value = 31 * value + ZoomFactor.GetHashCode();
                value = 31 * value + TessellationOptions.GetHashCode();
                value = 31 * value + RepaintOnWidgetChange.GetHashCode();
                value = 31 * value + MaxPasses.GetHashCode();
                value = 31 * value + ScreenReader.GetHashCode();
                value = 31 * value + PreloadFontGlyphs.GetHashCode();
                value = 31 * value + WarnOnIdClash.GetHashCode();
                value = 31 * value + LineScrollSpeed.GetHashCode();
                value = 31 * value + ScrollZoomSpeed.GetHashCode();
                value = 31 * value + InputOptions.GetHashCode();
                value = 31 * value + ReduceTextureMemory.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
