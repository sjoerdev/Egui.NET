namespace Egui.Containers;

public partial struct TopBottomPanel
{
    /// <summary>
    /// Show the panel at the top level.
    /// </summary>
    public readonly InnerResponse Show(Context ctx, Action<Ui> addContents)
    {
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, TopBottomPanel, EguiCallback, Response>(EguiFn.egui_containers_panel_TopBottomPanel_show, ctx.Ptr, this, callback);
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
        var response = EguiMarshal.Call<nuint, TopBottomPanel, EguiCallback, Response>(EguiFn.egui_containers_panel_TopBottomPanel_show, ctx.Ptr, this, callback);
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
        var response = EguiMarshal.Call<nuint, bool, TopBottomPanel, EguiCallback, Response?>(EguiFn.egui_containers_panel_TopBottomPanel_show_animated, ctx.Ptr, isExpanded, this, callback);

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
        var response = EguiMarshal.Call<nuint, bool, TopBottomPanel, EguiCallback, Response?>(EguiFn.egui_containers_panel_TopBottomPanel_show_animated, ctx.Ptr, isExpanded, this, callback);
        
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