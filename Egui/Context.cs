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
    /// Request to discard the visual output of this pass,
    /// and to immediately do another one.
    ///
    /// This can be called to cover up visual glitches during a "sizing pass".
    /// For instance, when a <see cref="Grid"/>  is first shown we don't yet know the
    /// width and heights of its columns and rows. egui will do a best guess,
    /// but it will likely be wrong. Next pass it can read the sizes from the previous
    /// pass, and from there on the widths will be stable.
    /// This means the first pass will look glitchy, and ideally should not be shown to the user.
    /// So <see cref="Grid"/>  calls <see cref="RequestDiscard"/>  to cover up this glitches.
    ///
    /// There is a limit to how many passes egui will perform, set by <see cref="Options.MaxPasses"/>  (default=2).
    /// Therefore, the request might be declined.
    ///
    /// You can check if the current pass will be discarded with <see cref="WillDiscard"/> .
    ///
    /// You should be very conservative with when you call <see cref="RequestDiscard"/> ,
    /// as it will cause an extra ui pass, potentially leading to extra CPU use and frame judder.
    ///
    /// The given reason should be a human-readable string that explains why `request_discard`
    /// was called. This will be shown in certain debug situations, to help you figure out
    /// why a pass was discarded.
    /// </summary>
    public void RequestDiscard(string reason)
    {
        EguiMarshal.Call(EguiFn.egui_context_Context_request_discard, Handle.ptr, reason);
    }

    /// <summary>
    /// Run the ui code for one frame.<br/>
    /// At most <see cref="Options.MaxPasses"/>  calls will be issued to <paramref name="runUi"/>, and only on the rare occasion that <see cref="RequestDiscard"/> is called. Usually, it will only be called once.<br/>
    /// Put your widgets into a <see cref="SidePanel"/> , <see cref="TopBottomPanel"/> , <see cref="CentralPanel"/> , <see cref="Window"/>  or <see cref="Area"/> .<br/>
    /// Instead of calling run, you can alternatively use <see cref="BeginPass"/>  and <see cref="EndPass"/> .<br/>
    /// </summary>
    public FullOutput Run(RawInput input, Action<Context> runUi)
    {
        return EguiMarshal.Call<RawInput, FullOutput>(EguiFn.egui_context_Context_run, Handle.ptr, input);
    }

    /// <summary>
    /// Mutate the currently active <see cref="Egui.Style"/> used by all subsequent windows, panels etc. Use <see cref="AllStylesMut"/> to mutate both dark and light mode styles.
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