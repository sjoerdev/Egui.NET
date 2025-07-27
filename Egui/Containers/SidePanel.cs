namespace Egui.Containers;

public partial struct SidePanel
{
    /// <summary>
    /// Show either a collapsed or a expanded panel, with a nice animation between.
    /// </summary>
    public unsafe static InnerResponse? ShowAnimatedBetween(Context ctx, bool isExpanded, SidePanel collapsedPanel, SidePanel expandedPanel, Action<Ui, float> addContents)
    {
        using var callback = new EguiCallback(data =>
        {
            EguiAnimatedUi animatedUi = *(EguiAnimatedUi*)data;
            addContents(new Ui(ctx, animatedUi.ui), animatedUi.t);
        });

        var response = EguiMarshal.Call<nuint, bool, SidePanel, SidePanel, EguiCallback, Response?>(EguiFn.egui_containers_panel_SidePanel_show_animated_between, ctx.Ptr, isExpanded, collapsedPanel, expandedPanel, callback);

        if (response.HasValue)
        {
            return new InnerResponse
            {
                Response = response.Value
            };
        }
        else
        {
            return null;
        }
    }
    
    /// <inheritdoc cref="ShowAnimatedBetween"/>
    public unsafe static InnerResponse<R>? ShowAnimatedBetween<R>(Context ctx, bool isExpanded, SidePanel collapsedPanel, SidePanel expandedPanel, Func<Ui, float, R> addContents)
    {
        R result = default!;
        using var callback = new EguiCallback(data =>
        {
            EguiAnimatedUi animatedUi = *(EguiAnimatedUi*)data;
            result = addContents(new Ui(ctx, animatedUi.ui), animatedUi.t);
        });

        var response = EguiMarshal.Call<nuint, bool, SidePanel, SidePanel, EguiCallback, Response?>(EguiFn.egui_containers_panel_SidePanel_show_animated_between, ctx.Ptr, isExpanded, collapsedPanel, expandedPanel, callback);

        if (response.HasValue)
        {
            return new InnerResponse<R>
            {
                Inner = result,
                Response = response.Value
            };
        }
        else
        {
            return null;
        }
    }
    
    /// <summary>
    /// Show either a collapsed or a expanded panel, with a nice animation between.
    /// </summary>
    public unsafe static InnerResponse? ShowAnimatedBetweenInside(Ui ui, bool isExpanded, SidePanel collapsedPanel, SidePanel expandedPanel, Action<Ui, float> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;

        using var callback = new EguiCallback(data =>
        {
            EguiAnimatedUi animatedUi = *(EguiAnimatedUi*)data;
            addContents(new Ui(ctx, animatedUi.ui), animatedUi.t);
        });

        var response = EguiMarshal.Call<nuint, bool, SidePanel, SidePanel, EguiCallback, Response?>(EguiFn.egui_containers_panel_SidePanel_show_animated_between_inside, ui.Ptr, isExpanded, collapsedPanel, expandedPanel, callback);

        if (response.HasValue)
        {
            return new InnerResponse
            {
                Response = response.Value
            };
        }
        else
        {
            return null;
        }
    }
    
