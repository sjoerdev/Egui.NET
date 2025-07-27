namespace Egui;

internal partial struct EguiHandle
{
    /// <summary>
    /// Serializes an instance of this value.
    /// </summary>
    /// <param name="serializer">The serializer to use.</param>
    internal static void Serialize(BincodeSerializer serializer, EguiHandle obj)
    {
        serializer.increase_container_depth();
        serializer.serialize_u64(obj.ptr);
        serializer.serialize_u64(obj.metadata);
        serializer.decrease_container_depth();
    }

    /// <summary>
    /// Deserializes an instance of this value.
    /// </summary>
    /// <param name="deserializer">The deserializer to use.</param>
    /// <returns>The object that was deserialized.</returns>
    internal static EguiHandle Deserialize(BincodeDeserializer deserializer)
    {
        deserializer.increase_container_depth();
        EguiHandle obj = default;
        obj.ptr = (nuint)deserializer.deserialize_u64();
        obj.metadata = (nuint)deserializer.deserialize_u64();
        deserializer.decrease_container_depth();
        return obj;
    }
}