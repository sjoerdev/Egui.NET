using System.Collections.Immutable;

namespace Egui.Ecolor;

public static partial class EcolorHelpers
{
    /// <summary>
    /// All ranges in 0-1, rgb is linear.
    /// </summary>
    public static (float, float, float) HsvFromRgb(ImmutableList<float> rgb)
    {
        return EguiMarshal.Call<ImmutableList<float>, (float, float, float)>(EguiFn.ecolor_hsva_hsv_from_rgb, rgb);
    }

    /// <summary>
    /// All ranges in 0-1, rgb is linear.
    /// </summary>
    public static ImmutableList<float> RgbFromHsv((float, float, float) hsv)
    {
        return EguiMarshal.Call<(float, float, float), ImmutableList<float>>(EguiFn.ecolor_hsva_rgb_from_hsv, hsv);
    }
}