    /// <inheritdoc cref="ShowAnimatedBetweenInside"/>
    public unsafe static InnerResponse<R>? ShowAnimatedBetweenInside<R>(Ui ui, bool isExpanded, SidePanel collapsedPanel, SidePanel expandedPanel, Func<Ui, float, R> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        R result = default!;
        using var callback = new EguiCallback(data =>
        {
            EguiAnimatedUi animatedUi = *(EguiAnimatedUi*)data;
            result = addContents(new Ui(ctx, animatedUi.ui), animatedUi.t);
        });

        var response = EguiMarshal.Call<nuint, bool, SidePanel, SidePanel, EguiCallback, Response?>(EguiFn.egui_containers_panel_SidePanel_show_animated_between_inside, ui.Ptr, isExpanded, collapsedPanel, expandedPanel, callback);

        if (response.HasValue)
        {
            return new InnerResponse<R>
            {
                Inner = result,
                Response = response.Value
            };
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Show the panel at the top level.
    /// </summary>
    public readonly InnerResponse Show(Context ctx, Action<Ui> addContents)
    {
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, SidePanel, EguiCallback, Response>(EguiFn.egui_containers_panel_SidePanel_show, ctx.Ptr, this, callback);
        return new InnerResponse
        {
            Response = response
        };
    }

    /// <inheritdoc cref="Show"/>
    public readonly InnerResponse<R> Show<R>(Context ctx, Func<Ui, R> addContents)
    {
        R result = default!;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, SidePanel, EguiCallback, Response>(EguiFn.egui_containers_panel_SidePanel_show, ctx.Ptr, this, callback);
        return new InnerResponse<R>
        {
            Inner = result,
            Response = response
        };
    }

    /// <summary>
    /// Show the panel if <c>IsExpanded</c> is <c>True</c>,
    /// otherwise don't show it, but with a nice animation between collapsed and expanded.
    /// </summary>
    public readonly InnerResponse? ShowAnimated(Context ctx, bool isExpanded, Action<Ui> addContents)
    {
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, bool, SidePanel, EguiCallback, Response?>(EguiFn.egui_containers_panel_SidePanel_show_animated, ctx.Ptr, isExpanded, this, callback);

        if (response.HasValue)
        {
            return new InnerResponse
            {
                Response = response.Value
            };
        }
        else
        {
            return null;
        }
    }

    /// <inheritdoc cref="ShowAnimated"/>
    public readonly InnerResponse<R>? ShowAnimated<R>(Context ctx, bool isExpanded, Func<Ui, R> addContents)
    {
        R result = default!;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, bool, SidePanel, EguiCallback, Response?>(EguiFn.egui_containers_panel_SidePanel_show_animated, ctx.Ptr, isExpanded, this, callback);

        if (response.HasValue)
        {
            return new InnerResponse<R>
            {
                Inner = result,
                Response = response.Value
            };
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Show the panel inside a <see cref="Ui"/>. 
    /// </summary>
    public readonly InnerResponse ShowInside(Ui ui, Action<Ui> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, SidePanel, EguiCallback, Response>(EguiFn.egui_containers_panel_SidePanel_show_inside, ui.Ptr, this, callback);
        return new InnerResponse
        {
            Response = response
        };
    }

    /// <inheritdoc cref="ShowInside"/>
    public readonly InnerResponse<R> ShowInside<R>(Ui ui, Func<Ui, R> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        R result = default!;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, SidePanel, EguiCallback, Response>(EguiFn.egui_containers_panel_SidePanel_show_inside, ui.Ptr, this, callback);
        return new InnerResponse<R>
        {
            Inner = result,
            Response = response
        };
    }
    

    /// <summary>
    /// Show the panel if <c>IsExpanded</c> is <c>True</c>,
    /// otherwise don't show it, but with a nice animation between collapsed and expanded.
    /// </summary>
    public readonly InnerResponse? ShowAnimatedInside(Ui ui, bool isExpanded, Action<Ui> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, bool, SidePanel, EguiCallback, Response?>(EguiFn.egui_containers_panel_SidePanel_show_animated_inside, ui.Ptr, isExpanded, this, callback);

        if (response.HasValue)
        {
            return new InnerResponse
            {
                Response = response.Value
            };
        }
        else
        {
            return null;
        }
    }

    /// <inheritdoc cref="ShowAnimatedInside"/>
    public readonly InnerResponse<R>? ShowAnimatedInside<R>(Ui ui, bool isExpanded, Func<Ui, R> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        R result = default!;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, bool, SidePanel, EguiCallback, Response?>(EguiFn.egui_containers_panel_SidePanel_show_animated_inside, ui.Ptr, isExpanded, this, callback);

        if (response.HasValue)
        {
            return new InnerResponse<R>
            {
                Inner = result,
                Response = response.Value
            };
        }
        else
        {
            return null;
        }
    }
}