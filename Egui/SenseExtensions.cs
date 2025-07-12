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
}