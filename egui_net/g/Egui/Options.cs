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
    /// Some global options that you can read and write.
    ///
    /// See also <c>DebugOptions</c>.
    /// </summary>
    public partial struct Options : IEquatable<Options> {
        /// <summary>
        /// Preference for selection between dark and light <c>Style</c>
        /// as the active style used by all subsequent windows, panels, etc.
        ///
        /// Default: <c>System</c>.
        /// </summary>
        public ThemePreference ThemePreference;
        /// <summary>
        /// Which theme to use in case <c>ThemePreference</c> is <c>System</c>
        /// and egui fails to detect the system theme.
        ///
        /// Default: <c>Dark</c>.
        /// </summary>
        public Theme FallbackTheme;
        /// <summary>
        /// Global zoom factor of the UI.
        ///
        /// This is used to calculate the <c>PixelsPerPoint</c>
        /// for the UI as <c>PixelsPerPoint=ZoomFator*NativePixelsPerPoint</c>.
        ///
        /// The default is 1.0. Increase it to make all UI elements larger.
        ///
        /// You should call <c>SetZoomFactor</c>
        /// instead of modifying this directly!
        /// </summary>
        public float ZoomFactor;
        /// <summary>
        /// Controls the tessellator.
        /// </summary>
        public TessellationOptions TessellationOptions;
        /// <summary>
        /// If any widget moves or changes id, repaint everything.
        ///
        /// It is recommended you keep this OFF, as it may
        /// lead to endless repaints for an unknown reason. See
        /// (<https://github.com/rerun-io/rerun/issues/5018>).
        /// </summary>
        public bool RepaintOnWidgetChange;
        /// <summary>
        /// Maximum number of passes to run in one frame.
        ///
        /// Set to <c>1</c> for pure single-pass immediate mode.
        /// Set to something larger than <c>1</c> to allow multi-pass when needed.
        ///
        /// Default is <c>2</c>. This means sometimes a frame will cost twice as much,
        /// but usually only rarely (e.g. when showing a new panel for the first time).
        ///
        /// egui will usually only ever run one pass, even if <c>MaxPasses</c> is large.
        ///
        /// If this is <c>1</c>, <c>RequestDiscard</c> will be ignored.
        ///
        /// Multi-pass is supported by <c>Run</c>.
        ///
        /// See <c>RequestDiscard</c> for more.
        /// </summary>
        public ulong MaxPasses;
        /// <summary>
        /// This is a signal to any backend that we want the <c>Events</c> read out loud.
        ///
        /// The only change to egui is that labels can be focused by pressing tab.
        ///
        /// Screen readers are an experimental feature of egui, and not supported on all platforms.
        /// <c>Eframe</c> only supports it on web.
        ///
        /// Consider using AccessKit instead,
        /// which is supported by <c>Eframe</c>.
        /// </summary>
        public bool ScreenReader;
        /// <summary>
        /// If true, the most common glyphs (ASCII) are pre-rendered to the texture atlas.
        ///
        /// Only the fonts in <c>TextStyles</c> will be pre-cached.
        ///
        /// This can lead to fewer texture operations, but may use up the texture atlas quicker
        /// if you are changing <c>TextStyles</c>, or have a lot of text styles.
        /// </summary>
        public bool PreloadFontGlyphs;
        /// <summary>
        /// Check reusing of <c>Id</c>s, and show a visual warning on screen when one is found.
        ///
        /// By default this is <c>True</c> in debug builds.
        /// </summary>
        public bool WarnOnIdClash;
        /// <summary>
        /// Multiplier for the scroll speed when reported in <c>Line</c>s.
        /// </summary>
        public float LineScrollSpeed;
        /// <summary>
        /// Controls the speed at which we zoom in when doing ctrl/cmd + scroll.
        /// </summary>
        public float ScrollZoomSpeed;
        /// <summary>
        /// Options related to input state handling.
        /// </summary>
        public InputOptions InputOptions;
        /// <summary>
        /// If <c>True</c>, <c>Egui</c> will discard the loaded image data after
        /// the texture is loaded onto the GPU to reduce memory usage.
        ///
        /// In modern GPU rendering, the texture data is not required after the texture is loaded.
        ///
        /// This is beneficial when using a large number or resolution of images and there is no need to
        /// retain the image data, potentially saving a significant amount of memory.
        ///
        /// The drawback is that it becomes impossible to serialize the loaded images or render in non-GPU systems.
        ///
        /// Default is <c>False</c>.
        /// </summary>
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
