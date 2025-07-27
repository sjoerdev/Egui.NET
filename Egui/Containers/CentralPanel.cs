namespace Egui.Containers;

public partial struct CentralPanel
{
    public readonly InnerResponse Show(Context ctx, Action<Ui> addContents)
    {
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, CentralPanel, EguiCallback, Response>(EguiFn.egui_containers_panel_CentralPanel_show, ctx.Ptr, this, callback);
        return new InnerResponse
        {
            Response = response
        };
    }

    public readonly InnerResponse<R> Show<R>(Context ctx, Func<Ui, R> addContents)
    {
        R result = default!;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, CentralPanel, EguiCallback, Response>(EguiFn.egui_containers_panel_CentralPanel_show, ctx.Ptr, this, callback);
        return new InnerResponse<R>
        {
            Inner = result,
            Response = response
        };
    }

    public readonly InnerResponse ShowInside(Ui ui, Action<Ui> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, CentralPanel, EguiCallback, Response>(EguiFn.egui_containers_panel_CentralPanel_show_inside, ui.Ptr, this, callback);
        return new InnerResponse
        {
            Response = response
        };
    }

    public readonly InnerResponse<R> ShowInside<R>(Ui ui, Func<Ui, R> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        R result = default!;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, CentralPanel, EguiCallback, Response>(EguiFn.egui_containers_panel_CentralPanel_show_inside, ui.Ptr, this, callback);
        return new InnerResponse<R>
        {
            Inner = result,
            Response = response
        };
    }
}