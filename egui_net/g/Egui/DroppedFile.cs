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
    /// A file dropped into egui.
    /// </summary>
    public partial struct DroppedFile : IEquatable<DroppedFile> {
        /// <summary>
        /// Set by the <c>EguiWinit</c> backend.
        /// </summary>
        public string? Path;
        /// <summary>
        /// Name of the file. Set by the <c>Eframe</c> web backend.
        /// </summary>
        public string Name;
        /// <summary>
        /// With the <c>Eframe</c> web backend, this is set to the mime-type of the file (if available).
        /// </summary>
        public string Mime;
        /// <summary>
        /// Set by the <c>Eframe</c> web backend.
        /// </summary>
        public SystemTime? LastModified;
        /// <summary>
        /// Set by the <c>Eframe</c> web backend.
        /// </summary>
        public ImmutableList<byte>? Bytes;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_option_str(Path, serializer);
            serializer.serialize_str(Name);
            serializer.serialize_str(Mime);
            TraitHelpers.serialize_option_SystemTime(LastModified, serializer);
            TraitHelpers.serialize_option_vector_u8(Bytes, serializer);
            serializer.decrease_container_depth();
        }

        internal static DroppedFile Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            DroppedFile obj = new DroppedFile {
            	Path = TraitHelpers.deserialize_option_str(deserializer),
            	Name = deserializer.deserialize_str(),
            	Mime = deserializer.deserialize_str(),
            	LastModified = TraitHelpers.deserialize_option_SystemTime(deserializer),
            	Bytes = TraitHelpers.deserialize_option_vector_u8(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is DroppedFile other && Equals(other);

        public static bool operator ==(DroppedFile left, DroppedFile right) => Equals(left, right);

        public static bool operator !=(DroppedFile left, DroppedFile right) => !Equals(left, right);

        public bool Equals(DroppedFile other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Path.Equals(other.Path)) return false;
            if (!Name.Equals(other.Name)) return false;
            if (!Mime.Equals(other.Mime)) return false;
            if (!LastModified.Equals(other.LastModified)) return false;
            if (!Bytes.Equals(other.Bytes)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Path.GetHashCode();
                value = 31 * value + Name.GetHashCode();
                value = 31 * value + Mime.GetHashCode();
                value = 31 * value + LastModified.GetHashCode();
                value = 31 * value + Bytes.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
