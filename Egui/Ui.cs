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
            return new Painter(Ctx, EguiMarshal.Call<nuint, EguiHandle>(EguiFn.egui_ui_Ui_painter, Ptr));
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
            return EguiMarshal.Call<nuint, Layout>(EguiFn.egui_ui_Ui_layout, Ptr);
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
            return EguiMarshal.Call<nuint, Spacing>(EguiFn.egui_ui_Ui_spacing, Ptr);
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
            return EguiMarshal.Call<nuint, UiStack>(EguiFn.egui_ui_Ui_stack, Ptr);
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
            return EguiMarshal.Call<nuint, Style>(EguiFn.egui_ui_Ui_style, Ptr);
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
            return EguiMarshal.Call<nuint, Visuals>(EguiFn.egui_ui_Ui_visuals, Ptr);
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
    public readonly Response Add<T>(T widget) where T : IWidget, allows ref struct
    {
        AssertInitialized();
        return widget.Ui(this);
    }

    /// <summary>
    /// Add a single <see cref="IWidget"/> that is possibly disabled, i.e. greyed out and non-interactive.
    ///
    /// If you call <see cref="AddEnabled"/>  from within an already disabled <see cref="Ui"/> ,
    /// the widget will always be disabled, even if the <paramref name="enabled"/> argument is true.
    ///
    /// See also <see cref="AddEnabledUi"/>  and <see cref="IsEnabled"/> .
    /// </summary>
    public unsafe readonly Response AddEnabled<T>(bool enabled, T widget) where T : IWidget, allows ref struct
    {
#pragma warning disable CS8500
        if (IsEnabled && !enabled)
        {
            var tp = &widget;
            return Scope(ui =>
            {
                ui.Disable();
                return ui.Add(*tp);
            }).Inner;
        }
        else
        {
            return Add(widget);
        }         
#pragma warning restore CS8500
    }

    /// <summary>
    /// Add a <see cref="Widget"/>  to this <see cref="Ui"/>  at a location dependent on the current <see cref="Layout"/> .
    ///
    /// The returned <see cref="Response"/> can be used to check for interactions,
    /// as well as adding tooltips using <see cref="Response.OnHoverText"/> .
    ///
    /// See also <see cref="AddSized"/>  and <see cref="Put"/> .
    /// </summary>
    public unsafe readonly Response AddSized<T>(Vec2 maxSize, T widget) where T : IWidget, allows ref struct
    {
#pragma warning disable CS8500
        var tp = &widget;
        var layout = Layout.CenteredAndJustified(Layout.MainDir);
        return AllocateUiWithLayout(maxSize, layout, ui => ui.Add(*tp)).Inner;
#pragma warning restore CS8500
    }

    /// <summary>
    /// Add a Widget to this <see cref="Ui"/> at a specific location (manual layout).<br/>
    /// See also <see cref="Add"/>  and <see cref="AddSized"/>.
    /// </summary>
    public unsafe readonly Response Put<T>(Rect maxRect, T widget) where T : IWidget, allows ref struct
    {
#pragma warning disable CS8500
        var tp = &widget;
        return ScopeBuilder(
            new UiBuilder()
                .WithMaxRect(maxRect)
                .WithLayout(Layout.CenteredAndJustified(Direction.TopDown)),
            ui => ui.Add(*tp)
        ).Inner;
#pragma warning restore CS8500
    }

    /// <summary>
    /// Add a single <see cref="IWidget"/>  that is possibly invisible.
    ///
    /// An invisible widget still takes up the same space as if it were visible.
    ///
    /// If you call <see cref="AddVisible"/>  from within an already invisible <see cref="Ui"/> ,
    /// the widget will always be invisible, even if the <paramref name="visible"/> argument is true.
    ///
    /// See also <see cref="AddVisibleUi"/> , <see cref="SetVisible"/> and <see cref="IsVisible"/>.
    /// </summary>
    public unsafe readonly Response AddVisible<T>(bool visible, T widget) where T : IWidget, allows ref struct
    {
#pragma warning disable CS8500
        if (IsVisible && !visible)
        {
            var tp = &widget;
            return Scope(ui =>
            {
                ui.SetInvisible();
                return ui.Add(*tp);
            }).Inner;
        }
        else
        {
            return Add(widget);
        }
#pragma warning restore CS8500
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
        var response = EguiMarshal.Call<nuint, Vec2, Layout, EguiCallback, Response>(EguiFn.egui_ui_Ui_allocate_ui_with_layout, Ptr, desiredSize, layout, callback);
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
        var response = EguiMarshal.Call<nuint, Vec2, Layout, EguiCallback, Response>(EguiFn.egui_ui_Ui_allocate_ui_with_layout, Ptr, desiredSize, layout, callback);
        return new InnerResponse<R>
        {
            Inner = result,
            Response = response
        };
    }

    /// <summary>
    /// Add a section that is possibly disabled, i.e. greyed out and non-interactive.
    /// </summary>
    public readonly InnerResponse AddEnabledUi(bool enabled, Action<Ui> addContents)
    {
        return Scope(ui =>
        {
            if (!enabled)
            {
                ui.Disable();
            }
            addContents(ui);
        });
    }

    /// <inheritdoc cref="AddEnabledUi"/>
    public readonly InnerResponse<R> AddEnabledUi<R>(bool enabled, Func<Ui, R> addContents)
    {
        return Scope(ui =>
        {
            if (!enabled)
            {
                ui.Disable();
            }
            return addContents(ui);
        });
    }

    /// <summary>
    /// Create a scoped child ui.
    /// </summary>
    public readonly InnerResponse Scope(Action<Ui> addContents)
    {
        return ScopeBuilder(new UiBuilder(), addContents);
    }

    /// <inheritdoc cref="Scope"/>
    public readonly InnerResponse<R> Scope<R>(Func<Ui, R> addContents)
    {
        return ScopeBuilder(new UiBuilder(), addContents);
    }

    /// <summary>
    /// Create a child, add content to it, and then allocate only what was used in the parent <see cref="Ui"/> .
    /// </summary>
    public readonly InnerResponse ScopeBuilder(UiBuilder uiBuilder, Action<Ui> addContents)
    {
        AssertInitialized();
        var ctx = Ctx;
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, UiBuilder, EguiCallback, Response>(EguiFn.egui_ui_Ui_scope_builder, Ptr, uiBuilder, callback);
        return new InnerResponse
        {
            Response = response
        };
    }

    /// <inheritdoc cref="ScopeBuilder"/>
    public readonly InnerResponse<R> ScopeBuilder<R>(UiBuilder uiBuilder, Func<Ui, R> addContents)
    {
        AssertInitialized();
        R result = default!;
        var ctx = Ctx;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, UiBuilder, EguiCallback, Response>(EguiFn.egui_ui_Ui_scope_builder, Ptr, uiBuilder, callback);
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
        var (response, handle) = EguiMarshal.Call<nuint, Vec2, Sense, (Response, EguiHandle)>(EguiFn.egui_ui_Ui_painter_at, Ptr, desiredSize, sense);
        return (response, new Painter(Ctx, handle));
    }

    /// <summary>
    /// Show a checkbox.<br/>
    /// 
    /// See also <c>ToggleValue</c>.
    /// </summary>
    public Response Checkbox(ref bool isChecked, Atoms atoms)
    {       
        var (result, checkedResult) = EguiMarshal.Call<nuint, bool, Atoms, (Response, bool)>(EguiFn.egui_ui_Ui_checkbox, Ptr, isChecked, atoms);
        isChecked = checkedResult;
        return result;
    }

    /// <summary>
    /// Create a painter for a sub-region of this <see cref="Ui"/>.
    /// The clip-rect of the returned <see cref="Painter"/>  will be the intersection of the given rectangle and the <see cref="ClipRect"/> of this <see cref="Ui"/>.
    /// </summary>
    public readonly Painter PainterAt(Rect rect)
    {
        AssertInitialized();
        return new Painter(Ctx, EguiMarshal.Call<nuint, Rect, EguiHandle>(EguiFn.egui_ui_Ui_painter_at, Ptr, rect));
    }

    /// <summary>
    /// Shortcut for <c>Default()</c><br/>
    /// 
    /// See also <c>Separator</c>.
    /// </summary>
    public readonly Response Separator()
    {
        AssertInitialized();
        return EguiMarshal.Call<nuint, Response>(EguiFn.egui_ui_Ui_separator, Ptr);
    }

    /// <summary>
    /// Throws an exception if this is a null object.
    /// </summary>
    internal readonly void AssertInitialized()
    {
        if (Ptr == 0) { throw new NullReferenceException("Ui instance was uninitialized"); }
    }
}