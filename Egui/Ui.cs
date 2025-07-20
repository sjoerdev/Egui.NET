namespace Egui;

/// <summary>
/// This is what you use to place widgets.
/// Represents a region of the screen with a type of layout (horizontal or vertical).
/// </summary>
public readonly ref partial struct Ui
{
    /// <summary>
    /// Read the <see cref="Layout"/> 
    /// </summary>
    public Layout Layout => EguiMarshal.Call<Layout>(EguiFn.egui_ui_Ui_layout, Ptr);

    /// <summary>
    /// The current spacing options for this <see cref="Ui"/> . Short for <c>ui.Style.Spacing</c>.
    /// </summary>
    public Spacing Spacing => EguiMarshal.Call<Spacing>(EguiFn.egui_ui_Ui_spacing, Ptr);

    /// <summary>
    /// Get a reference to this <see cref="Ui"/>'s <see cref="UiStack"/> .
    /// </summary>
    public UiStack Stack => EguiMarshal.Call<UiStack>(EguiFn.egui_ui_Ui_stack, Ptr);

    /// <summary>
    /// Style options for this <see cref="Ui"/>  and its children.
    /// Note that this may be a different <see cref="Style"/>  than that of <see cref="Context.Style"/> .
    /// </summary>
    public Style Style => EguiMarshal.Call<Style>(EguiFn.egui_ui_Ui_style, Ptr);

    /// <summary>
    /// The current visuals settings of this <see cref="Ui"/> . Short for <c>ui.Style.Visuals</c>.
    /// </summary>
    public Visuals Visuals => EguiMarshal.Call<Visuals>(EguiFn.egui_ui_Ui_visuals, Ptr);

    /// <summary>
    /// Get a reference to the parent <see cref="Context"/>. 
    /// </summary>
    public readonly Context Ctx;

    /// <summary>
    /// A pointer to the underlying UI object.
    /// </summary>
    internal readonly nuint Ptr;

    /// <summary>
    /// Creates a UI reference.
    /// </summary>
    /// <param name="ctx">The context that owns the UI.</param>
    /// <param name="ptr">A pointer to the UI.</param>
    internal Ui(Context ctx, nuint ptr)
    {
        Ctx = ctx;
        Ptr = ptr;
    }

    /// <summary>
    /// Add a <see cref="Widget"/>  to this <see cref="Ui"/>  at a location dependent on the current <see cref="Layout"/> .
    ///
    /// The returned <see cref="Response"/> can be used to check for interactions,
    /// as well as adding tooltips using <see cref="Response.OnHoverText"/> .
    ///
    /// See also <see cref="AddSized"/>  and <see cref="Put"/> .
    /// </summary>
    public readonly Response Add<T>(T widget) where T : Widget, allows ref struct
    {
        return widget.Ui(this);
    }

    /// <summary>
    /// Add a <see cref="Widget"/>  to this <see cref="Ui"/>  at a location dependent on the current <see cref="Layout"/> .
    ///
    /// The returned <see cref="Response"/> can be used to check for interactions,
    /// as well as adding tooltips using <see cref="Response.OnHoverText"/> .
    ///
    /// See also <see cref="AddSized"/>  and <see cref="Put"/> .
    /// </summary>
    public readonly Response AddSized<T>(Vec2 maxSize, T widget) where T : Widget, allows ref struct
    {
        var layout = Layout.CenteredAndJustified(Layout.MainDir);
        throw new NotImplementedException();
        //return AllocateUiWithLayout(maxSize, layout, ui => ui.Add(widget))
    }

    /// <summary>
    /// Allocated the given space and then adds content to that space.
    /// If the contents overflow, more space will be allocated.
    /// When finished, the amount of space actually used (<c>min_rect</c>) will be allocated.
    /// So you can request a lot of space and then use less.
    /// </summary>
    public readonly InnerResponse AllocateUiWithLayout(Vec2 desiredSize, Layout layout, Action<Ui> addContents)
    {
        var ctx = Ctx;
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<Vec2, Layout, EguiCallback, Response>(EguiFn.egui_ui_Ui_allocate_ui_with_layout, Ptr, desiredSize, layout, callback);
        return new InnerResponse
        {
            Response = response
        };
    }

    /// <inheritdoc cref="AllocateUiWithLayout"/>
    public readonly InnerResponse<R> AllocateUiWithLayout<R>(Vec2 desiredSize, Layout layout, Func<Ui, R> addContents)
    {
        R result = default!;
        var ctx = Ctx;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<Vec2, Layout, EguiCallback, Response>(EguiFn.egui_ui_Ui_allocate_ui_with_layout, Ptr, desiredSize, layout, callback);
        return new InnerResponse<R>
        {
            Inner = result,
            Response = response
        };
    }

    /// <summary>
    /// Shortcut for <c>Default())</c><br/>
    /// 
    /// See also <c>Separator</c>.
    /// </summary>
    public readonly Response Separator()
    {
        if (Ptr == 0) { throw new NullReferenceException("Ui instance was uninitialized"); }
        return EguiMarshal.Call<Response>(EguiFn.egui_ui_Ui_separator, Ptr);
    }
}