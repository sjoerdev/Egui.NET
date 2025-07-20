namespace Egui;

public partial struct Grid
{
    public readonly InnerResponse Show(Ui ui, Action<Ui> addContents)
    {
        var ctx = ui.Ctx;
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<Grid, EguiCallback, Response>(EguiFn.egui_grid_Grid_show, ui.Ptr, this, callback);
        return new InnerResponse
        {
            Response = response
        };
    }

    public readonly InnerResponse<R> Show<R>(Ui ui, Func<Ui, R> addContents)
    {
        R result = default!;
        var ctx = ui.Ctx;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<Grid, EguiCallback, Response>(EguiFn.egui_grid_Grid_show, ui.Ptr, this, callback);
        return new InnerResponse<R>
        {
            Inner = result,
            Response = response
        };
    }
}