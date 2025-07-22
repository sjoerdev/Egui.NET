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
    /// A list of contexts that have been created.
    /// This list is used for deserializing the <see cref="Response.Ctx"/>
    /// field without allocating a new context object.
    /// </summary>
    private static readonly Dictionary<nuint, WeakReference<Context>> _contexts = new Dictionary<nuint, WeakReference<Context>>();

    /// <summary>
    /// Paint on top of everything else
    /// </summary>
    public Painter DebugPainter => new Painter(this, EguiMarshal.Call<EguiHandle>(EguiFn.egui_context_Context_debug_painter, Handle.ptr));

    /// <summary>
    /// A unique ID used for internal tracking.
    /// </summary>
    internal readonly nuint Id;

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
        lock (_contexts)
        {
            foreach (var pair in _contexts)
            {
                if (!pair.Value.TryGetTarget(out _))
                {
                    _contexts.Remove(pair.Key);
                }
            }

            Id = EguiMarshal.Call<nuint>(EguiFn.egui_context_Context_ref_id, Handle.ptr);
            _contexts.Add(Id, new WeakReference<Context>(this));
        }
    }

    /// <summary>
    /// Ensures that the object is not garbage-collected until the context has no other references left.
    /// </summary>
    ~Context()
    {
        if (1 < EguiMarshal.Call<nuint>(EguiFn.egui_context_Context_ref_count, Handle.ptr))
        {
            GC.ReRegisterForFinalize(this);
        }
    }

    /// <summary>
    /// Retrieves an existing context.
    /// The result of this method is only defined if <see cref="ptr"/>
    /// is a pointer to a valid context previously created with the constructor.
    /// </summary>
    internal static Context FromId(nuint id)
    {
        lock (_contexts)
        {
            if (_contexts.TryGetValue(id, out var value)
                && value.TryGetTarget(out var target))
            {
                return target;
            }
        }

        throw new ArgumentException("Unable to find context with ID", nameof(id));
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
    /// Get a full-screen painter for a new or existing layer
    /// </summary>
    public Painter LayerPainter(LayerId layerId) => new Painter(this, EguiMarshal.Call<LayerId, EguiHandle>(EguiFn.egui_context_Context_layer_painter, Handle.ptr, layerId));

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
        using var callback = new EguiCallback(_ => runUi(this));
        return EguiMarshal.Call<RawInput, EguiCallback, FullOutput>(EguiFn.egui_context_Context_run, Handle.ptr, input, callback);
    }

    /// <summary>
    /// Show the state of egui, including its input and output.
    /// </summary>
    public void InspectionUi(Ui ui)
    {
        ui.AssertInitialized();
        EguiMarshal.Call(EguiFn.egui_context_Context_inspection_ui, Handle.ptr, ui.Ptr);
    }

    /// <summary>
    /// Show stats about different image loaders.
    /// </summary>
    public void LoadersUi(Ui ui)
    {
        ui.AssertInitialized();
        EguiMarshal.Call(EguiFn.egui_context_Context_loaders_ui, Handle.ptr, ui.Ptr);
    }

    /// <summary>
    /// Shows the contents of <see cref="Memory"/>. 
    /// </summary>
    public void MemoryUi(Ui ui)
    {
        ui.AssertInitialized();
        EguiMarshal.Call(EguiFn.egui_context_Context_memory_ui, Handle.ptr, ui.Ptr);
    }

    /// <summary>
    /// Show a ui for settings (style and tessellation options).
    /// </summary>
    public void SettingsUi(Ui ui)
    {
        ui.AssertInitialized();
        EguiMarshal.Call(EguiFn.egui_context_Context_settings_ui, Handle.ptr, ui.Ptr);
    }

    /// <summary>
    /// Edit the <see cref="Style"/>. 
    /// </summary>
    public void StyleUi(Ui ui, Theme theme)
    {
        ui.AssertInitialized();
        EguiMarshal.Call(EguiFn.egui_context_Context_style_ui, Handle.ptr, ui.Ptr, theme);
    }

    /// <summary>
    /// Show stats about the allocated textures.
    /// </summary>
    public void TextureUi(Ui ui)
    {
        ui.AssertInitialized();
        EguiMarshal.Call(EguiFn.egui_context_Context_texture_ui, Handle.ptr, ui.Ptr);
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