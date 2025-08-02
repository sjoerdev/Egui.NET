using System.Collections.Immutable;

namespace Egui.Widgets;

public partial struct FrameDurations
{
    public IEnumerable<Duration> All => EguiMarshal.Call<FrameDurations, ImmutableList<Duration>>(EguiFn.egui_widgets_image_FrameDurations_all, this);
}