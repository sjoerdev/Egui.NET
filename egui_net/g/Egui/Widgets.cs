#pragma warning disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Widgets : IEquatable<Widgets> {
        public WidgetVisuals Noninteractive;
        public WidgetVisuals Inactive;
        public WidgetVisuals Hovered;
        public WidgetVisuals Active;
        public WidgetVisuals Open;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Noninteractive.Serialize(serializer);
            Inactive.Serialize(serializer);
            Hovered.Serialize(serializer);
            Active.Serialize(serializer);
            Open.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static Widgets Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Widgets obj = new Widgets {
            	Noninteractive = WidgetVisuals.Deserialize(deserializer),
            	Inactive = WidgetVisuals.Deserialize(deserializer),
            	Hovered = WidgetVisuals.Deserialize(deserializer),
            	Active = WidgetVisuals.Deserialize(deserializer),
            	Open = WidgetVisuals.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Widgets other && Equals(other);

        public static bool operator ==(Widgets left, Widgets right) => Equals(left, right);

        public static bool operator !=(Widgets left, Widgets right) => !Equals(left, right);

        public bool Equals(Widgets other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Noninteractive.Equals(other.Noninteractive)) return false;
            if (!Inactive.Equals(other.Inactive)) return false;
            if (!Hovered.Equals(other.Hovered)) return false;
            if (!Active.Equals(other.Active)) return false;
            if (!Open.Equals(other.Open)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Noninteractive.GetHashCode();
                value = 31 * value + Inactive.GetHashCode();
                value = 31 * value + Hovered.GetHashCode();
                value = 31 * value + Active.GetHashCode();
                value = 31 * value + Open.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
