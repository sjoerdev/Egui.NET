#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Selection : IEquatable<Selection> {
        public Color32 BgFill;
        public Stroke Stroke;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            BgFill.Serialize(serializer);
            Stroke.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static Selection Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Selection obj = new Selection {
            	BgFill = Color32.Deserialize(deserializer),
            	Stroke = Stroke.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Selection other && Equals(other);

        public static bool operator ==(Selection left, Selection right) => Equals(left, right);

        public static bool operator !=(Selection left, Selection right) => !Equals(left, right);

        public bool Equals(Selection other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!BgFill.Equals(other.BgFill)) return false;
            if (!Stroke.Equals(other.Stroke)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + BgFill.GetHashCode();
                value = 31 * value + Stroke.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
