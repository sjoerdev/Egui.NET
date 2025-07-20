namespace Egui;

/// <summary>
/// This is what you use to place widgets.
/// Represents a region of the screen with a type of layout (horizontal or vertical).
/// </summary>
public readonly ref partial struct Ui
{
    /// <summary>
    /// Use this to paint stuff within this <see cref="Ui"/> .
    /// </summary>
    public Painter Painter
    {
        get
        {
            AssertInitialized();
            return new Painter(Ctx, EguiMarshal.Call<EguiHandle>(EguiFn.egui_ui_Ui_painter, Ptr));
        }
    }

    /// <summary>
    /// Read the <see cref="Layout"/> 
    /// </summary>
    public Layout Layout
    {
        get
        {
            AssertInitialized();
            return EguiMarshal.Call<Layout>(EguiFn.egui_ui_Ui_layout, Ptr);
        }
    }

    /// <summary>
    /// The current spacing options for this <see cref="Ui"/> . Short for <c>ui.Style.Spacing</c>.
    /// </summary>
    public Spacing Spacing
    {
        get
        {
            AssertInitialized();
            return EguiMarshal.Call<Spacing>(EguiFn.egui_ui_Ui_spacing, Ptr);
        }
    }

    /// <summary>
    /// Get a reference to this <see cref="Ui"/>'s <see cref="UiStack"/> .
    /// </summary>
    public UiStack Stack
    {
        get
        {
            AssertInitialized();
            return EguiMarshal.Call<UiStack>(EguiFn.egui_ui_Ui_stack, Ptr);
        }
    }

    /// <summary>
    /// Style options for this <see cref="Ui"/>  and its children.
    /// Note that this may be a different <see cref="Style"/>  than that of <see cref="Context.Style"/> .
    /// </summary>
    public Style Style
    {
        get
        {
            AssertInitialized();
            return EguiMarshal.Call<Style>(EguiFn.egui_ui_Ui_style, Ptr);
        }
    }

    /// <summary>
    /// The current visuals settings of this <see cref="Ui"/> . Short for <c>ui.Style.Visuals</c>.
    /// </summary>
    public Visuals Visuals
    {
        get
        {
            AssertInitialized();
            return EguiMarshal.Call<Visuals>(EguiFn.egui_ui_Ui_visuals, Ptr);
        }
    }

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
        AssertInitialized();
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
        AssertInitialized();
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
        AssertInitialized();
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
        AssertInitialized();
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
    /// Convenience function to get a region to paint on.
    /// Note that egui uses screen coordinates for everything.
    /// </summary>
    public readonly (Response, Painter) AllocatePainter(Vec2 desiredSize, Sense sense)
    {
        AssertInitialized();
        var (response, handle) = EguiMarshal.Call<Vec2, Sense, (Response, EguiHandle)>(EguiFn.egui_ui_Ui_painter_at, Ptr, desiredSize, sense);
        return (response, new Painter(Ctx, handle));
    }

    /// <summary>
    /// Create a painter for a sub-region of this <see cref="Ui"/>.
    /// The clip-rect of the returned <see cref="Painter"/>  will be the intersection of the given rectangle and the <see cref="ClipRect"/> of this <see cref="Ui"/>.
    /// </summary>
    public readonly Painter PainterAt(Rect rect)
    {
        AssertInitialized();
        return new Painter(Ctx, EguiMarshal.Call<Rect, EguiHandle>(EguiFn.egui_ui_Ui_painter_at, Ptr, rect));
    }

    /// <summary>
    /// Shortcut for <c>Default())</c><br/>
    /// 
    /// See also <c>Separator</c>.
    /// </summary>
    public readonly Response Separator()
    {
        AssertInitialized();
        return EguiMarshal.Call<Response>(EguiFn.egui_ui_Ui_separator, Ptr);
    }

    /// <summary>
    /// Throws an exception if this is a null object.
    /// </summary>
    internal readonly void AssertInitialized()
    {
        if (Ptr == 0) { throw new NullReferenceException("Ui instance was uninitialized"); }
    }
}