namespace Egui;

/// <summary>
/// Additional methods for <see cref="Sense"/>.
/// </summary>
public static class SenseExtensions
{
    /// <summary>
    /// Returns true if we sense either clicks or drags.
    /// </summary>
    public static bool Interactive(this Sense sense)
    {
        return EguiMarshal.Call<byte, bool>(EguiFn.egui_sense_Sense_interactive, (byte)sense);
    }

    public static bool SensesClick(this Sense sense)
    {
        return EguiMarshal.Call<byte, bool>(EguiFn.egui_sense_Sense_senses_click, (byte)sense);
    }

    public static bool SensesDrag(this Sense sense)
    {
        return EguiMarshal.Call<byte, bool>(EguiFn.egui_sense_Sense_senses_drag, (byte)sense);
    }

    public static bool IsFocusable(this Sense sense)
    {
        return EguiMarshal.Call<byte, bool>(EguiFn.egui_sense_Sense_is_focusable, (byte)sense);
    }

    internal static void Serialize(this Sense value, Serde.ISerializer serializer) {
        serializer.increase_container_depth();
        serializer.serialize_variant_index((int)value);
        serializer.decrease_container_depth();
    }

    internal static Sense Deserialize(Serde.IDeserializer deserializer) {
        deserializer.increase_container_depth();
        int index = deserializer.deserialize_variant_index();
        if (!Enum.IsDefined(typeof(Sense), index))
            throw new Serde.DeserializationException("Unknown variant index for Align: " + index);
        Sense value = (Sense)index;
        deserializer.decrease_container_depth();
        return value;
    }
}