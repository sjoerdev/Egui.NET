namespace Egui;

public partial struct CentralPanel
{
    public readonly InnerResponse Show(Context ctx, Action<Ui> addContents)
    {
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<CentralPanel, EguiCallback, Response>(EguiFn.egui_containers_panel_CentralPanel_show, ctx.Handle.ptr, this, callback);
        return new InnerResponse
        {
            Response = response
        };
    }

    public readonly InnerResponse<R> Show<R>(Context ctx, Func<Ui, R> addContents)
    {
        R result = default!;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<CentralPanel, EguiCallback, Response>(EguiFn.egui_containers_panel_CentralPanel_show, ctx.Handle.ptr, this, callback);
        return new InnerResponse<R>
        {
            Inner = result,
            Response = response
        };
    }
}