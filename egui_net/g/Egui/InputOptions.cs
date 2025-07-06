#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct InputOptions : IEquatable<InputOptions> {
        public float MaxClickDist;
        public double MaxClickDuration;
        public double MaxDoubleClickDelay;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_f32(MaxClickDist);
            serializer.serialize_f64(MaxClickDuration);
            serializer.serialize_f64(MaxDoubleClickDelay);
            serializer.decrease_container_depth();
        }

        internal static InputOptions Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            InputOptions obj = new InputOptions {
            	MaxClickDist = deserializer.deserialize_f32(),
            	MaxClickDuration = deserializer.deserialize_f64(),
            	MaxDoubleClickDelay = deserializer.deserialize_f64() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is InputOptions other && Equals(other);

        public static bool operator ==(InputOptions left, InputOptions right) => Equals(left, right);

        public static bool operator !=(InputOptions left, InputOptions right) => !Equals(left, right);

        public bool Equals(InputOptions other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!MaxClickDist.Equals(other.MaxClickDist)) return false;
            if (!MaxClickDuration.Equals(other.MaxClickDuration)) return false;
            if (!MaxDoubleClickDelay.Equals(other.MaxDoubleClickDelay)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + MaxClickDist.GetHashCode();
                value = 31 * value + MaxClickDuration.GetHashCode();
                value = 31 * value + MaxDoubleClickDelay.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
