using System.Diagnostics.CodeAnalysis;

namespace Egui;

/// <summary>
/// What sort of interaction is a widget sensitive to?
/// </summary>
public readonly struct Sense
{
    /// <summary>
    /// Only hovers detected.
    /// </summary>
    public static readonly Sense Hover = new Sense(0);

    /// <summary>
    /// Buttons, sliders, windows, ...
    /// </summary>
    public static readonly Sense ClickNoFocus = new Sense(1 << 0);

    /// <summary>
    /// Sliders, windows, scroll bars, scroll areas, ...
    /// </summary>
    public static readonly Sense DragNoFocus = new Sense(1 << 1);

    /// <summary>
    /// This widget wants focus.
    ///
    /// Anything interactive + labels that can be focused
    /// for the benefit of screen readers.
    /// </summary>
    public static readonly Sense Focusable = new Sense(1 << 2);

    /// <summary>
    /// Sense drags and hover, but not clicks.
    /// </summary>
    public static readonly Sense Click = ClickNoFocus | Focusable;

    /// <summary>
    /// Sense drags and hover, but not clicks.
    /// </summary>
    public static readonly Sense Drag = DragNoFocus | Focusable;

    /// <summary>
    /// Sense both clicks, drags and hover (e.g. a slider or window).<br/>
    /// Note that this will introduce a latency when dragging, because when the user starts a press egui can't know if this is the start of a click or a drag, and it won't know until the cursor has either moved a certain distance, or the user has released the mouse button.<br/>
    /// See <c>IsDecidedlyDragging</c> for details.
    /// </summary>
    public static readonly Sense ClickAndDrag = ClickNoFocus | DragNoFocus | Focusable;

    /// <summary>
    /// The internal value of the enum.
    /// </summary>
    private readonly byte _value;

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="value">The underlying enum value to assign.</param>
    private Sense(byte value)
    {
        _value = value;
    }

    /// <summary>
    /// Returns true if we sense either clicks or drags.
    /// </summary>
    public bool Interactive => EguiMarshal.Call<byte, bool>(EguiFn.egui_sense_Sense_interactive, _value);

    public bool SensesClick => EguiMarshal.Call<byte, bool>(EguiFn.egui_sense_Sense_senses_click, _value);

    public bool SensesDrag => EguiMarshal.Call<byte, bool>(EguiFn.egui_sense_Sense_senses_drag, _value);

    public bool IsFocusable => EguiMarshal.Call<byte, bool>(EguiFn.egui_sense_Sense_is_focusable, _value);

    internal void Serialize(Serde.ISerializer serializer) {
        serializer.increase_container_depth();
        serializer.serialize_variant_index(_value);
        serializer.decrease_container_depth();
    }

    internal static Sense Deserialize(Serde.IDeserializer deserializer) {
        deserializer.increase_container_depth();
        int index = deserializer.deserialize_variant_index();
        if (!Enum.IsDefined(typeof(Sense), index))
            throw new Serde.DeserializationException("Unknown variant index for Sense: " + index);
        Sense value = (Sense)index;
        deserializer.decrease_container_depth();
        return value;
    }

    /// <summary>
    /// Inverts the current sense flags.
    /// </summary>
    /// <param name="flags">The set of flags.</param>
    /// <returns>The new sense value.</returns>
    public static Sense operator ~(Sense flags) => (Sense)~flags._value;

    /// <summary>
    /// Gets the underlying bitwise representation of the flags.
    /// </summary>
    /// <param name="flags">The flags in use.</param>
    public static explicit operator byte(Sense flags) => flags._value;

    /// <summary>
    /// Converts from a raw bitwise representation to a set of flags.
    /// </summary>
    /// <param name="flags">The underlying representation of the flags.</param>
    public static explicit operator Sense(byte flags) => new Sense((byte)(flags & 0b111));

    /// <summary>
    /// Takes the setwise <c>and</c> of the sense flags.
    /// </summary>
    /// <param name="lhs">The first set of flags.</param>
    /// <param name="rhs">The second set of flags.</param>
    /// <returns>The new sense value.</returns>
    public static Sense operator &(Sense lhs, Sense rhs) => new Sense((byte)(lhs._value | rhs._value));

    /// <summary>
    /// Takes the setwise <c>or</c> of the sense flags.
    /// </summary>
    /// <param name="lhs">The first set of flags.</param>
    /// <param name="rhs">The second set of flags.</param>
    /// <returns>The new sense value.</returns>
    public static Sense operator |(Sense lhs, Sense rhs) => new Sense((byte)(lhs._value | rhs._value));

    /// <summary>
    /// Determines whether two sense sets are equal.
    /// </summary>
    /// <param name="lhs">The first set of flags.</param>
    /// <param name="rhs">The second set of flags.</param>
    /// <returns>Whether the two were the same.</returns>
    public static bool operator ==(Sense lhs, Sense rhs) => lhs._value == rhs._value;

    /// <summary>
    /// Determines whether two sense sets are not equal.
    /// </summary>
    /// <param name="lhs">The first set of flags.</param>
    /// <param name="rhs">The second set of flags.</param>
    /// <returns>Whether the two were different.</returns>
    public static bool operator !=(Sense lhs, Sense rhs) => !(lhs == rhs);

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Sense rhs)
        {
            return this == rhs;
        }
        else
        {
            return false;
        }
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}