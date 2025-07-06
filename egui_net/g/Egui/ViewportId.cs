#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct ViewportId : IEquatable<ViewportId> {
        public Id Value;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Value.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static ViewportId Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            ViewportId obj = new ViewportId {
            	Value = Id.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is ViewportId other && Equals(other);

        public static bool operator ==(ViewportId left, ViewportId right) => Equals(left, right);

        public static bool operator !=(ViewportId left, ViewportId right) => !Equals(left, right);

        public bool Equals(ViewportId other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Value.Equals(other.Value)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Value.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
