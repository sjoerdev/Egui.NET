#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct LayerId : IEquatable<LayerId> {
        public Order Order;
        public Id Id;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            Order.Serialize(serializer);
            Id.Serialize(serializer);
            serializer.decrease_container_depth();
        }

        internal static LayerId Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            LayerId obj = new LayerId {
            	Order = OrderExtensions.Deserialize(deserializer),
            	Id = Id.Deserialize(deserializer) };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is LayerId other && Equals(other);

        public static bool operator ==(LayerId left, LayerId right) => Equals(left, right);

        public static bool operator !=(LayerId left, LayerId right) => !Equals(left, right);

        public bool Equals(LayerId other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!Order.Equals(other.Order)) return false;
            if (!Id.Equals(other.Id)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + Order.GetHashCode();
                value = 31 * value + Id.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
