using System.Drawing;

namespace Egui;

/// <summary>
/// This format is used for space-efficient color representation (32 bits).
///
/// Instead of manipulating this directly it is often better
/// to first convert it to either <c>Rgba</c> or <c>Hsva</c>.
///
/// Internally this uses 0-255 gamma space <c>sRGBA</c> color with _premultiplied alpha_.
///
/// It's the non-linear ("gamma") values that are multiplied with the alpha.
///
/// Premultiplied alpha means that the color values have been pre-multiplied with the alpha (opacity).
/// This is in contrast with "normal" RGBA, where the alpha is _separate_ (or "unmultiplied").
/// Using premultiplied alpha has some advantages:
/// * It allows encoding additive colors
/// * It is the better way to blend colors, e.g. when filtering texture colors
/// * Because the above, it is the better way to encode colors in a GPU texture
///
/// The color space is assumed to be <c>sRGB</c>.
///
/// All operations on <see cref="Color32"/>  are done in "gamma space" (see <https://en.wikipedia.org/wiki/SRGB>).
/// This is not physically correct, but it is fast and sometimes more perceptually even than linear space.
/// If you instead want to perform these operations in linear-space color, use <c>Rgba</c>.
///
/// An <c>alpha=0<c/> means the color is to be treated as an additive color.
/// </summary>
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