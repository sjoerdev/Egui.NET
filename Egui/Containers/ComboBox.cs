namespace Egui.Containers;

public partial struct ComboBox
{
    /// <summary>
    /// Show the combo box, with the given ui code for the menu contents.<br/>
    /// Returns <c>null</c> if the combo box is closed.
    /// </summary>
    public readonly InnerResponse ShowUi(Ui ui, Action<Ui> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        using var callback = new EguiCallback(ui => addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, ComboBox, EguiCallback, Response>(EguiFn.egui_containers_combo_box_ComboBox_show_ui, ui.Ptr, this, callback);
        return new InnerResponse
        {
            Response = response
        };
    }

    /// <inheritdoc cref="ShowUi"/>
    public readonly InnerResponse<R?> ShowUi<R>(Ui ui, Func<Ui, R> addContents)
    {
        ui.AssertInitialized();
        var ctx = ui.Ctx;
        R? result = default;
        using var callback = new EguiCallback(ui => result = addContents(new Ui(ctx, ui)));
        var response = EguiMarshal.Call<nuint, ComboBox, EguiCallback, Response>(EguiFn.egui_containers_combo_box_ComboBox_show_ui, ui.Ptr, this, callback);
        return new InnerResponse<R?>
        {
            Inner = result,
            Response = response
        };
    }
}