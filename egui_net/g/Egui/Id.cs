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
    /// egui tracks widgets frame-to-frame using <c>Id</c>s.
    ///
    /// For instance, if you start dragging a slider one frame, egui stores
    /// the sliders <c>Id</c> as the current active id so that next frame when
    /// you move the mouse the same slider changes, even if the mouse has
    /// moved outside the slider.
    ///
    /// For some widgets <c>Id</c>s are also used to persist some state about the
    /// widgets, such as Window position or whether not a collapsing header region is open.
    ///
    /// This implies that the <c>Id</c>s must be unique.
    ///
    /// For simple things like sliders and buttons that don't have any memory and
    /// doesn't move we can use the location of the widget as a source of identity.
    /// For instance, a slider only needs a unique and persistent ID while you are
    /// dragging the slider. As long as it is still while moving, that is fine.
    ///
    /// For things that need to persist state even after moving (windows, collapsing headers)
    /// the location of the widgets is obviously not good enough. For instance,
    /// a collapsing region needs to remember whether or not it is open even
    /// if the layout next frame is different and the collapsing is not lower down
    /// on the screen.
    ///
    /// Then there are widgets that need no identifiers at all, like labels,
    /// because they have no state nor are interacted with.
    ///
    /// This is niche-optimized to that <c>Option<id></c> is the same size as <c>Id</c>.
    /// </summary>
    public partial struct Id : IEquatable<Id> {
        public ulong Value;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_u64(Value);
            serializer.decrease_container_depth();
        }

        internal static Id Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Id obj = new Id {
            	Value = deserializer.deserialize_u64() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Id other && Equals(other);

        public static bool operator ==(Id left, Id right) => Equals(left, right);

        public static bool operator !=(Id left, Id right) => !Equals(left, right);

        public bool Equals(Id other) {
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
