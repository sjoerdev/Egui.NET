#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct ViewportIdPair : IEquatable<ViewportIdPair> {
        public ViewportId This;
        public ViewportId Parent;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            This.Serialize(serializer);
            Parent.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static ViewportIdPair Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            ViewportIdPair obj = new ViewportIdPair {
            	This = ViewportId.Deserialize(deserializer),
            	Parent = ViewportId.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is ViewportIdPair other && Equals(other);

        public static bool operator ==(ViewportIdPair left, ViewportIdPair right) => Equals(left, right);

        public static bool operator !=(ViewportIdPair left, ViewportIdPair right) => !Equals(left, right);

        public bool Equals(ViewportIdPair other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!This.Equals(other.This)) return false;
            if (!Parent.Equals(other.Parent)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + This.GetHashCode();
                value = 31 * value + Parent.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
