namespace Egui.Containers;

public partial struct AreaState
{
    /// <summary>
    /// Load the state of an <see cref="Area"/> from memory.
    /// </summary>
    public static AreaState? Load(Context ctx, Id id)
    {
        return EguiMarshal.Call<Id, AreaState>(EguiFn.egui_containers_area_AreaState_load, ctx.Handle.ptr, id);
    }
}