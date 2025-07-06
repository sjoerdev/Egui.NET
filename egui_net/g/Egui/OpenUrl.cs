#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct OpenUrl : IEquatable<OpenUrl> {
        public string Url;
        public bool NewTab;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_str(Url);
            serializer.serialize_bool(NewTab);
            serializer.decrease_container_depth();
        }

        internal static OpenUrl Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            OpenUrl obj = new OpenUrl {
            	Url = deserializer.deserialize_str(),
            	NewTab = deserializer.deserialize_bool() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is OpenUrl other && Equals(other);

        public static bool operator ==(OpenUrl left, OpenUrl right) => Equals(left, right);

        public static bool operator !=(OpenUrl left, OpenUrl right) => !Equals(left, right);

        public bool Equals(OpenUrl other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Url.Equals(other.Url)) return false;
            if (!NewTab.Equals(other.NewTab)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Url.GetHashCode();
                value = 31 * value + NewTab.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
