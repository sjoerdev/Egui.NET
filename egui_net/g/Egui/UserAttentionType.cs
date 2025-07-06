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
    /// Types of attention to request from a user when a native window is not in focus.
    ///
    /// See winit's documentationuser_attention_type for platform-specific meaning of the attention types.
    ///
    /// user_attention_type: https://docs.rs/winit/latest/winit/window/enum.UserAttentionType.html
    /// </summary>
    public enum UserAttentionType {
            /// <summary>
            /// Request an elevated amount of animations and flair for the window and the task bar or dock icon.
            /// </summary>
            Critical = 0,
            /// <summary>
            /// Request a standard amount of attention-grabbing actions.
            /// </summary>
            Informational = 1,
            /// <summary>
            /// Reset the attention request and interrupt related animations and flashes.
            /// </summary>
            Reset = 2,
    }
    internal static class UserAttentionTypeExtensions {

        internal static void Serialize(this UserAttentionType value, Serde.ISerializer serializer) {
            serializer.increase_container_depth();
            serializer.serialize_variant_index((int)value);
            serializer.decrease_container_depth();
        }

        internal static UserAttentionType Deserialize(Serde.IDeserializer deserializer) {
            deserializer.increase_container_depth();
            int index = deserializer.deserialize_variant_index();
            if (!Enum.IsDefined(typeof(UserAttentionType), index))
                throw new Serde.DeserializationException("Unknown variant index for UserAttentionType: " + index);
            UserAttentionType value = (UserAttentionType)index;
            deserializer.decrease_container_depth();
            return value;
        }
    }

} // end of namespace Egui
