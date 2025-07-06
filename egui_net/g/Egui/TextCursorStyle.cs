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
    /// Look and feel of the text cursor.
    /// </summary>
    public partial struct TextCursorStyle : IEquatable<TextCursorStyle> {
        /// <summary>
        /// The color and width of the text cursor
        /// </summary>
        public Stroke Stroke;
        /// <summary>
        /// Show where the text cursor would be if you clicked?
        /// </summary>
        public bool Preview;
        /// <summary>
        /// Should the cursor blink?
        /// </summary>
        public bool Blink;
        /// <summary>
        /// When blinking, this is how long the cursor is visible.
        /// </summary>
        public float OnDuration;
        /// <summary>
        /// When blinking, this is how long the cursor is invisible.
        /// </summary>
        public float OffDuration;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Stroke.Serialize(serializer);
            serializer.serialize_bool(Preview);
            serializer.serialize_bool(Blink);
            serializer.serialize_f32(OnDuration);
            serializer.serialize_f32(OffDuration);
            serializer.decrease_container_depth();
        }

        internal static TextCursorStyle Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            TextCursorStyle obj = new TextCursorStyle {
            	Stroke = Stroke.Deserialize(deserializer),
            	Preview = deserializer.deserialize_bool(),
            	Blink = deserializer.deserialize_bool(),
            	OnDuration = deserializer.deserialize_f32(),
            	OffDuration = deserializer.deserialize_f32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is TextCursorStyle other && Equals(other);

        public static bool operator ==(TextCursorStyle left, TextCursorStyle right) => Equals(left, right);

        public static bool operator !=(TextCursorStyle left, TextCursorStyle right) => !Equals(left, right);

        public bool Equals(TextCursorStyle other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Stroke.Equals(other.Stroke)) return false;
            if (!Preview.Equals(other.Preview)) return false;
            if (!Blink.Equals(other.Blink)) return false;
            if (!OnDuration.Equals(other.OnDuration)) return false;
            if (!OffDuration.Equals(other.OffDuration)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Stroke.GetHashCode();
                value = 31 * value + Preview.GetHashCode();
                value = 31 * value + Blink.GetHashCode();
                value = 31 * value + OnDuration.GetHashCode();
                value = 31 * value + OffDuration.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
