using System;

namespace Serde {
    internal static class PrimitiveExtensions {
        internal static void Serialize(this uint value, Serde.ISerializer serializer) {
            serializer.serialize_u32(value);
        }
    }
}