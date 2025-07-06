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
    /// How and when interaction happens.
    /// </summary>
    public partial struct Interaction : IEquatable<Interaction> {
        /// <summary>
        /// How close a widget must be to the mouse to have a chance to register as a click or drag.
        ///
        /// If this is larger than zero, it gets easier to hit widgets,
        /// which is important for e.g. touch screens.
        /// </summary>
        public float InteractRadius;
        /// <summary>
        /// Radius of the interactive area of the side of a window during drag-to-resize.
        /// </summary>
        public float ResizeGrabRadiusSide;
        /// <summary>
        /// Radius of the interactive area of the corner of a window during drag-to-resize.
        /// </summary>
        public float ResizeGrabRadiusCorner;
        /// <summary>
        /// If <c>False</c>, tooltips will show up anytime you hover anything, even if mouse is still moving
        /// </summary>
        public bool ShowTooltipsOnlyWhenStill;
        /// <summary>
        /// Delay in seconds before showing tooltips after the mouse stops moving
        /// </summary>
        public float TooltipDelay;
        /// <summary>
        /// If you have waited for a tooltip and then hover some other widget within
        /// this many seconds, then show the new tooltip right away,
        /// skipping <c>TooltipDelay</c>.
        ///
        /// This lets the user quickly move over some dead space to hover the next thing.
        /// </summary>
        public float TooltipGraceTime;
        /// <summary>
        /// Can you select the text on a <c>Label</c> by default?
        /// </summary>
        public bool SelectableLabels;
        /// <summary>
        /// Can the user select text that span multiple labels?
        ///
        /// The default is <c>True</c>, but text selection can be slightly glitchy,
        /// so you may want to disable it.
        /// </summary>
        public bool MultiWidgetTextSelect;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_f32(InteractRadius);
            serializer.serialize_f32(ResizeGrabRadiusSide);
            serializer.serialize_f32(ResizeGrabRadiusCorner);
            serializer.serialize_bool(ShowTooltipsOnlyWhenStill);
            serializer.serialize_f32(TooltipDelay);
            serializer.serialize_f32(TooltipGraceTime);
            serializer.serialize_bool(SelectableLabels);
            serializer.serialize_bool(MultiWidgetTextSelect);
            serializer.decrease_container_depth();
        }

        internal static Interaction Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Interaction obj = new Interaction {
            	InteractRadius = deserializer.deserialize_f32(),
            	ResizeGrabRadiusSide = deserializer.deserialize_f32(),
            	ResizeGrabRadiusCorner = deserializer.deserialize_f32(),
            	ShowTooltipsOnlyWhenStill = deserializer.deserialize_bool(),
            	TooltipDelay = deserializer.deserialize_f32(),
            	TooltipGraceTime = deserializer.deserialize_f32(),
            	SelectableLabels = deserializer.deserialize_bool(),
            	MultiWidgetTextSelect = deserializer.deserialize_bool() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Interaction other && Equals(other);

        public static bool operator ==(Interaction left, Interaction right) => Equals(left, right);

        public static bool operator !=(Interaction left, Interaction right) => !Equals(left, right);

        public bool Equals(Interaction other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!InteractRadius.Equals(other.InteractRadius)) return false;
            if (!ResizeGrabRadiusSide.Equals(other.ResizeGrabRadiusSide)) return false;
            if (!ResizeGrabRadiusCorner.Equals(other.ResizeGrabRadiusCorner)) return false;
            if (!ShowTooltipsOnlyWhenStill.Equals(other.ShowTooltipsOnlyWhenStill)) return false;
            if (!TooltipDelay.Equals(other.TooltipDelay)) return false;
            if (!TooltipGraceTime.Equals(other.TooltipGraceTime)) return false;
            if (!SelectableLabels.Equals(other.SelectableLabels)) return false;
            if (!MultiWidgetTextSelect.Equals(other.MultiWidgetTextSelect)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + InteractRadius.GetHashCode();
                value = 31 * value + ResizeGrabRadiusSide.GetHashCode();
                value = 31 * value + ResizeGrabRadiusCorner.GetHashCode();
                value = 31 * value + ShowTooltipsOnlyWhenStill.GetHashCode();
                value = 31 * value + TooltipDelay.GetHashCode();
                value = 31 * value + TooltipGraceTime.GetHashCode();
                value = 31 * value + SelectableLabels.GetHashCode();
                value = 31 * value + MultiWidgetTextSelect.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
