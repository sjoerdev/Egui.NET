namespace Egui;


public unsafe partial struct Color32 : IEquatable<Color32>
{
    /// <summary>
    /// The underlying encoded color.
    /// </summary>
    private uint _value;

    /// <summary>
    /// Serializes an instance of this type.
    /// </summary>
    internal static void Serialize(Serde.ISerializer serializer, Color32 value) => value.Serialize(serializer);

    /// <summary>
    /// Serializes this object.
    /// </summary>
    internal void Serialize(Serde.ISerializer serializer)
    {
        serializer.increase_container_depth();
        serializer.serialize_u32(_value);
        serializer.decrease_container_depth();
    }

    /// <summary>
    /// Deserializes an instance of this type.
    /// </summary>
    internal static Color32 Deserialize(Serde.IDeserializer deserializer)
    {
        deserializer.increase_container_depth();
        Color32 obj = default;
        obj._value = deserializer.deserialize_u32();
        deserializer.decrease_container_depth();
        return obj;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Color32 other && Equals(other);

    /// <summary>
    /// Compares two objects for equality.
    /// </summary>
    /// <param name="left">The first object.</param>
    /// <param name="right">The second object.</param>
    /// <returns>Whether the two are the same.</returns>
    public static bool operator ==(Color32 left, Color32 right) => Equals(left, right);

    /// <summary>
    /// Compares two objects for inequality.
    /// </summary>
    /// <param name="left">The first object.</param>
    /// <param name="right">The second object.</param>
    /// <returns>Whether the two are different.</returns>
    public static bool operator !=(Color32 left, Color32 right) => !Equals(left, right);

    /// <inheritdoc/>
    public bool Equals(Color32 other)
    {
        if (_value != other._value) return false;
        return true;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            int value = 7;
            value = 31 * value + _value.GetHashCode();
            return value;
        }
    }
}