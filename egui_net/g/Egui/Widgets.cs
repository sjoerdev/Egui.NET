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
    /// The visuals of widgets for different states of interaction.
    /// </summary>
    public partial struct Widgets : IEquatable<Widgets> {
        /// <summary>
        /// The style of a widget that you cannot interact with.
        /// * <c>Noninteractive.bgStroke</c> is the outline of windows.
        /// * <c>Noninteractive.bgFill</c> is the background color of windows.
        /// * <c>Noninteractive.fgStroke</c> is the normal text color.
        /// </summary>
        public WidgetVisuals Noninteractive;
        /// <summary>
        /// The style of an interactive widget, such as a button, at rest.
        /// </summary>
        public WidgetVisuals Inactive;
        /// <summary>
        /// The style of an interactive widget while you hover it, or when it is highlighted.
        ///
        /// See <c>Hovered</c>, <c>Highlighted</c> and <c>Highlight</c>.
        /// </summary>
        public WidgetVisuals Hovered;
        /// <summary>
        /// The style of an interactive widget as you are clicking or dragging it.
        /// </summary>
        public WidgetVisuals Active;
        /// <summary>
        /// The style of a button that has an open menu beneath it (e.g. a combo-box)
        /// </summary>
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
