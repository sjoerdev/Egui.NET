namespace Egui.Containers;

public partial struct ScrollArea
{
    /// <summary>
    /// Show the <see cref="ScrollArea"/>, and add the contents to the viewport.<br/>
    /// If the inner area can be very long, consider using <see cref="ShowRows"/> instead.
    /// </summary>
    public readonly ScrollAreaOutput Show(Ui ui, Action<Ui> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var (id, state, contentSize, innerRect) = EguiMarshal.Call<nuint, ScrollArea, EguiCallback, (Id, State, Vec2, Rect)>(EguiFn.egui_containers_scroll_area_ScrollArea_show, ui.Ptr, this, callback);
        return new ScrollAreaOutput
        {
            Id = id,
            State = state,
            ContentSize = contentSize,
            InnerRect = innerRect
        };
    }

    /// <inheritdoc cref="Show"/>
    public readonly ScrollAreaOutput<R> Show<R>(Ui ui, Func<Ui, R> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        R result = default!;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var (id, state, contentSize, innerRect) = EguiMarshal.Call<nuint, ScrollArea, EguiCallback, (Id, State, Vec2, Rect)>(EguiFn.egui_containers_scroll_area_ScrollArea_show, ui.Ptr, this, callback);
        return new ScrollAreaOutput<R>
        {
            Inner = result,
            Id = id,
            State = state,
            ContentSize = contentSize,
            InnerRect = innerRect
        };
    }
}