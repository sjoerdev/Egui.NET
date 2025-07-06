#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct WidgetInfo : IEquatable<WidgetInfo> {
        public WidgetType Typ;
        public bool Enabled;
        public string? Label;
        public string? CurrentTextValue;
        public string? PrevTextValue;
        public bool? Selected;
        public double? Value;
        public RangeInclusive? TextSelection;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Typ.Serialize(serializer);
            serializer.serialize_bool(Enabled);
            TraitHelpers.serialize_option_str(Label, serializer);
            TraitHelpers.serialize_option_str(CurrentTextValue, serializer);
            TraitHelpers.serialize_option_str(PrevTextValue, serializer);
            TraitHelpers.serialize_option_bool(Selected, serializer);
            TraitHelpers.serialize_option_f64(Value, serializer);
            TraitHelpers.serialize_option_RangeInclusive(TextSelection, serializer);
            serializer.decrease_container_depth();
        }

        internal static WidgetInfo Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            WidgetInfo obj = new WidgetInfo {
            	Typ = WidgetTypeExtensions.Deserialize(deserializer),
            	Enabled = deserializer.deserialize_bool(),
            	Label = TraitHelpers.deserialize_option_str(deserializer),
            	CurrentTextValue = TraitHelpers.deserialize_option_str(deserializer),
            	PrevTextValue = TraitHelpers.deserialize_option_str(deserializer),
            	Selected = TraitHelpers.deserialize_option_bool(deserializer),
            	Value = TraitHelpers.deserialize_option_f64(deserializer),
            	TextSelection = TraitHelpers.deserialize_option_RangeInclusive(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is WidgetInfo other && Equals(other);

        public static bool operator ==(WidgetInfo left, WidgetInfo right) => Equals(left, right);

        public static bool operator !=(WidgetInfo left, WidgetInfo right) => !Equals(left, right);

        public bool Equals(WidgetInfo other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Typ.Equals(other.Typ)) return false;
            if (!Enabled.Equals(other.Enabled)) return false;
            if (!Label.Equals(other.Label)) return false;
            if (!CurrentTextValue.Equals(other.CurrentTextValue)) return false;
            if (!PrevTextValue.Equals(other.PrevTextValue)) return false;
            if (!Selected.Equals(other.Selected)) return false;
            if (!Value.Equals(other.Value)) return false;
            if (!TextSelection.Equals(other.TextSelection)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Typ.GetHashCode();
                value = 31 * value + Enabled.GetHashCode();
                value = 31 * value + Label.GetHashCode();
                value = 31 * value + CurrentTextValue.GetHashCode();
                value = 31 * value + PrevTextValue.GetHashCode();
                value = 31 * value + Selected.GetHashCode();
                value = 31 * value + Value.GetHashCode();
                value = 31 * value + TextSelection.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
