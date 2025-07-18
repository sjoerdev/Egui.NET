namespace Egui;

/// <summary>
/// Your handle to egui.<br/>
///
/// This is the first thing you need when working with egui.
/// Contains the <see cref="InputState"/>, <see cref="Memory"/>, <see cref="PlatformOutput"/>, and more.
///
/// <see cref="Ui"/>  has many of the same accessor functions, and the same applies there.
/// </summary>
public sealed partial class Context : EguiObject
{
    /// <summary>
    /// A function that modifies a style.
    /// </summary>
    /// <param name="style">The style to modify.</param>
    public delegate void MutateStyle(ref Style style);

    /// <summary>
    /// Returns the "default value" for a type.
    /// Default values are often some kind of initial value, identity value, or anything else that may make sense as a default.
    /// </summary>
    public Context() : base(EguiMarshal.Call<EguiHandle>(EguiFn.egui_context_Context_default, 0))
    {

    }

    /// <summary>
    /// Mutate the <see cref="Style"/>s used by all subsequent windows, panels etc. in both dark and light mode.
    /// </summary>
    public void AllStylesMut(MutateStyle mutateStyle)
    {
        StyleMutOf(Theme.Dark, mutateStyle);
        StyleMutOf(Theme.Light, mutateStyle);
    }

    /// <summary>
    /// Mutate the currently active <see cref="Style"/>  used by all subsequent windows, panels etc. Use <see cref="AllStylesMut"/> to mutate both dark and light mode styles.
    /// </summary>
    /// <param name="mutateStyle"></param>
    public void StyleMut(MutateStyle mutateStyle)
    {
        var style = Style;
        mutateStyle(ref style);
        SetStyle(style);
    }

    /// <summary>
    /// Mutate the <see cref="Style"/>  used by all subsequent windows, panels etc.
    /// </summary>
    public void StyleMutOf(Theme theme, MutateStyle mutateStyle)
    {
        var style = StyleOf(theme);
        mutateStyle(ref style);
        SetStyleOf(theme, style);
    }
}