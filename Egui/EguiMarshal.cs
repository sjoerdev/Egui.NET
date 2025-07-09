using Serde;

namespace Egui;

internal static class EguiMarshal {
    /// <summary>
    /// The serializer to use for temporary operations.
    /// </summary>
    [ThreadStatic]
    private static BinarySerializer? _serializer;

    //public static ReadOnlySpan<byte> Serialize<T>()
}