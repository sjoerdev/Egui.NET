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
    public Painter DebugPainter => new Painter(this, EguiMarshal.Call<nuint, EguiHandle>(EguiFn.egui_context_Context_debug_painter, Ptr));

    /// <summary>
    /// A unique ID used for internal tracking.
    /// </summary>
    internal readonly nuint Id;

    /// <summary>
    /// Returns the "default value" for a type.
    /// Default values are often some kind of initial value, identity value, or anything else that may make sense as a default.
    /// </summary>
    public Context() : base(EguiMarshal.Call<EguiHandle>(EguiFn.egui_context_Context_default))
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

            Id = EguiMarshal.Call<nuint, nuint>(EguiFn.egui_context_Context_ref_id, Ptr);
            _contexts.Add(Id, new WeakReference<Context>(this));
        }
    }

    /// <summary>
    /// Ensures that the object is not garbage-collected until the context has no other references left.
    /// </summary>
    ~Context()
    {
        if (1 < EguiMarshal.Call<nuint, nuint>(EguiFn.egui_context_Context_ref_count, Ptr))
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
    public void AllStylesMut(MutateDelegate<Style> mutateStyle)
    {
        StyleMutOf(Theme.Dark, mutateStyle);
        StyleMutOf(Theme.Light, mutateStyle);
    }

    /// <summary>
    /// Get a full-screen painter for a new or existing layer
    /// </summary>
    public Painter LayerPainter(LayerId layerId) => new Painter(this, EguiMarshal.Call<nuint, LayerId, EguiHandle>(EguiFn.egui_context_Context_layer_painter, Ptr, layerId));

    /// <summary>
    /// Run the ui code for one frame.<br/>
    /// At most <see cref="Options.MaxPasses"/>  calls will be issued to <paramref name="runUi"/>, and only on the rare occasion that <see cref="RequestDiscard"/> is called. Usually, it will only be called once.<br/>
    /// Put your widgets into a <see cref="SidePanel"/> , <see cref="TopBottomPanel"/> , <see cref="CentralPanel"/> , <see cref="Window"/>  or <see cref="Area"/> .<br/>
    /// Instead of calling run, you can alternatively use <see cref="BeginPass"/>  and <see cref="EndPass"/> .<br/>
    /// </summary>
    public FullOutput Run(RawInput input, Action<Context> runUi)
    {
        using var callback = new EguiCallback(_ => runUi(this));
        return EguiMarshal.Call<nuint, RawInput, EguiCallback, FullOutput>(EguiFn.egui_context_Context_run, Ptr, input, callback);
    }

    /// <summary>
    /// Mutate the currently active <see cref="Egui.Style"/> used by all subsequent windows, panels etc. Use <see cref="AllStylesMut"/> to mutate both dark and light mode styles.
    /// </summary>
    /// <param name="mutateStyle"></param>
    public void StyleMut(MutateDelegate<Style> mutateStyle)
    {
        var style = Style;
        try
        {
            mutateStyle(ref style);
        }
        finally
        {
            SetStyle(style);
        }
    }

    /// <summary>
    /// Mutate the <see cref="Style"/>  used by all subsequent windows, panels etc.
    /// </summary>
    public void StyleMutOf(Theme theme, MutateDelegate<Style> mutateStyle)
    {
        var style = StyleOf(theme);
        try
        {
            mutateStyle(ref style);
        }
        finally
        { 
            SetStyleOf(theme, style);
        }
    }

    /// <summary>
    /// Read-only access to Fonts.<br/>
    /// Not valid until first call to <see cref="Run"/>. That’s because since we don’t know the proper <c>PixelsPerPoint</c> until then.
    /// </summary>
    public void Fonts(Action<Fonts> reader)
    {
        Fonts(f =>
        {
            reader(f);
            return false;
        });
    }

    /// <inheritdoc cref="Fonts"/>
    public R Fonts<R>(Func<Fonts, R> reader)
    {
        R result = default!;
        using var callback = new EguiCallback(f => result = reader(new Fonts(f)));
        EguiMarshal.Call(EguiFn.egui_context_Context_fonts, Ptr, callback);
        return result;
    }

    /// <summary>
    /// Read-only access to <see cref="InputState"/>. 
    /// </summary>
    public void Input(Action<InputState> reader)
    {
        Input(i =>
        {
            reader(i);
            return false;
        });
    }

    /// <inheritdoc cref="Input"/>
    public R Input<R>(Func<InputState, R> reader)
    {
        var input = EguiMarshal.Call<nuint, InputState>(EguiFn.egui_context_Context_input, Ptr);
        return reader(input);
    }

    /// <summary>
    /// Read-write access to <see cref="InputState"/>. 
    /// </summary>
    public void InputMut(MutateDelegate<InputState> writer)
    {
        InputMut((ref InputState input) =>
        {
            writer(ref input);
            return false;
        });
    }

    /// <inheritdoc cref="InputMut"/>
    public R InputMut<R>(MutateDelegate<InputState, R> writer)
    {
        var input = Input(x => x);
        try
        {
            return writer(ref input);
        }
        finally
        {
            EguiMarshal.Call(EguiFn.egui_context_Context_input_mut, Ptr, input);
        }
    }

    /// <summary>
    /// Read-only access to <see cref="PlatformOutput"/>. 
    /// </summary>
    public void Output(Action<PlatformOutput> reader)
    {
        Output(i =>
        {
            reader(i);
            return false;
        });
    }

    /// <inheritdoc cref="Input"/>
    public R Output<R>(Func<PlatformOutput, R> reader)
    {
        var input = EguiMarshal.Call<nuint, PlatformOutput>(EguiFn.egui_context_Context_output, Ptr);
        return reader(input);
    }

    /// <summary>
    /// Read-write access to <see cref="PlatformOutput"/>. 
    /// </summary>
    public void OutputMut(MutateDelegate<PlatformOutput> writer)
    {
        OutputMut((ref PlatformOutput output) =>
        {
            writer(ref output);
            return false;
        });
    }

    /// <inheritdoc cref="OutputMut"/>
    public R OutputMut<R>(MutateDelegate<PlatformOutput, R> writer)
    {
        var input = Output(x => x);
        try
        {
            return writer(ref input);
        }
        finally
        {
            EguiMarshal.Call(EguiFn.egui_context_Context_output_mut, Ptr, input);
        }
    }
}