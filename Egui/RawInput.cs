namespace Egui;

public partial struct RawInput
{
    /// <summary>
    /// Info about the active viewport
    /// </summary>
    public ViewportInfo Viewport
    {
        get
        {
            return EguiMarshal.Call<RawInput, ViewportInfo>(EguiFn.egui_data_input_RawInput_viewport, 0, this);
        }
    }

    /// <summary>
    /// Add on new input.
    /// </summary>
    public void Append(RawInput newer)
    {
        this = EguiMarshal.Call<RawInput, RawInput, RawInput>(EguiFn.egui_data_input_RawInput_append, 0, this, newer);
    }

    /// <summary>
    /// Helper: move volatile (deltas and events), clone the rest.<br/>
    /// <see cref="HoveredFiles"/> is moved. <see cref="DroppedFiles"/> is moved. 
    /// </summary>
    public RawInput Take()
    {
        var (newThis, result) = EguiMarshal.Call<RawInput, (RawInput, RawInput)>(EguiFn.egui_data_input_RawInput_take, 0, this);
        this = newThis;
        return result;
    }
}