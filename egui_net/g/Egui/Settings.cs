#pragma warning disable
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Egui {

    public partial struct Settings : IEquatable<Settings> {
        /// <summary>
        /// Maximum number of undos.
        /// If your state is resource intensive, you should keep this low.
        ///
        /// Default: <c>100</c>
        /// </summary>
        public ulong MaxUndos;
        /// <summary>
        /// When that state hasn't changed for this many seconds,
        /// create a new undo point (if one is needed).
        ///
        /// Default value: <c>1.0</c> seconds.
        /// </summary>
        public float StableTime;
        /// <summary>
        /// If the state is changing so often that we never get to <c>StableTime</c>,
        /// then still create a save point every <c>AutoSaveInterval</c> seconds,
        /// so we have something to undo to.
        ///
        /// Default value: <c>30</c> seconds.
        /// </summary>
        public float AutoSaveInterval;


        internal void Serialize(Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_u64(MaxUndos);
            serializer.serialize_f32(StableTime);
            serializer.serialize_f32(AutoSaveInterval);
            serializer.decrease_container_depth();
        }

        internal static Settings Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            Settings obj = new Settings {
            	MaxUndos = deserializer.deserialize_u64(),
            	StableTime = deserializer.deserialize_f32(),
            	AutoSaveInterval = deserializer.deserialize_f32() };
            deserializer.decrease_container_depth();
            return obj;
        }
        public override bool Equals(object? obj) => obj is Settings other && Equals(other);

        public static bool operator ==(Settings left, Settings right) => Equals(left, right);

        public static bool operator !=(Settings left, Settings right) => !Equals(left, right);

        public bool Equals(Settings other) {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!MaxUndos.Equals(other.MaxUndos)) return false;
            if (!StableTime.Equals(other.StableTime)) return false;
            if (!AutoSaveInterval.Equals(other.AutoSaveInterval)) return false;
            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int value = 7;
                value = 31 * value + MaxUndos.GetHashCode();
                value = 31 * value + StableTime.GetHashCode();
                value = 31 * value + AutoSaveInterval.GetHashCode();
                return value;
            }
        }

    }

} // end of namespace Egui
