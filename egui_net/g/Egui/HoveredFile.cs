#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct HoveredFile : IEquatable<HoveredFile> {
        public string? Path;
        public string Mime;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            TraitHelpers.serialize_option_str(Path, serializer);
            serializer.serialize_str(Mime);
            serializer.decrease_container_depth();
        }

        internal static HoveredFile Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            HoveredFile obj = new HoveredFile {
            	Path = TraitHelpers.deserialize_option_str(deserializer),
            	Mime = deserializer.deserialize_str() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is HoveredFile other && Equals(other);

        public static bool operator ==(HoveredFile left, HoveredFile right) => Equals(left, right);

        public static bool operator !=(HoveredFile left, HoveredFile right) => !Equals(left, right);

        public bool Equals(HoveredFile other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Path.Equals(other.Path)) return false;
            if (!Mime.Equals(other.Mime)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Path.GetHashCode();
                value = 31 * value + Mime.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
